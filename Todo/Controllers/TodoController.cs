using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Models;
using System;

namespace AspNetCoreTodo.Controllers
{
    public class TodoController : Controller
    {
        public IActionResult Index()
        {
            TodoItem item = new TodoItem();
            item.Title = "First Todo";
            item.IsDone = false;
            item.DueAt = DateTime.Now;

            return Content(item.Title);
        }
    }
}