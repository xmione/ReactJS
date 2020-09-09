using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class MockTodoItemRepository : ITodoItemRepository
    {
        private List<TodoItem> _todoItemList;
        public MockTodoItemRepository()
        {
            _todoItemList = new List<TodoItem>();
            _todoItemList.Add(new TodoItem() { Id = 1, IsComplete = true, Name = "Name1" }) ;
            _todoItemList.Add(new TodoItem() { Id = 2, IsComplete = false, Name = "Name2" });
            _todoItemList.Add(new TodoItem() { Id = 3, IsComplete = true, Name = "Name3" });
            _todoItemList.Add(new TodoItem() { Id = 4, IsComplete = false, Name = "Name4" });
        }
        public async Task<TodoItem> Add(TodoItem todoItem)
        {
            await Task.Run(() => 
            {
                todoItem.Id = _todoItemList.Max(e => e.Id) + 1;
                _todoItemList.Add(todoItem);
            });

            return todoItem;
        }

        public void Delete(int id)
        {
            TodoItem todoItem = _todoItemList.FirstOrDefault(t => t.Id == id);
            if (todoItem != null)
            {
                //found, delete
                _todoItemList.Remove(todoItem);
            }
            
        }

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            List<TodoItem> list = null;
            await Task.Run(() => 
            {
                list = _todoItemList;
            });

            return list;
        }

        public async Task<TodoItem> Get(int id)
        {
            TodoItem todoItem = null;
            await Task.Run(() => 
            {
                todoItem = _todoItemList.FirstOrDefault(t => t.Id == id);
            });
            
            return todoItem;
        }

        public async Task<TodoItem> Update(TodoItem newTodoItem)
        {
            TodoItem todoItem = null;
            await Task.Run(() => 
            {
                todoItem = _todoItemList.FirstOrDefault(t => t.Id == newTodoItem.Id);
                if (todoItem != null)
                {
                    //found, update
                    todoItem.IsComplete = newTodoItem.IsComplete;
                    todoItem.Name = newTodoItem.Name;
                }
            });
            
            return todoItem;
        }
    }
}
