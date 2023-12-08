using APICreationPractice.Models;
using Microsoft.AspNetCore.Mvc;

namespace APICreationPractice.Controllers
{
    public class TodoController : ControllerBase
    {
        private static List<TodoItem> todos = new List<TodoItem>
        {
            new TodoItem { Id = 1, Title = "Do something", IsCompleted = false },
            new TodoItem { Id = 2, Title = "Do something else", IsCompleted = true },
            new TodoItem { Id = 3, Title = "Do third thing", IsCompleted = true }
        };

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get()
        {
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> Get (int id)
        {
            var todo = todos.Find(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TodoItem todo)
        {
            todo.Id = todos.Count + 1;
            todos.Add(todo);
            return CreatedAtAction(nameof(Get), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TodoItem updatedTodo)
        {
            var todo = todos.Find(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Title = updatedTodo.Title;
            todo.IsCompleted = updatedTodo.IsCompleted;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = todos.Find(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            todos.Remove(todo);
            return NoContent();
        }
    }
}
