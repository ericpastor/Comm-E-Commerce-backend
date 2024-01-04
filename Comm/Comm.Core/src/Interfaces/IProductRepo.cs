using Comm.Core.src.Entities;

namespace Comm.Core.src.Interfaces
{
    public interface IProductRepo : IBaseRepo<Product>
    {
        Task<Product?> FindByEmailAsync(string email);
    }
}