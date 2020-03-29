using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Todo;
using Todo.Data;
using Todo.Services;
using Microsoft.AspNetCore.Identity;
using Todo.Models;
using System.IO;

namespace Todo.UnitTests
{
    public class TodoItemServicesShould
    {
        [Fact]
        public async Task AddNewItemAsIncompleteWithDueDate()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().
                UseInMemoryDatabase(databaseName: "Test_Todo").Options;

            using(var context = new ApplicationDbContext(options))
            {
                var service = new TodoItemService(context);

                var fakeUser = new IdentityUser {
                    UserName = "amolk@task.com"
                };

                await service.AddItemAsync(
                    new TodoItem {
                        Title = "Testing",
                    }, 
                    fakeUser);
            }

            using(var context = new ApplicationDbContext(options))
            {
                var items = await context.Items.CountAsync();

                Assert.Equal(1, items);

                
                var item = await context.Items.FirstAsync();
                Assert.Equal("Testing", item.Title);
                Assert.Equal(false, item.IsDone);

                // Item should be due 3 days from now (give or take a second)
                File.AppendAllText("log.txt", $"DueAt: {item.DueAt.ToString()}");
                var difference = DateTimeOffset.Now.AddDays(3) - item.DueAt;
                File.AppendAllText("log.txt", $"Difference: {difference}, TimeSpan: {TimeSpan.FromSeconds(1)}");
                Assert.True(difference < TimeSpan.FromSeconds(1));                 
            }

        }
    }
}
