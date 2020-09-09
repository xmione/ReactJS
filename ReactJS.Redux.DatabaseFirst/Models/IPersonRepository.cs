using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactJS.Redux.DatabaseFirst.Models
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAll();
        Task<Person> Get(int id);
        Task<Person> Add(Person todoItem);
        Task<Person> Update(Person todoItem);
        void Delete(int id);
    }
}
