using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogroconAPI.Repositories
{
    interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<T> Create(T item, List<int> navigationsId);
        Task Update(T item);
        Task<bool> Delete(int id);
    }
}
