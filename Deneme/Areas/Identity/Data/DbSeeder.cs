using Deneme.Constants;
using Microsoft.AspNetCore.Identity;

namespace Deneme.Areas.Identity.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
            var user = new ApplicationUser
            {
                UserName = "g201210078@sakarya.edu.tr",
                Email = "g2012100078@sakarya.edu.tr",
                FirstAndLastName="Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true

            };
            var userInDb = await userManager.FindByEmailAsync(user.Email);
            if (userInDb == null) 
            {
                await userManager.CreateAsync(user, "Sau.123");
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }
            
        }
    }
}
