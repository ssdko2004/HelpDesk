using HelpDesk.Models;
using Microsoft.AspNetCore.Identity;

namespace HelpDesk.Services;

public static class SeedDataService
{
    public static async Task SeedAsync(IServiceProvider services, IConfiguration config)
    {
        using var scope = services.CreateScope();

        var rolleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var roles = new[] { "Admin", "TeamLead", "TeamMember", "NetworkManager" };

        foreach (var role in roles)
        {
            if (!await rolleManager.RoleExistsAsync(role))
            {
                await rolleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }

        var adminEmail = config["SeedAdmin:Email"];
        var adminPassword = config["SeedAdmin:Password"];
        var adminName = config["SeedAdmin:FullName"] ?? "System Admin";

        if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
        {
            return;
        }

        var existing = await userManager.FindByEmailAsync(adminEmail);
        if (existing == null)
        {
            var adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = adminName,
                IsActive = true,
                CreatedAtUtc = DateTime.UtcNow,
                UpdatedAtUtc = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
      
    }
}