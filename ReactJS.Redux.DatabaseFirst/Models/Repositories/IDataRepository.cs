using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactJS.Redux.DatabaseFirst.Models.Repositories
{
    public interface IDataRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(int id);
        Task<TEntity> Add(TEntity entity);
        Task<IEnumerable<TEntity>> AddMockData();
        Task<TEntity> Update(TEntity newEntity);
        void Delete(int id);
    }
}
