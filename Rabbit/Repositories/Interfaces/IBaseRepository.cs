using Rabbit.Db.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Rabbit.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        bool Any();
        Task<T> GetByIdAsync(long id);
        T GetById(long id);
        Task<List<T>> GetAllAsync();
        void Add(T model);
        Task AddAsync(T model);
        Task<bool> TryAddAsync(T model);
        void AddList(List<T> models);
        Task AddListAsync(List<T> models);
        Task UpdateAsync(T model);
        void UpdateRange(IEnumerable<T> models);
        Task DeleteAsync(T model);
        Task DeleteByIdAsync(long id);
        int Count();
        bool Exists(long id);
    }
}
