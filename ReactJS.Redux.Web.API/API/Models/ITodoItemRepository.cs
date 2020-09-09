using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public interface ITodoItemRepository
    {
        Task<IEnumerable<TodoItem>> GetAll();
        Task<TodoItem> Get(int id);
        Task<TodoItem> Add(TodoItem todoItem);
        Task<TodoItem> Update(TodoItem todoItem);
        void Delete(int id);
    }
}
