using Microsoft.AspNetCore.Mvc;
using HelpDesk.Models;
using Microsoft.AspNetCore.Authorization;
using HelpDesk.ViewModels;
using HelpDesk.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Controllers;

[Authorize]
public class TicketsController(ApplicationDbContext context) : Controller
{
    ApplicationDbContext _context = context;

    [HttpGet]
    public async Task<IActionResult> Create()
    {                 
        await PopulateDropdownsAsync(null);
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTicketViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropdownsAsync(model);
            return View(model);
        }

        var ticket = new Ticket
        {
            Title = model.Title,
            Description = model.Description,
            Priority = model.Priority,
            CategoryId = model.CategoryId,
            ProjectId = model.ProjectId,
            DepartmentId = model.DepartmentId,
            Status = "New",
            RequesterUserId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? 
                throw new Exception("User ID not found"))
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();


        
        return RedirectToAction(nameof(MyTickets), "Tickets" );
    }

    [HttpGet]
    public async Task<IActionResult> ProjectsByDepartmentAsync(Guid departmentId)
    {
        Console.WriteLine($"Fetching projects for DepartmentId: {departmentId}");
        var projects = await _context.Projects
            .Where(p => p.DepartmentId == departmentId && p.IsActive)
            .OrderBy(p => p.Name)
            .Select(p => new { p.Id, p.Name })
            .ToListAsync();

        return Json(projects);
    }

    [HttpGet]
    public async Task<IActionResult> MyTickets(string sortOrder = MyTicketSortOrders.DateDesc)
    {        

        MyTicketsViewModel viewModel = new()
        {
            SortOrder = sortOrder
        };

        var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? 
            throw new Exception("User ID not found"));

        IQueryable<Ticket> ticketsQuery = _context.Tickets;

        ticketsQuery = ticketsQuery
            .Where(t => t.RequesterUserId == userId)
            .Include(t => t.Category)
            .Include(t => t.Project)
            .Include(t => t.Department);

        ticketsQuery = sortOrder switch
        {
            MyTicketSortOrders.DateAsc => ticketsQuery.OrderBy(t => t.CreatedAtUtc),
            MyTicketSortOrders.DateDesc => ticketsQuery.OrderByDescending(t => t.CreatedAtUtc),
            MyTicketSortOrders.TitleAsc => ticketsQuery.OrderBy(t => t.Title),
            MyTicketSortOrders.TitleDesc => ticketsQuery.OrderByDescending(t => t.Title),
            MyTicketSortOrders.StatusAsc => ticketsQuery.OrderBy(t => t.Status),
            MyTicketSortOrders.StatusDesc => ticketsQuery.OrderByDescending(t => t.Status),
            _ => ticketsQuery.OrderByDescending(t => t.CreatedAtUtc),
        };
        viewModel.Tickets = await ticketsQuery.ToListAsync();
        
        return View(viewModel);
    }
   
    private async Task PopulateDropdownsAsync(CreateTicketViewModel? model)
    {
        ViewBag.Categories = new SelectList(
            await _context.TicketCategories.Where(c => c.IsActive).OrderBy(c => c.SortOrder).ToListAsync(), "Id", "Name");

        ViewBag.Departments = new SelectList(
            await _context.Departments.Where(d => d.IsActive).OrderBy(d => d.Name).ToListAsync(), "Id", "Name");

        if (model != null && model.DepartmentId.HasValue)
        {
            ViewBag.Projects = new SelectList(
                await _context.Projects.Where(p => p.DepartmentId == model.DepartmentId && p.IsActive).OrderBy(p => p.Name).ToListAsync(), "Id", "Name");
        }
        else
        {
            ViewBag.Projects = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
        }
    }

}
