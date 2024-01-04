using Comm.Core.src.Entities;

namespace Comm.Business.src.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}