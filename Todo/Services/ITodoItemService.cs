using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Todo.Models;

namespace Todo.Services
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetIncompleteItemsAsync(IdentityUser user);
        Task<bool> AddItemAsync(TodoItem newItem, IdentityUser user);
        Task<bool> MarkDoneAsnc(Guid id, IdentityUser user);
    }
}