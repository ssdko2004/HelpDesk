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
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(
            _context.TicketCategories.Where(c => c.IsActive).OrderBy(c => c.SortOrder), "Id", "Name");

        ViewBag.Departments = new SelectList(
            _context.Departments.Where(d => d.IsActive).OrderBy(d => d.Name), "Id", "Name");

        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTicketViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        foreach (var claim in User.Claims)
        {
            Console.WriteLine($"{claim.Type}: {claim.Value}");
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


        //return RedirectToAction(nameof(MyTickets));
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> ProjectsByDepartmet(Guid departmentId)
    {
        var projects = await _context.Projects
            .Where(p => p.DepartmentId == departmentId && p.IsActive)
            .OrderBy(p => p.Name)
            .Select(p => new { p.Id, p.Name })
            .ToListAsync();

        return Json(projects);
    }
   
}
