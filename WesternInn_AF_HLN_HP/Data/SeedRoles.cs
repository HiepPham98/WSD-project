using Microsoft.AspNetCore.Identity;

namespace WesternInn_AF_HLN_HP.Data
{
    public class SeedRoles
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            // Get the RoleManager and the UserManager objects
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            // Include role name here
            string[] roleNames = { "administrators", "guests" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                // check whether roles already exists
                var roleExists = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    //creating the roles
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //creating an admin user who will maintain the web app
            //His/her configuration are read from the configuration file: asp.settings
            var powerUser = new IdentityUser
            {
                UserName = Configuration.GetSection("UserSettings")["UserEmail"],
                Email = Configuration.GetSection("UserSettings")["UserEmail"]
            };
            string userPassword = Configuration.GetSection("UserSettings")["UserPassword"];
            var user = await UserManager.FindByEmailAsync(Configuration.GetSection("UserSettings")["UserEmail"]);
            // if this admin user doesn't exist in the database, ​create it in the database;
            // otherwise, do nothing.
            if (user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(powerUser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    // here we assign the new user the "Admin" role 
                    await UserManager.AddToRoleAsync(powerUser, "administrators");
                }
            }
        }
    }
}
