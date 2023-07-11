using ITHealth.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace ITHealth.Data
{
    public class RoleInitializer
    {
        public static async Task RoleInitializeAsync(RoleManager<IdentityRole<int>> roleManager)
        {
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                if (await roleManager.FindByNameAsync(role.ToString()) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role.ToString()));
                }
            }
        }
    }
}
