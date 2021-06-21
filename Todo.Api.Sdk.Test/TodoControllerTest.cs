using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Todo.Api.Sdk.Contracts;
using Xunit;

namespace Todo.Api.Sdk.Test {
    
    public class TodoControllerTest {

        private static ITodoClient GetTodoClient() {
            var services = new ServiceCollection();
            services.AddTodoClient(new TodoClientOptions {
                Url = "http://localhost:5000",
                Timeout = TimeSpan.FromSeconds(30)
            });
            var provider = services.BuildServiceProvider();
            return provider.GetService<ITodoClient>();
        }

        [Fact]
        public async void CreateTodoTest() {
            var client = GetTodoClient();
            var result = await client.CreateTodo("example for create");
            Assert.Equal("example for create", result.Description);
        }

        [Fact]
        public async void UpdateTodoTest() {
            var client = GetTodoClient();
            var result = await client.CreateTodo("example for update");
            Assert.False(result.Completed);

            result.Completed = true;
            var isUpdated = await client.UpdateTodo(result);
            Assert.True(isUpdated);
        }

        [Fact]
        public async void DeleteTodoTest() {
            var client = GetTodoClient();
            var result = await client.CreateTodo("example for delete");
            Assert.False(result.Completed);

            var isDeleted = await client.DeleteTodo(result);
            Assert.True(isDeleted);
        }
        
        [Fact]
        public async void GetTodoByIdTest() {
            var client = GetTodoClient();
            var result = await client.CreateTodo("example for get by id");
            
            var todo = await client.GetTodoById(result.Id);
            Assert.Equal(result.Description, todo.Description);
        }
        
        [Fact]
        public async void GetTodosTest() {
            var client = GetTodoClient();
            var results = await client.GetTodos();
            Assert.NotNull(results);
        }
    }
}
