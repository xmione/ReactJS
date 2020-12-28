#if CF
    using EF = ReactJS.Redux.CodeFirst;
    using EFModels = ReactJS.Redux.CodeFirst.Models;
#else
    using EF = ReactJS.Redux.DatabasEFModelsirst;
    using EFModels = ReactJS.Redux.DatabasEFModelsirst.Models;
#endif

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ReactJS.Redux.Repositories
{
    public class SQLPersonRepository : IDataRepository<EFModels.Person>
    {

        readonly EF.RRCContext _context;

        public SQLPersonRepository(EF.RRCContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EFModels.Person>> GetMockData()
        {
            var mockRepo = new MockPersonRepository();

            return await mockRepo.GetAll();
        }
        public async Task<IEnumerable<EFModels.Person>> AddMockData()
        {
            var mockRepo = new MockPersonRepository();
            var mockList = await mockRepo.GetAll();
            var people = _context.People;
            if (people.Count() == 0)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    await _context.People.AddRangeAsync(mockList);
                    await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[People] ON");
                    await _context.SaveChangesAsync();
                    await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[People] OFF");
                    
                    transaction.Commit();
                }
            }
            return mockList;
        }
        public async Task<IEnumerable<EFModels.Person>> GetAll()
        {
            var people = await _context.People.ToListAsync();
            //if (people.Count() == 0)
            //{
            //    people = (List<Person>)await GetMockData();
            //}
            return people;
        }
        public async Task<EFModels.Person> Get(int id)
        {
            return await _context.People.FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<EFModels.Person> Add(EFModels.Person item)
        {
            var result = await _context.AddAsync(item);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<EFModels.Person> Update(EFModels.Person newEntity)
        {
            var result = _context.People.FirstOrDefault(t => t.Id == newEntity.Id);
            if (result != null)
            {
                result.Email = newEntity.Email;
                result.FirstName = newEntity.FirstName;
                result.MiddleName = newEntity.MiddleName;
                result.LastName = newEntity.LastName;
                await _context.SaveChangesAsync();
            }

            return result;
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
    }
}
