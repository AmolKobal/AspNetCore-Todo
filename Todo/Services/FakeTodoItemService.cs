using System;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;
using Todo.Models;

namespace AspNetCoreTodo.Services
{
    public class FakeTodoItemService : ITodoItemService
    {
        public Task<bool> AddItemAsync(TodoItem newItem, IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<TodoItem[]> GetIncompleteItemsAsync(IdentityUser user)
        {
            var item1 = new TodoItem {
                Title = "Learn ASP.NET Core",
                DueAt = DateTimeOffset.Now.AddDays(1)
            };

            var item2 = new TodoItem {
                Title = "Build awesome apps",
                DueAt = DateTimeOffset.Now.AddDays(1)
            };

            var item3 = new TodoItem {
                Title = "Have fun coding",
                DueAt = DateTimeOffset.Now.AddDays(1)
            };

            return Task.FromResult (new[] {item1, item2, item3 });

        }

        public Task<bool> MarkDoneAsnc(Guid id, IdentityUser user)
        {
            throw new NotImplementedException();
        }
    }
}