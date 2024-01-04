using Comm.Business.src.DTO;
using Comm.Core.src.Entities;

namespace Comm.Business.src.DTOs
{
    public class UserReadDto : BaseEntity
    {
        public Role Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public IEnumerable<AddressReadDto> Addresses { get; set; }
        public AvatarReadDto Avatar { get; set; }
        public string Email { get; set; }
    }

    public class UserCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public List<AddressCreateDto> Addresses { get; set; }
        public AvatarCreateDto Avatar { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public List<AddressCreateDto> Addresses { get; set; }
        public AvatarCreateDto Avatar { get; set; }
        public string Email { get; set; }
    }


}