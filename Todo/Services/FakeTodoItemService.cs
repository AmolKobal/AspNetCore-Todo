using System;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Services
{
    public class FakeTodoItemService : ITodoItemService
    {
        public Task<bool> AddItemAsync(TodoItem newItem)
        {
            throw new NotImplementedException();
        }

        public Task<TodoItem[]> GetIncompleteItemsAsync()
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

        public Task<bool> MarkDoneAsnc(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}