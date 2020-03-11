using System.Threading.Tasks;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Services
{
    public interface ITodoService
    {
        Task<TodoItem[]> GetIncompleteItemsAsync();
    }
}