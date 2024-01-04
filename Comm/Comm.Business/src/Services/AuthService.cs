using Comm.Business.src.Interfaces;
using Comm.Business.src.Shared;
using Comm.Core.src.Interfaces;
using Comm.Core.src.Parameters;

namespace Comm.Business.src.Services
{
    public class AuthService : IAuthService
    {
        private IUserRepo _repo; // a esto le llaman injection que necesita el ctor de abajo
        private ITokenService _tokenService;
        public AuthService(IUserRepo userRepo, ITokenService tokenService)
        {
            _repo = userRepo;
            _tokenService = tokenService;
        }
        public async Task<string> Login(Credentials credentials)
        {
            var foundEmail = await _repo.FindByEmailAsync(credentials.Email);
            if (foundEmail is null)
            {
                throw CustomException.NotFoundException();
            }
            var isPasswordMatch = PasswordService.VerifyPassword(credentials.Password, foundEmail.Password, foundEmail.Salt);
            if (isPasswordMatch)
            {
                return _tokenService.GenerateToken(foundEmail);
            }
            throw CustomException.NotFoundException();
        }
    }
}