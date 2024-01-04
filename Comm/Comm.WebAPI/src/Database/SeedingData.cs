using Comm.Business.src.Shared;
using Comm.Core.src.Entities;

namespace Comm.WebAPI.src.Database
{
    public class SeedingData
    {

        public static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new()
                {
                Id = Guid.Parse("2e7b8212-d5e4-4bb9-a876-1f49919eb244"),
                Title = "Wireless Noise-Canceling Headphones",
                Description = "Immerse yourself in your favorite music with these high-quality headphones",
                Price = 249.99m,
                CategoryId = Guid.Parse("2e7b8212-d5e4-4bb9-a876-1f49919eb243"),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
                },
                new()
                {
                Id = Guid.Parse("8d8e5c27-41bf-4f2e-9c42-5c6e3b4b3633"),
                Title = "4K Ultra HD Smart TV",
                Description = "High-resolution smart television with vibrant colors",
                Price = 1200.00m,
                CategoryId = Guid.Parse("8d8e5c27-41bf-4f2e-9c42-5c6e3b4b3611"),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
                }
            };
        }



        public static List<Image> GetImages()
        {
            return new List<Image>
        {
            new Image
            {
                Id = Guid.Parse("8d8e5c27-41bf-4f2e-9c42-5c6e3b4b3631"),
                ProductId = Guid.Parse("8d8e5c27-41bf-4f2e-9c42-5c6e3b4b3633"),
                ImageUrl = "https://res.cloudinary.com/deghpnzo2/image/upload/v1703960944/dc8hgpwomgngiuhyntg4.jpg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
            new Image
            {
                Id = Guid.Parse("8d8e5c27-41bf-4f2e-9c42-5c6e3b4b3632"),
                ProductId =  Guid.Parse("8d8e5c27-41bf-4f2e-9c42-5c6e3b4b3633"),
                ImageUrl = "https://res.cloudinary.com/deghpnzo2/image/upload/v1703960944/dc8hgpwomgngiuhyntg4.jpg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
            new Image
            {
                Id = Guid.Parse("8d8e5c27-41bf-4f2e-9c42-5c6e3b4b3633"),
                ProductId =  Guid.Parse("8d8e5c27-41bf-4f2e-9c42-5c6e3b4b3633"),
                ImageUrl = "https://res.cloudinary.com/deghpnzo2/image/upload/v1703960944/dc8hgpwomgngiuhyntg4.jpg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
            new Image
            {
                Id = Guid.Parse("8d8e5c27-41bf-4f2e-9c42-5c6e3b4b3634"),
                ProductId = Guid.Parse("2e7b8212-d5e4-4bb9-a876-1f49919eb244"),
                ImageUrl = "https://res.cloudinary.com/deghpnzo2/image/upload/v1704168315/1577722054635_qlstro.jpg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
            new Image
            {
                Id = Guid.Parse("8d8e5c27-41bf-4f2e-9c42-5c6e3b4b3635"),
                ProductId = Guid.Parse("2e7b8212-d5e4-4bb9-a876-1f49919eb244"),
                ImageUrl = "https://res.cloudinary.com/deghpnzo2/image/upload/v1704168315/1577722054635_qlstro.jpg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
            new Image
            {
                Id = Guid.Parse("8d8e5c27-41bf-4f2e-9c42-5c6e3b4b3636"),
                ProductId = Guid.Parse("2e7b8212-d5e4-4bb9-a876-1f49919eb244"),
                ImageUrl = "https://res.cloudinary.com/deghpnzo2/image/upload/v1704168315/1577722054635_qlstro.jpg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
        };
        }



        public static List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category
            {
                Id = Guid.Parse("8d8e5c27-41bf-4f2e-9c42-5c6e3b4b3611"),
                Name = "Fenders",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
            new Category
            {
                Id = Guid.Parse("2e7b8212-d5e4-4bb9-a876-1f49919eb243"),
                Name = "Electronics",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
            new Category
            {
                Id = Guid.Parse("c19d8c72-5cc4-4d01-9ef6-c9abdb07e438"),
                Name = "Safety Equipment",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
            new Category
            {
                Id = Guid.Parse("f4f78665-682d-4173-b3f5-1c0d6a142162"),
                Name = "Shoes",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
            new Category
            {
                Id = Guid.Parse("a9e5a5bf-9a2c-4c66-9183-8c4e51c788d1"),
                Name = "Toys & Games",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            }
            };
        }

        public static List<CategoryImage> GetCategoryImages()
        {
            return new List<CategoryImage>{
             new CategoryImage
            {
                Id = Guid.Parse("0a1f53c0-38b9-4fb3-9b80-2c9f80d3a4ab"),
                CategoryId = Guid.Parse("8d8e5c27-41bf-4f2e-9c42-5c6e3b4b3611"),
                CategoryImageUrl = "https://res.cloudinary.com/deghpnzo2/image/upload/v1703960944/dc8hgpwomgngiuhyntg4.jpg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
            new CategoryImage
            {
                Id = Guid.Parse("3e9d9d56-85a8-4a0a-9cc9-07f40e847a95"),
                CategoryId = Guid.Parse("2e7b8212-d5e4-4bb9-a876-1f49919eb243"),
                CategoryImageUrl = "https://res.cloudinary.com/deghpnzo2/image/upload/v1704168315/1577722054635_qlstro.jpg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
            new CategoryImage
            {
                Id = Guid.Parse("6c3d2902-65f5-42eb-9c0e-3512ee24d21e"),
                CategoryId = Guid.Parse("c19d8c72-5cc4-4d01-9ef6-c9abdb07e438"),
                CategoryImageUrl = "https://res.cloudinary.com/deghpnzo2/image/upload/v1704168374/1577560802648_lj4yym.jpg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
            new CategoryImage
            {
                Id = Guid.Parse("91aef4fb-503c-4a22-afed-3947110c0c9d"),
                CategoryId = Guid.Parse("f4f78665-682d-4173-b3f5-1c0d6a142162"),
                CategoryImageUrl = "https://res.cloudinary.com/deghpnzo2/image/upload/v1704168375/20161005_164922_-_copia_za0hs8.jpg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
            new CategoryImage
            {
                Id = Guid.Parse("bd238e9e-2e61-4f68-a5fb-14b4ce87f91e"),
                CategoryId = Guid.Parse("a9e5a5bf-9a2c-4c66-9183-8c4e51c788d1"),
                CategoryImageUrl = "https://res.cloudinary.com/deghpnzo2/image/upload/v1704168373/1577560804638_egjwro.jpg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            }
            };
        }

        public static List<User> GetUsers()
        {
            PasswordService.HashPassword("eric1234", out string hp, out byte[] salt);
            return new List<User>
            {
                new()
                {
                    Id = Guid.Parse("b7f6f4ea-4b90-47b8-9e36-045c01d6c984"),
                    FirstName = "Eric",
                    LastName = "Pas",
                    Email = "eric@mail.com",
                    Role = Role.Admin,
                    Phone = "+34111111111",
                    Password = hp,
                    Salt = salt,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                }
            };
        }

        public static List<Address> GetAddresses()
        {
            return new List<Address>
            {
                new Address
                {
                    Id = Guid.Parse("79e0c72c-38db-4e7b-b5cb-9d6b9f2de7ac"),
                    UserId = Guid.Parse("b7f6f4ea-4b90-47b8-9e36-045c01d6c984"),
                    HouseNumber = 32,
                    Street = "Vilanova",
                    PostCode = "08032",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                }
            };
        }

        public static List<Avatar> GetAvatars()
        {
            return new List<Avatar>
            {
                new Avatar
                {
                     Id = Guid.Parse("e44123b8-8c0b-4c90-b395-5f37d92b097f"),
                     UserId = Guid.Parse("b7f6f4ea-4b90-47b8-9e36-045c01d6c984"),
                     AvatarUrl = "https://res.cloudinary.com/deghpnzo2/image/upload/v1703960944/dc8hgpwomgngiuhyntg4.jpg",
                     CreatedAt = DateTime.Now,
                     UpdatedAt = DateTime.Now,
                }
            };
        }
    }
}

