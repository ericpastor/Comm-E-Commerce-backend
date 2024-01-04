using System.Text;
using AutoMapper;
using Comm.Business.src.DTO;
using Comm.Business.src.DTOs;
using Comm.Business.src.Service;
using Comm.Business.src.Shared;
using Comm.Core.src.Entities;
using Comm.Core.src.Interfaces;
using Comm.Core.src.Parameters;
using Moq;

namespace Comm.Test.src
{
    public class UserServiceTest
    {
        [Fact]
        public async Task UpdatePasswordAsync_WithExistingUser_ShouldReturnTrue()
        {
            var userId = Guid.NewGuid();
            var newPassword = "newPassword";

            var repoMock = new Mock<IUserRepo>();
            var userService = new UserService(repoMock.Object, Mock.Of<IMapper>());

            var existingUser = new User
            {
                Id = userId,
                FirstName = "Ernst",
                LastName = "Air",
                Phone = "+346666666",
                Addresses = new List<Address>
                {
                    new Address
                    {
                        HouseNumber = 32,
                        Street = "Vilanova",
                        PostCode = "08032"
                    }
                },
                Avatar = new Avatar {AvatarUrl = "http://ernst.com" },

                Email = "ernst@mail.com",
                Password = newPassword
            };

            repoMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(existingUser);

            var hashedPassword = "hashedPassword";
            var salt = new byte[] { 1, 2, 3 };
            PasswordService.HashPassword(newPassword, out hashedPassword, out salt);

            repoMock.Setup(repo => repo.UpdateOneAsync(existingUser)).ReturnsAsync(true);

            var result = await userService.UpdatePasswordAsync(newPassword, userId);

            repoMock.Verify(repo => repo.GetByIdAsync(userId), Times.Once);
            repoMock.Verify(repo => repo.UpdateOneAsync(existingUser), Times.Once);

            Assert.True(result);
        }



        [Fact]
        public async void GetAllUsers_WithoutParameter_ShouldInvokeRepoMethod()
        {
            var repo = new Mock<IUserRepo>();
            var mapper = new Mock<IMapper>();
            var userService = new UserService(repo.Object, mapper.Object);

            repo.Setup(repo => repo.GetAllAsync(It.IsAny<GetAllParams>())).ReturnsAsync(new List<User>());

            await userService.GetAllAsync(new GetAllParams());

            repo.Verify(repo => repo.GetAllAsync(It.IsAny<GetAllParams>()), Times.Once);
        }


        [Fact]
        public async Task GetUserByUserId_WithValidUserId_ShouldInvokeRepoMethod()
        {
            var repo = new Mock<IUserRepo>();
            var mapper = new Mock<IMapper>();
            var userService = new UserService(repo.Object, mapper.Object);
            var fakeUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Ernst",
                LastName = "Air",
                Phone = "+346666666",
                Addresses = new List<Address>
                {
                    new Address
                    {
                        HouseNumber = 32,
                        Street = "Vilanova",
                        PostCode = "08032"
                    }
                },
                Avatar = new Avatar { AvatarUrl = "http://ernst.com" },

                Email = "ernst@mail.com",
                Password = "ernst1234"
            };
            repo.Setup(r => r.GetByIdAsync(fakeUser.Id)).ReturnsAsync(fakeUser);

            var result = await userService.GetByIdAsync(fakeUser.Id);

            repo.Verify(r => r.GetByIdAsync(fakeUser.Id), Times.Once);
        }

