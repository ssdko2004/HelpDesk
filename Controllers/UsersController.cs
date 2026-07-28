using HelpDesk.Data;
using HelpDesk.Enums;
using HelpDesk.Models;
using HelpDesk.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Controllers;

[Authorize(Roles = "Admin")]
public class UsersController(ApplicationDbContext context) : Controller
{
    ApplicationDbContext _context = context;

    [HttpGet]
    public async Task<IActionResult> Index(string sortOrder = UserSortOrders.FullNameAsc)
    {

        UsersViewModel viewModel = new()
        {           
            SortOrder = sortOrder
        };

        IQueryable<ApplicationUser> usersQuery = _context.Users;

        usersQuery = sortOrder switch
        {
            UserSortOrders.FullNameAsc => usersQuery.OrderBy(u => u.FullName),
            UserSortOrders.FullNameDesc => usersQuery.OrderByDescending(u => u.FullName),
            UserSortOrders.DateCreatedAsc => usersQuery.OrderBy(u => u.CreatedAtUtc),
            UserSortOrders.DateCreatedDesc => usersQuery.OrderByDescending(u => u.CreatedAtUtc),
            _ => usersQuery
        };

        viewModel.Users = await usersQuery.ToListAsync();
        return View(viewModel);
    }
}