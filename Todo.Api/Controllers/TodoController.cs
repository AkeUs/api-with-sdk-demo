using Microsoft.AspNetCore.Mvc;
using Todo.Api.Models;
using Todo.Api.Repositories;

namespace Todo.Api.Controllers {
    
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class TodoController : ControllerBase {

        private readonly TodoRepository _repository;

        public TodoController(TodoRepository repository) {
            _repository = repository;
        }
        
        
        [HttpGet]
        public IActionResult GetTodos() {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTodoById(int id) {
            if (id < 1) {
                return BadRequest();
            }

            var todo = _repository.Get(id);

            if (todo == null) {
                return NotFound();
            }
            
            return Ok(todo);
        }

        [HttpPost]
        public IActionResult AddTodo(NewTodo request) {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            
            var result = _repository.Add(request);

            return CreatedAtAction(
                nameof(GetTodoById),
                new { id = result.Id },
                result
            );
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateTodo(int id, Models.Todo request) {
            if (!ModelState.IsValid ||
                request.Id != id) {
                return BadRequest(ModelState);
            }

            var todo = _repository.Get(id);

            if (todo == null) {
                return NotFound();
            }

            _repository.Update(request);

            return NoContent();
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult DeleteTodo(int id) {
            if (id < 1) {
                return BadRequest();
            }
            
            var todo = _repository.Get(id);

            if (todo == null) {
                return NotFound();
            }

            _repository.Remove(id);
            
            return NoContent();
        }
    }
}
