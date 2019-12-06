using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DatingApp.API.Model;
using Microsoft.AspNetCore.Identity;

namespace DatingApp.API.Data
{
    public class Seed
    {
        public async static Task SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var userData = File.ReadAllText("Data/UserSeedData.json");
                var options = new JsonSerializerOptions
                {
                    // PropertyNamingPolicy = JsonNamingPolicy.,
                    WriteIndented = false,

                };
                var users = JsonSerializer.Deserialize<IEnumerable<User>>(userData, options);
                
                var roles = new List<Role>
                {
                    new Role { Name = "Member" },
                    new Role { Name = "Admin" },
                    new Role { Name = "Moderator" },
                    new Role { Name = "VIP" },
                };

                foreach(var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
             
                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "password"); 
                    await userManager.AddToRoleAsync(user, "Member");                  
                }

                var adminUser = new User
                {
                    UserName = "admin"
                };

                var result = await userManager.CreateAsync(adminUser, "password");

                if(result.Succeeded)
                {
                    var admin = await userManager.FindByNameAsync("admin");
                    await userManager.AddToRolesAsync(admin, new [] { "Admin", "Moderator" });      
                }
            }
        }
    }
}