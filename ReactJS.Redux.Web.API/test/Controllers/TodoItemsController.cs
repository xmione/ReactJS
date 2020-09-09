using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using test.Models;

namespace test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoItemsController : ControllerBase
    {

        public TodoItemsController()
        {
        }

        [HttpGet]
        public IEnumerable<TodoItem> Get() 
        {
            List<TodoItem> todoItemList = null;
            
            using (var context = new TodoContext())
            {
                var todoItem = new TodoItem();
                todoItem.IsComplete = false;
                todoItem.Name = "sol";
                
                context.TodoItems.Add(todoItem);
                context.SaveChanges();
                todoItemList = context.TodoItems.ToList();
            }

            return todoItemList;
        }
    }
}
