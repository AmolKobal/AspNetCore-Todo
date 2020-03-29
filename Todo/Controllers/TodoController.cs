using Microsoft.AspNetCore.Mvc;
using Todo.Models;
using Todo.Services;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Todo.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly SignInManager<IdentityUser> _signInManager;
        public TodoController(ITodoItemService todoItemService,
                              UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager)
        {
            _todoItemService = todoItemService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {            
            var currentUser = await _userManager.GetUserAsync(User);
            
            if(currentUser == null)
            {
                return Challenge();
            }

            var items = await _todoItemService.GetIncompleteItemsAsync(currentUser);

            var model = new TodoViewModel {
                Items = items,
            };

            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(TodoItem newItem)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Index");
                //return View("Index", items);
            }

            var currentUser = await _userManager.GetUserAsync(User);

            var successful = await _todoItemService.AddItemAsync(newItem, currentUser);
            if(!successful)
            {
                return BadRequest($"Could not add Item {newItem.Title}");
            }

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if(id == Guid.Empty)
            {
                RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);

            var successful = await _todoItemService.MarkDoneAsnc(id, currentUser);
            if(!successful)
            {
                return BadRequest("Could not mark item as done!");
            }

            return RedirectToAction("Index");
        }
    }
}