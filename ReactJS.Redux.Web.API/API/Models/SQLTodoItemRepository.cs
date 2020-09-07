using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class SQLTodoItemRepository : ITodoItemRepository
    {
        private readonly TodoContext _context;

        public SQLTodoItemRepository(TodoContext context)
        {
            this._context = context;
        }
        public TodoItem Add(TodoItem todoItem)
        {
            _context.Add(todoItem);
            _context.SaveChanges();

            return todoItem;
        }

        public TodoItem Delete(int id)
        {
            var todoItem = _context.TodoItems.Find(id);
            if (todoItem != null)
            {
                _context.TodoItems.Remove(todoItem);
                _context.SaveChanges();
            }

            return todoItem;
        }

        public IEnumerable<TodoItem> GetAllTodoItems()
        {
            return _context.TodoItems;
        }

        public TodoItem GetTodoItem(int id)
        {
            return _context.TodoItems.FirstOrDefault(t => t.Id == id);
        }

        public TodoItem Update(TodoItem newTodoItem)
        {
            var todoItem = _context.TodoItems.Attach(newTodoItem);
            todoItem.State = EntityState.Modified;
            _context.SaveChanges();

            return newTodoItem;
        }
    }
}
