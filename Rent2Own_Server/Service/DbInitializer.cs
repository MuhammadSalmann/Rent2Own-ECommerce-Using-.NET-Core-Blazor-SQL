using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using Rent2Own_Common;
using Rent2Own_DataAccess;
using Rent2Own_Server.Service.IService;

namespace Rent2Own_Server.Service
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DBHelper _db;
        public DbInitializer(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            DBHelper db)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
                if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                }
                else
                {
                    return;
                }

                IdentityUser user = new()
                {
                    UserName = "salman@gmail.com",
                    Email = "salman@gmail.com",
                    EmailConfirmed = true
                };

                _userManager.CreateAsync(user, "Salman@123").GetAwaiter().GetResult();

                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
