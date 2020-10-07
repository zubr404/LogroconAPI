using System.Threading.Tasks;

namespace LogroconAPI.Services
{
    public interface IDtoService<T> where T : class
    {
        Task<T> Get(int id);
        Task<T> Create(T item);
        Task Update(T item);
        Task<bool> Delete(int id);
    }
}
