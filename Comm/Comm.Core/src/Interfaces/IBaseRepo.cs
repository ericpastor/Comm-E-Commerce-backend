using Comm.Core.src.Entities;
using Comm.Core.src.Parameters;

namespace Comm.Core.src.Interfaces
{
    public interface IBaseRepo<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(GetAllParams getAllParams);
        Task<T> GetByIdAsync(Guid id);
        Task<bool> UpdateOneAsync(T upDateObject);
        Task<bool> DeleteOneAsync(Guid id);
        Task<T> CreateOneAsync(T createObject);
        Task<T?> FindByEmailAsync(string email);
        Task<T?> FindByNameAsync(string name);
    }
}