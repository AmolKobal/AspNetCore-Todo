using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using System.Threading.Tasks;
using System;

namespace AspNetCoreTodo.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;
        private const int _defaultDueDays = 3;
        public TodoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _todoItemService.GetIncompleteItemsAsync();

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

            if(newItem.DueAt == null)
            {
                newItem.DueAt = DateTimeOffset.Now.AddDays(_defaultDueDays);
            }

            var successful = await _todoItemService.AddItemAsync(newItem);
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

            var successful = await _todoItemService.MarkDoneAsnc(id);
            if(!successful)
            {
                return BadRequest("Could not mark item as done!");
            }

            return RedirectToAction("Index");
        }
    }
}