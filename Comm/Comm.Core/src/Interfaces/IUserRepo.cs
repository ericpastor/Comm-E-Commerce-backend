using Comm.Core.src.Entities;

namespace Comm.Core.src.Interfaces
{
    public interface IUserRepo : IBaseRepo<User>
    {
        // Task<bool> UpdatePasswordAsync(string newPassword, Guid userId);
        Task<User?> FindByEmailAsync(string email);
    }
}