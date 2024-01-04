using Comm.Business.src.Interfaces;
using Comm.Business.src.DTOs;
using Comm.Core.src.Interfaces;
using AutoMapper;
using Comm.Core.src.Entities;
using Comm.Business.src.Shared;
using Comm.Business.src.Services;
using Comm.Core.src.Parameters;

namespace Comm.Business.src.Service
{
    // este hereda de BaseService y al mismo tiempo implementa a IuserServico
    public class UserService : BaseService<User, UserReadDto, UserCreateDto, UserUpdateDto>, IUserService
    {

        public UserService(IUserRepo repo, IMapper mapper) : base(repo, mapper)
        {
        }

        public async Task<bool> UpdatePasswordAsync(string newPassword, Guid userId)
        {
            var user = await _repo.GetByIdAsync(userId);
            if (user is null)
            {
                throw CustomException.NotFoundException();
            }
            PasswordService.HashPassword(newPassword, out string hashedPassword, out byte[] salt);
            user.Password = hashedPassword;
            user.Salt = salt;
            return await _repo.UpdateOneAsync(user);
        }
        public override async Task<UserReadDto> CreateOneAsync(UserCreateDto createObject)
        {
            if (createObject == null)
            {
                throw new ArgumentNullException(nameof(createObject));
            }
            else if (await _repo.FindByEmailAsync(createObject.Email) != null)
            {
                throw CustomException.ConflictException();
            }
            PasswordService.HashPassword(createObject.Password, out string hashedPassword, out byte[] salt);
            var user = _mapper.Map<UserCreateDto, User>(createObject) ?? throw new InvalidOperationException("Mapping to User failed.");
            user.Password = hashedPassword;
            user.Salt = salt;

            user.Role = Role.Customer;

            var createdUser = await _repo.CreateOneAsync(user);

            if (createdUser == null)
            {
                throw new InvalidOperationException("User creation failed.");
            }
            return _mapper.Map<User, UserReadDto>(createdUser);
        }

        public override async Task<IEnumerable<UserReadDto>> GetAllAsync(GetAllParams getAllParams)
        {
            if (getAllParams.Offset < 0 || getAllParams.Limit < 0)
            {
                throw CustomException.InvalidValueException();
            }
            var users = await _repo.GetAllAsync(getAllParams);
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserReadDto>>(users);
        }

        public override async Task<UserReadDto?> GetByIdAsync(Guid id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user is null)
            {
                throw CustomException.NotFoundException();
            }

            return _mapper.Map<User, UserReadDto>(user);
        }

        public override async Task<bool> UpdateOneAsync(Guid id, UserUpdateDto updateObject)
        {
            var existingUser = await _repo.GetByIdAsync(id);
            if (existingUser is null)
            {
                throw CustomException.NotFoundException();
            }

            _mapper.Map(updateObject, existingUser);

            return await _repo.UpdateOneAsync(existingUser);
        }

        public override async Task<bool> DeleteOneAsync(Guid id)
        {
            var existingUser = await _repo.GetByIdAsync(id);
            if (existingUser is null)
            {
                throw CustomException.NotFoundException();
            }

            return await _repo.DeleteOneAsync(id);
        }
    }
}
