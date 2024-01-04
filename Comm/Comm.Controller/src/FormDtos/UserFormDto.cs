using Comm.Business.src.DTO;
using Comm.Business.src.DTOs;
using Comm.Core.src.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Comm.Controller.src.Controllers
{
    public class UserCreateFormDto
    {
        [FromForm(Name = "firstname")]
        public string FirstName { get; set; }

        [FromForm(Name = "lastname")]
        public string LastName { get; set; }

        [FromForm(Name = "phone")]
        public string Phone { get; set; }

        [FromForm(Name = "addresses")]
        public List<AddressCreateDto> Addresses { get; set; }

        [FromForm(Name = "avatar")]
        public AvatarCreateDto Avatar { get; set; }

        [FromForm(Name = "email")]
        public string Email { get; set; }

        [FromForm(Name = "password")]
        public string Password { get; set; }
    }

    public class UserUpdateFormDto
    {
        [FromForm(Name = "firstname")]
        public string FirstName { get; set; }

        [FromForm(Name = "lastname")]
        public string LastName { get; set; }

        [FromForm(Name = "phone")]
        public string Phone { get; set; }

        [FromForm(Name = "addresses")]
        public List<AddressCreateDto> Addresses { get; set; }

        [FromForm(Name = "avatar")]
        public AvatarCreateDto Avatar { get; set; }

        [FromForm(Name = "email")]
        public string Email { get; set; }
    }
}