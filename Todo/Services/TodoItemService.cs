using System;
using System.Linq;
using System.Threading.Tasks;
using Todo.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Services
{
    public class TodoItemService : ITodoItemService
    {        
        private const int _defaultDueDays = 3;
        private readonly ApplicationDbContext _context;
    
        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddItemAsync(TodoItem newItem, IdentityUser user)
        {
            newItem.Id = Guid.NewGuid();
            newItem.IsDone = false;
            newItem.UserID = user.Id;

            if(newItem.DueAt == null)
            {
                newItem.DueAt = DateTimeOffset.Now.AddDays(_defaultDueDays);
            }
            
            _context.Items.Add(newItem);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<TodoItem[]> GetIncompleteItemsAsync(IdentityUser user)
        {
            return await _context.Items
                .Where(x => x.IsDone == false && x.UserID == user.Id)
                .ToArrayAsync();
        }

        public async Task<bool> MarkDoneAsnc(Guid id, IdentityUser user)
        {
            var item = await _context.Items
                .Where(x => x.Id == id & x.UserID == user.Id)
                .SingleOrDefaultAsync();

            if(item == null)
            {
                return false;
            }

            item.IsDone = true;

            var saveResult = await _context.SaveChangesAsync();

            return saveResult == 1;

        }
    }
}