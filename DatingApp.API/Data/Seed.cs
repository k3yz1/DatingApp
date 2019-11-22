using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DatingApp.API.Model;

namespace DatingApp.API.Data
{
    public class Seed
    {
        public async static Task SeedUsers(DataContext context)
        {
            if (!context.Users.Any())
            {
                var userData = File.ReadAllText("Data/UserSeedData.json");
                var options = new JsonSerializerOptions
                {
                    // PropertyNamingPolicy = JsonNamingPolicy.,
                    WriteIndented = false,

                };
                var users = JsonSerializer.Deserialize<IEnumerable<User>>(userData, options);
                var repository = new AuthRepository(context);
                foreach (var user in users)
                {
                    await repository.Register(user, "password");
                }
            }
        }
    }
}