using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public interface ITodoItemRepository
    {
        TodoItem GetTodoItem(int id);
        IEnumerable<TodoItem> GetAllTodoItems();
        TodoItem Add(TodoItem todoItem);
        TodoItem Update(TodoItem todoItem);
        TodoItem Delete(int id);
    }
}
