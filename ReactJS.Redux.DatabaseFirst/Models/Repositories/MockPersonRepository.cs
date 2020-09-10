using ReactJS.Redux.DatabaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactJS.Redux.DatabaseFirst.Models.Repositories
{
    public class MockPersonRepository : IDataRepository<Person>
    {
        private List<Person> _list;
        public MockPersonRepository()
        {
            _list = new List<Person>();
            _list.Add(new Person { Id = 1, FirstName = "Cynthia", MiddleName = "KungFu", LastName = "Luster", Email = "cynthialuster@test.com" }) ;
            _list.Add(new Person { Id = 2, FirstName = "Bat", MiddleName = "Na", LastName = "Man", Email = "batman@test.com" });
            _list.Add(new Person { Id = 3, FirstName = "Spider", MiddleName = "Web", LastName = "Man", Email = "spiderman@test.com" });
            _list.Add(new Person { Id = 4, FirstName = "Super", MiddleName = "Dooper", LastName = "Man", Email = "superman@test.com" });
        }
        public async Task<Person> Add(Person item)
        {
            await Task.Run(() => 
            {
                item.Id = _list.Max(e => e.Id) + 1;
                _list.Add(item);
            });

            return item;
        }

        public void Delete(int id)
        {
            Person item = _list.FirstOrDefault(t => t.Id == id);
            if (item!= null)
            {
                //found, delete
                _list.Remove(item);
            }
            
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            List<Person> list = null;
            await Task.Run(() => 
            {
                list = _list;
            });

            return list;
        }

        public async Task<Person> Get(int id)
        {
            Person item = null;
            await Task.Run(() => 
            {
                item = _list.FirstOrDefault(t => t.Id == id);
            });
            
            return item;
        }

        public async Task<Person> Update(Person newItem)
        {
            Person item = null;
            await Task.Run(() => 
            {
                item = _list.FirstOrDefault(t => t.Id == newItem.Id);
                if (item != null)
                {
                    //found, update
                    item.Email = newItem.Email;
                    item.FirstName = newItem.FirstName;
                    item.MiddleName = newItem.MiddleName;
                    item.LastName = newItem.LastName;
                }
            });
            
            return item;
        }

        public Task<Person> Update(Person sourceEntity, Person targtEntity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Person entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>> AddMockData()
        {
            throw new NotImplementedException();
        }
    }
}
