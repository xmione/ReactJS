#if CF
    using EF = ReactJS.Redux.CodeFirst.Models;
#else
    using EF = ReactJS.Redux.DatabaseFirst.Models;
#endif


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactJS.Redux.Repositories
{

    public class MockPersonRepository : IDataRepository<EF.Person>
    {
        private List<EF.Person> _list;
        public MockPersonRepository()
        {
            _list = new List<EF.Person>();
            _list.Add(new EF.Person { Id = 1, FirstName = "Cynthia", MiddleName = "KungFu", LastName = "Luster", Email = "cynthialuster@test.com" }) ;
            _list.Add(new EF.Person { Id = 2, FirstName = "Bat", MiddleName = "Na", LastName = "Man", Email = "batman@test.com" });
            _list.Add(new EF.Person { Id = 3, FirstName = "Spider", MiddleName = "Web", LastName = "Man", Email = "spiderman@test.com" });
            _list.Add(new EF.Person { Id = 4, FirstName = "Super", MiddleName = "Dooper", LastName = "Man", Email = "superman@test.com" });
        }
        public async Task<EF.Person> Add(EF.Person item)
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
            EF.Person item = _list.FirstOrDefault(t => t.Id == id);
            if (item!= null)
            {
                //found, delete
                _list.Remove(item);
            }
            
        }

        public async Task<IEnumerable<EF.Person>> GetAll()
        {
            List<EF.Person> list = null;
            await Task.Run(() => 
            {
                list = _list;
            });

            return list;
        }

        public async Task<EF.Person> Get(int id)
        {
            EF.Person item = null;
            await Task.Run(() => 
            {
                item = _list.FirstOrDefault(t => t.Id == id);
            });
            
            return item;
        }

        public async Task<EF.Person> Update(EF.Person newItem)
        {
            EF.Person item = null;
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

        public Task<EF.Person> Update(EF.Person sourceEntity, EF.Person targtEntity)
        {
            throw new NotImplementedException();
        }

        public void Delete(EF.Person entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EF.Person>> AddMockData()
        {
            throw new NotImplementedException();
        }
    }
}
