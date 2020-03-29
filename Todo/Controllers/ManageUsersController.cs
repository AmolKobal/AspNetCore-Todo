using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Controllers
{
    [Authorize/*(Roles = "Administrator")*/]
    public class ManageUsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ManageUsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var admins = new IdentityUser[0];

            try{
                admins = (await _userManager.GetUsersInRoleAsync(Constants.AdministratorRole))
                .ToArray();
            }
            catch{

            }
            
            var everone = await _userManager.Users.ToArrayAsync();

            var model = new ManageUsersViewModel {
                Administrators = admins,
                Everyone = everone
            };

            return View(model);
        }
    }
}