        [Fact]
        public async Task UpdateOneAsync_WithExistingUser_ShouldReturnTrue()
        {
            var userId = Guid.NewGuid();
            var updateObject = new UserUpdateDto
            {
                FirstName = "",
                LastName = "",
                Phone = "+34777777777",
                Addresses = new List<AddressCreateDto>
                {
                    new AddressCreateDto
                    {
                        HouseNumber = 32,
                        Street = "Vilanova",
                        PostCode = "08032"
                    }
                },
                Avatar = new AvatarCreateDto {AvatarUrl = "http://ernst.com" },

                Email = "ernst@mail.com",
            };

            var repoMock = new Mock<IUserRepo>();
            var mapperMock = new Mock<IMapper>();
            var userService = new UserService(repoMock.Object, mapperMock.Object);

            var existingUser = new User
            {
                Id = userId,
            };

            repoMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(existingUser);
            repoMock.Setup(repo => repo.UpdateOneAsync(existingUser)).ReturnsAsync(true);
            mapperMock.Setup(mapper => mapper.Map(updateObject, existingUser));

            var result = await userService.UpdateOneAsync(userId, updateObject);

            repoMock.Verify(repo => repo.GetByIdAsync(userId), Times.Once);
            mapperMock.Verify(mapper => mapper.Map(updateObject, existingUser), Times.Once);
            repoMock.Verify(repo => repo.UpdateOneAsync(existingUser), Times.Once);

            Assert.True(result);
        }


        [Fact]
        public async Task DeleteOneAsync_WithExistingUser_ShouldReturnTrue()
        {
            var userId = Guid.NewGuid();

            var repoMock = new Mock<IUserRepo>();
            var userService = new UserService(repoMock.Object, Mock.Of<IMapper>());

            var existingUser = new User
            {
                Id = userId,
                FirstName = "Ernst",
                LastName = "Air",
                Phone = "+346666666",
                Addresses = new List<Address>
                {
                    new Address
                    {
                        HouseNumber = 32,
                        Street = "Vilanova",
                        PostCode = "08032"
                    }
                },
                Avatar = new Avatar {AvatarUrl = "http://ernst.com" },

                Email = "ernst@mail.com"
            };

            repoMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(existingUser);
            repoMock.Setup(repo => repo.DeleteOneAsync(userId)).ReturnsAsync(true);

            var result = await userService.DeleteOneAsync(userId);

            repoMock.Verify(repo => repo.GetByIdAsync(userId), Times.Once);
            repoMock.Verify(repo => repo.DeleteOneAsync(userId), Times.Once);

            Assert.True(result);
        }


        [Theory]
        [ClassData(typeof(GetAllUsersData))]
        public async Task GetAllAsync_WithoutParameter_ReturnValidData(List<UserReadDto> expectedResult)
        {
            var repo = new Mock<IUserRepo>();
            var mapper = new Mock<IMapper>();
            var userService = new UserService(repo.Object, mapper.Object);

            repo.Setup(repo => repo.GetAllAsync(It.IsAny<GetAllParams>())).ReturnsAsync(new List<User>());

            var users = await userService.GetAllAsync(new GetAllParams());

            if (expectedResult == null)
            {
                Assert.Null(users);
            }
            else
            {
                Assert.Equal(expectedResult, users.ToList());
            }
        }

        public class GetAllUsersData : TheoryData<List<UserReadDto>>
        {
            public GetAllUsersData()
            {
                Add(new List<UserReadDto>());
            }
        }

        [Theory]
        [ClassData(typeof(GetUserByIdData))]
        public async Task GetUserById_WithId_ReturnsUser(Guid idGuid, User fakeUser)
        {
            var repo = new Mock<IUserRepo>();
            var mapper = new Mock<IMapper>();
            var userService = new UserService(repo.Object, mapper.Object);

            var fakeUserReadDto = mapper.Object.Map<UserReadDto>(fakeUser);

            repo.Setup(r => r.GetByIdAsync(fakeUser.Id)).ReturnsAsync(fakeUser);

            var userFound = await userService.GetByIdAsync(idGuid);

            if (userFound is not null)
            {
                Assert.Contains(fakeUser, (IEnumerable<User>)userFound);
            }
        }

        public class GetUserByIdData : TheoryData<Guid, User>
        {

            public GetUserByIdData()
            {
                var idGuid = new Guid("3fac7300-30f2-0000-0000-e1a639e54100");
                var fakeUser = new User
                {
                    Id = idGuid,
                    FirstName = "Ernst",
                    LastName = "Air",
                    Phone = "+346666666",
                    Addresses = new List<Address>
                {
                    new Address
                    {
                        HouseNumber = 32,
                        Street = "Vilanova",
                        PostCode = "08032"
                    }
                },
                    Avatar = new Avatar {AvatarUrl = "http://ernst.com" },

                    Email = "ernst@mail.com",
                    Password = "ernst1234"
                };
                Add(idGuid, fakeUser);
            }
        }

