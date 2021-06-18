using System;
using System.Collections.Generic;
using System.Linq;

namespace Todo.Api.Repositories {
    public class TodoRepository {

        private readonly List<Models.Todo> _todos;
        
        public TodoRepository() {
            _todos = new List<Models.Todo>();
        }

        public Models.Todo Add(Models.NewTodo todo) {
            var maxId = 0;
            if (_todos.Any()) {
                maxId = _todos.Max(item => item.Id);
            }
            
            var newItem = new Models.Todo {
                Id = maxId + 1,
                Description = todo.Description,
                Completed = false
            };
            
            _todos.Add(newItem);
            
            return newItem;
        }

        public void Update(Models.Todo todo) {
            var result = _todos.Find(item => item.Id == todo.Id);

            if (result == null) {
                throw new Exception("Error on update");
            }
            
            _todos.Remove(result);

            result.Description = todo.Description;
            result.Completed = todo.Completed;
            _todos.Add(result);
        }

        public void Remove(int id) {
            var todo = _todos.Find(item => item.Id == id);
            _todos.Remove(todo);
        }

        public Models.Todo Get(int id) {
            return _todos.Find(item => item.Id == id);
        }

        public List<Models.Todo> GetAll() {
            return _todos.OrderBy(item => item.Id).ToList();
        }
        
    }
}