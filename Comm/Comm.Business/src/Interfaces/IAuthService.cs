using Comm.Core.src.Parameters;

namespace Comm.Business.src.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(Credentials credentials);
    }
}