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
        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            return await _context.TodoItems.ToListAsync();
        }
        public async Task<TodoItem> Get(int id)
        {
            return await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<TodoItem> Add(TodoItem todoItem)
        {
            var result = await _context.AddAsync(todoItem);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async void Delete(int id)
        {
            var result = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            
            if (result != null)
            {
                _context.TodoItems.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<TodoItem> Update(TodoItem newTodoItem)
        {
            var result = _context.TodoItems.FirstOrDefault(t => t.Id == newTodoItem.Id);
            if (result != null)
            {
                result.IsComplete = newTodoItem.IsComplete;
                result.Name = newTodoItem.Name;
                await _context.SaveChangesAsync();
            }
            
            return result;
        }
    }
}