        [Theory]
        [ClassData(typeof(UpdateUserData))]
        public async Task UpdateUser_WithRightData_ReturnsUser(User fakeUserToUpdate, UserUpdateDto updateObject, bool updateSuccess)
        {
            var repo = new Mock<IUserRepo>();
            var mapper = new Mock<IMapper>();
            var userService = new UserService(repo.Object, mapper.Object);

            mapper.Setup(m => m.Map<User>(updateObject)).Returns(fakeUserToUpdate);

            repo.Setup(r => r.GetByIdAsync(fakeUserToUpdate.Id)).ReturnsAsync(fakeUserToUpdate);

            if (updateSuccess)
            {
                repo.Setup(r => r.UpdateOneAsync(fakeUserToUpdate)).ReturnsAsync(true);
            }
            else
            {
                repo.Setup(r => r.UpdateOneAsync(fakeUserToUpdate)).ReturnsAsync(false);
            }

            var userUpdated = await userService.UpdateOneAsync(fakeUserToUpdate.Id, updateObject);

            if (updateSuccess)
            {
                Assert.True(userUpdated);
            }
            else
            {
                Assert.False(userUpdated);
            }

        }

        public class UpdateUserData : TheoryData<User, UserUpdateDto, bool>
        {
            public UpdateUserData()
            {
                var idGuid = new Guid("3fac7300-30f2-0000-0000-e1a639e54100");
                var fakeUserToUpdate = new User
                {
                    Id = idGuid,
                    FirstName = "Ernst",
                    LastName = "Air",
                    Phone = "+346666666",
                    Addresses = new List<Address>
                {
                    new Address
                    {
                        HouseNumber = 32,
                        Street = "Vilanova",
                        PostCode = "08032"
                    }
                },
                    Avatar = new Avatar {AvatarUrl = "http://ernst.com" },
                    Email = "ernst@mail.com",
                    Password = "ernst1234"
                };
                var updateObject = new UserUpdateDto
                {
                    FirstName = "",
                    LastName = "",
                    Phone = "+34777777777",
                    Addresses = new List<AddressCreateDto>
                {
                    new AddressCreateDto
                    {
                        HouseNumber = 32,
                        Street = "Vilanova",
                        PostCode = "08032"
                    }
                },
                    Avatar = new AvatarCreateDto {AvatarUrl = "http://ernst.com" },
                    Email = "ernst@mail.com",
                };
                Add(fakeUserToUpdate, updateObject, true);
            }
        }

        [Theory]
        [ClassData(typeof(RemoveUserData))]
        public async Task RemoveUser_WithRightData_ReturnsUser(User userToRemove, bool success)
        {
            var repo = new Mock<IUserRepo>();
            var mapper = new Mock<IMapper>();
            var userService = new UserService(repo.Object, mapper.Object);

            repo.Setup(repo => repo.GetByIdAsync(userToRemove.Id)).ReturnsAsync(userToRemove);
            repo.Setup(repo => repo.DeleteOneAsync(userToRemove.Id)).ReturnsAsync(true);

            var result = await userService.DeleteOneAsync(userToRemove.Id);
            var userAfterDelete = await userService.GetByIdAsync(userToRemove.Id);

            var userToRemoveReadDto = mapper.Object.Map<UserReadDto>(userToRemove);
            Assert.True(success);
        }

        public class RemoveUserData : TheoryData<User, bool>
        {

            public RemoveUserData()
            {
                var userId = Guid.NewGuid();
                var userToRemove = new User
                {
                    Id = userId,
                    FirstName = "Ernst",
                    LastName = "Air",
                    Phone = "+346666666",
                    Addresses = new List<Address>
                {
                    new Address
                    {
                        HouseNumber = 32,
                        Street = "Vilanova",
                        PostCode = "08032"
                    }
                },
                    Avatar = new Avatar {AvatarUrl = "http://ernst.com" },

                    Email = "ernst@mail.com"
                };
                Add(userToRemove, true);
            }
        }
    }
}