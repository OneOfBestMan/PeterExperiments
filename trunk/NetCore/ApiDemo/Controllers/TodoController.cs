using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDemo.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : Controller
    {
        private readonly TodoContext _todoContext;

        public TodoController(TodoContext todoContext)
        {
            _todoContext = todoContext;
            if (_todoContext.TodoItems.Count()==0)
            {
                _todoContext.TodoItems.Add(new TodoItem() { Name="Item1"});
                _todoContext.SaveChanges();
            }
        }
        /// <summary>
        /// 获取所有待做名单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll()
        {
            return _todoContext.TodoItems.ToList();
        }

        /// <summary>
        /// 根据Id获取一个待做
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItem> GetByid(long id)
        {
            var item = _todoContext.TodoItems.Find(id);
            if (item==null)
            {
                return NotFound();
            }
            return item;
        }

        /// <summary>
        /// 创建一个待做
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<TodoItem> Create(TodoItem item)
        {
            _todoContext.TodoItems.Add(item);
            _todoContext.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        /// <summary>
        /// 更新一个待做
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(long id, TodoItem item)
        {
            var todo = _todoContext.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _todoContext.TodoItems.Update(todo);
            _todoContext.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// 删除一个待做
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _todoContext.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _todoContext.TodoItems.Remove(todo);
            _todoContext.SaveChanges();
            return NoContent();
        }
    }
}
