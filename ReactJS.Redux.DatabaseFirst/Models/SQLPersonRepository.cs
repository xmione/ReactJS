using Microsoft.EntityFrameworkCore;
using ReactJS.Redux.DatabaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class SQLPersonRepository : IPersonRepository
    {
        private readonly RRCContext _context;

        public SQLPersonRepository(RRCContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Person>> GetMockData()
        {
            var mockRepo = new MockPersonRepository();

            return await mockRepo.GetAll();
        }
        public async Task<IEnumerable<Person>> GetAll()
        {
            var people = _context.People.ToListAsync().Result;
            if (people.Count() == 0)
            {
                people = (List<Person>)await GetMockData();
            }

            return people;
        }
        public async Task<Person> Get(int id)
        {
            return await _context.People.FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<Person> Add(Person item)
        {
            var result = await _context.AddAsync(item);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async void Delete(int id)
        {
            var result = _context.People.FirstOrDefault(t => t.Id == id);
            
            if (result != null)
            {
                _context.People.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Person> Update(Person newItem)
        {
            var result = _context.People.FirstOrDefault(t => t.Id == newItem.Id);
            if (result != null)
            {
                result.Email = newItem.Email;
                result.FirstName = newItem.FirstName;
                result.MiddleName = newItem.MiddleName;
                result.LastName = newItem.LastName;
                await _context.SaveChangesAsync();
            }
            
            return result;
        }
    }
}
