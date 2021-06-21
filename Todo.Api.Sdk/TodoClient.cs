using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Todo.Api.Sdk.Contracts;
using Todo.Api.Sdk.Models;

namespace Todo.Api.Sdk {

    public class TodoClient : ITodoClient {
        
        private readonly HttpClient _client;

        public TodoClient(HttpClient client) {
            _client = client;
        }

        public async Task<IEnumerable<TodoItem>> GetTodos() {
            var response = await _client.GetAsync("/todo");
            
            if (!response.IsSuccessStatusCode) {
                throw new Exception("Error on get results");
            }

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<TodoItem>>(result);
        }

        public async Task<TodoItem> GetTodoById(int id) {
            var response = await _client.GetAsync($"/todo/{id}");
            
            if (!response.IsSuccessStatusCode) {
                throw new Exception("Error on get results");
            }

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoItem>(result);
        }

        public async Task<TodoItem> CreateTodo(string description) {
            var request = JsonConvert.SerializeObject(
                new CreateTodoRequest {
                        Description = description
                });

            var response = await _client.PostAsync("/todo",
                new StringContent(request, Encoding.UTF8, "application/json")
            );
            
            if (!response.IsSuccessStatusCode) {
                throw new Exception("Error on create todo");
            }
            
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoItem>(result);
        }

        public async Task<bool> UpdateTodo(TodoItem todo) {
            var request = JsonConvert.SerializeObject(todo);

            var response = await _client.PutAsync($"todo/{todo.Id}", 
                new StringContent(request, Encoding.UTF8, "application/json")
            );
            
            if (!response.IsSuccessStatusCode) {
                throw new Exception("Error on update todo");
            }

            return true;
        }

        public async Task<bool> DeleteTodo(TodoItem todo) {
            var response = await _client.DeleteAsync($"todo/{todo.Id}");
            
            if (!response.IsSuccessStatusCode) {
                throw new Exception("Error on delete todo");
            }

            return true;
        }
    }
}
