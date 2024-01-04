using Comm.Core.src.Entities;

namespace Comm.Core.src.Interfaces
{
    public interface ICategoryRepo : IBaseRepo<Category>
    {
        Task<Category?> FindByNameAsync(string name);
    }
}