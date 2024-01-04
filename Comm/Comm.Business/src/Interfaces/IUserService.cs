using Comm.Business.src.DTOs;
using Comm.Core.src.Entities;

namespace Comm.Business.src.Interfaces
{
    public interface IUserService : IBaseService<User, UserReadDto, UserCreateDto, UserUpdateDto>
    {
        Task<bool> UpdatePasswordAsync(string newPassword, Guid userId);
    }
}