using System.Text;
using AutoMapper;
using Comm.Business.src.DTOs;
using Comm.Business.src.Interfaces;
using Comm.Business.src.Services;
using Comm.Core.src.Entities;
using Comm.Core.src.Interfaces;
using Comm.Core.src.Parameters;
using Moq;

namespace Comm.Test.src
{
    public class ProductServiceTest
    {
        public class ProductServiceTests
        {
            [Fact]
            public async Task CreateOneAsync_WithValidCategory_ShouldReturnProductReadDto()
            {
                // Arrange
                var categoryId = Guid.NewGuid();
                var createObject = new ProductCreateDto
                {
                    CategoryId = categoryId,
                    Title = "Test Product",
                    Description = "Test Description",
                    Price = 10.99m,
                    Images = new List<ImageCreateDto>
                    {
                        new ImageCreateDto
                        {
                            ImageUrl = "http://originalImage.com",
                        }
                    }
                };

                var categoryServiceMock = new Mock<ICategoryService>();
                categoryServiceMock.Setup(service => service.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new CategoryReadDto());


                var repoMock = new Mock<IProductRepo>();
                repoMock.Setup(repo => repo.CreateOneAsync(It.IsAny<Product>())).ReturnsAsync(new Product());

                var mapperMock = new Mock<IMapper>();
                mapperMock.Setup(mapper => mapper.Map<ProductCreateDto, Product>(createObject)).Returns(new Product());
                mapperMock.Setup(mapper => mapper.Map<Product, ProductReadDto>(It.IsAny<Product>(), It.IsAny<Action<IMappingOperationOptions<Product, ProductReadDto>>>()))
                    .Returns(new ProductReadDto());

                var productService = new ProductService(repoMock.Object, mapperMock.Object, categoryServiceMock.Object);

                // Act
                var result = await productService.CreateOneAsync(createObject);

                // Assert
                categoryServiceMock.Verify(service => service.GetByIdAsync(categoryId), Times.Once);
                repoMock.Verify(repo => repo.CreateOneAsync(It.IsAny<Product>()), Times.Once);
                mapperMock.Verify(mapper => mapper.Map<ProductCreateDto, Product>(createObject), Times.Once);
                // mapperMock.Verify(mapper => mapper.Map<Product, ProductReadDto>(It.IsAny<Product>(), It.IsAny<Action<IMappingOperationOptions<Product, ProductReadDto>>>()), Times.Once);

                // Assert.NotNull(result);
                // Assert.IsType<ProductReadDto>(result);
            }
        }

        [Fact]
        public async void GetAllProducts_WithoutParameter_ShouldInvokeRepoMethod()
        {
            var repo = new Mock<IProductRepo>();
            var mapper = new Mock<IMapper>();
            var categoryService = new Mock<ICategoryService>();
            var productService = new ProductService(repo.Object, mapper.Object, categoryService.Object);

            repo.Setup(r => r.GetAllAsync(It.IsAny<GetAllParams>())).ReturnsAsync(new List<Product>());

            await productService.GetAllAsync(new GetAllParams());

            repo.Verify(r => r.GetAllAsync(It.IsAny<GetAllParams>()), Times.Once);
        }

        [Fact]
        public async Task GetProductById_WithValidProductId_ShouldInvokeRepoMethod()
        {
            var repo = new Mock<IProductRepo>();
            var mapper = new Mock<IMapper>();
            var categoryService = new Mock<ICategoryService>();
            var productService = new ProductService(repo.Object, mapper.Object, categoryService.Object);

            var fakeProduct = new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                Title = "Test Product",
                Description = "Test Description",
                Price = 10.99m,
                Images = new List<Image>
                    {
                        new Image
                        {
                            ImageUrl = "http://originalImage.com",
                        }
                    }
            };

            repo.Setup(r => r.GetByIdAsync(fakeProduct.Id)).ReturnsAsync(fakeProduct);

            var result = await productService.GetByIdAsync(fakeProduct.Id);

            repo.Verify(r => r.GetByIdAsync(fakeProduct.Id), Times.Once);
        }

        [Fact]
        public async Task UpdateOneAsync_WithExistingProduct_ShouldReturnTrue()
        {
            var productId = Guid.NewGuid();
            var updateObject = new ProductUpdateDto
            {
                Title = "Updated Product",
                Description = "Updated Description",
                Price = 19.99m,
                Images = new List<ImageCreateDto>
                    {
                        new ImageCreateDto
                        {
                            ImageUrl = "http://originalImage.com",
                        }
                    }
            };

            var repoMock = new Mock<IProductRepo>();
            var mapperMock = new Mock<IMapper>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var productService = new ProductService(repoMock.Object, mapperMock.Object, categoryServiceMock.Object);

            var existingProduct = new Product
            {
                Id = productId,
                Title = "Original Product",
                Description = "Original Description",
                Price = 9.99m,
                Images = new List<Image>
                    {
                        new Image
                        {
                            ImageUrl = "http://originalImage.com",
                        }
                    }
            };

            repoMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(existingProduct);
            repoMock.Setup(repo => repo.UpdateOneAsync(existingProduct)).ReturnsAsync(true);
            mapperMock.Setup(mapper => mapper.Map(updateObject, existingProduct));

            var result = await productService.UpdateOneAsync(productId, updateObject);

            repoMock.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
            mapperMock.Verify(mapper => mapper.Map(updateObject, existingProduct), Times.Once);
            repoMock.Verify(repo => repo.UpdateOneAsync(existingProduct), Times.Once);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteOneAsync_WithExistingProduct_ShouldReturnTrue()
        {
            var productId = Guid.NewGuid();

            var repoMock = new Mock<IProductRepo>();
            var mapperMock = new Mock<IMapper>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var productService = new ProductService(repoMock.Object, mapperMock.Object, categoryServiceMock.Object);

            var existingProduct = new Product
            {
                Id = productId,
                Title = "Original Product",
                Description = "Original Description",
                Price = 9.99m,
                Images = new List<Image>
                    {
                        new Image
                        {
                            ImageUrl = "http://originalImage.com",
                        }
                    }
            };

            repoMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(existingProduct);
            repoMock.Setup(repo => repo.DeleteOneAsync(productId)).ReturnsAsync(true);

            var result = await productService.DeleteOneAsync(productId);

            repoMock.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
            repoMock.Verify(repo => repo.DeleteOneAsync(productId), Times.Once);

            Assert.True(result);
        }



        [Theory]
        [ClassData(typeof(CreateProductTestData))]
        public async Task CreateOneAsync_WithRightData_ReturnsProductReadDto(ProductCreateDto createObject)
        {
            var repoMock = new Mock<IProductRepo>();
            var mapperMock = new Mock<IMapper>();
            var categoryServiceMock = new Mock<ICategoryService>();

            var categoryId = Guid.NewGuid();

            var createdProduct = new Product
            {
                CategoryId = categoryId,
                Title = "Test Product",
                Description = "Test Description",
                Price = 10.99m,
                Images = new List<Image>
                    {
                        new Image
                        {
                            ImageUrl = "http://originalImage.com",
                        }
                    }
            };

            repoMock.Setup(repo => repo.CreateOneAsync(It.IsAny<Product>())).ReturnsAsync(createdProduct);

            mapperMock.Setup(mapper => mapper.Map<ProductCreateDto, Product>(createObject)).Returns(new Product());
            mapperMock.Setup(mapper => mapper.Map<Product, ProductReadDto>(createdProduct, It.IsAny<Action<IMappingOperationOptions<Product, ProductReadDto>>>()))
                .Returns(new ProductReadDto());

            categoryServiceMock.Setup(service => service.GetByIdAsync(categoryId)).ReturnsAsync(new CategoryReadDto());

            var productService = new ProductService(repoMock.Object, mapperMock.Object, categoryServiceMock.Object);

            var result = await productService.CreateOneAsync(createObject);

            // Assert.NotNull(result);
            // Assert.IsType<ProductReadDto>(result);
        }

        public class CreateProductTestData : TheoryData<ProductCreateDto>
        {
            public CreateProductTestData()
            {
                var createObject = new ProductCreateDto
                {
                    CategoryId = Guid.NewGuid(),
                    Title = "Test Product",
                    Description = "Test Description",
                    Price = 10.99m,
                    Images = new List<ImageCreateDto>
                    {
                        new ImageCreateDto
                        {
                            ImageUrl = "http://originalImage.com",
                        }
                    }
                };

                Add(createObject);
            }
        }

        [Theory]
        [ClassData(typeof(GetAllProductsData))]
        public async Task GetAllAsync_WithoutParameter_ReturnValidData(List<ProductReadDto> expectedResult)
        {
            var repo = new Mock<IProductRepo>();
            var mapper = new Mock<IMapper>();
            var categoryService = new Mock<ICategoryService>();
            var productService = new ProductService(repo.Object, mapper.Object, categoryService.Object);

            repo.Setup(repo => repo.GetAllAsync(It.IsAny<GetAllParams>())).ReturnsAsync(new List<Product>());

            var products = await productService.GetAllAsync(new GetAllParams());

            if (expectedResult == null)
            {
                Assert.Null(products);
            }
            else
            {
                Assert.Equal(expectedResult, products.ToList());
            }
        }

        public class GetAllProductsData : TheoryData<List<ProductReadDto>>
        {
            public GetAllProductsData()
            {
                Add(new List<ProductReadDto>());
            }
        }

        [Theory]
        [ClassData(typeof(GetProductByIdData))]
        public async Task GetProductById_WithId_ReturnsProduct(Guid idGuid, Product fakeProduct)
        {
            var repo = new Mock<IProductRepo>();
            var mapper = new Mock<IMapper>();
            var categoryService = new Mock<ICategoryService>();
            var productService = new ProductService(repo.Object, mapper.Object, categoryService.Object);

            var fakeProductReadDto = mapper.Object.Map<ProductReadDto>(fakeProduct);

            repo.Setup(r => r.GetByIdAsync(fakeProduct.Id)).ReturnsAsync(fakeProduct);

            var productFound = await productService.GetByIdAsync(idGuid);

            if (productFound is not null)
            {
                Assert.Contains(fakeProduct, (IEnumerable<Product>)productFound);
            }
        }

        public class GetProductByIdData : TheoryData<Guid, Product>
        {
            public GetProductByIdData()
            {
                var idGuid = new Guid("3fac7300-30f2-0000-0000-e1a639e54100");
                var fakeProduct = new Product
                {
                    Id = idGuid,
                    CategoryId = Guid.NewGuid(),
                    Title = "Test Product",
                    Description = "Test Description",
                    Price = 10.99m,
                    Images = new List<Image>
                    {
                        new Image
                        {
                            ImageUrl = "http://originalImage.com",
                        }
                    }
                };
                Add(idGuid, fakeProduct);
            }
        }
        [Theory]
        [ClassData(typeof(UpdateProductData))]
        public async Task UpdateProduct_WithRightData_ReturnsProduct(Product fakeProductToUpdate, ProductUpdateDto updateObject, bool updateSuccess)
        {
            var repo = new Mock<IProductRepo>();
            var mapper = new Mock<IMapper>();
            var categoryService = new Mock<ICategoryService>();
            var productService = new ProductService(repo.Object, mapper.Object, categoryService.Object);

            mapper.Setup(m => m.Map<Product>(updateObject)).Returns(fakeProductToUpdate);

            repo.Setup(r => r.GetByIdAsync(fakeProductToUpdate.Id)).ReturnsAsync(fakeProductToUpdate);

            if (updateSuccess)
            {
                repo.Setup(r => r.UpdateOneAsync(fakeProductToUpdate)).ReturnsAsync(true);
            }
            else
            {
                repo.Setup(r => r.UpdateOneAsync(fakeProductToUpdate)).ReturnsAsync(false);
            }

            var productUpdated = await productService.UpdateOneAsync(fakeProductToUpdate.Id, updateObject);

            if (updateSuccess)
            {
                Assert.True(productUpdated);
            }
            else
            {
                Assert.False(productUpdated);
            }
        }

        public class UpdateProductData : TheoryData<Product, ProductUpdateDto, bool>
        {
            public UpdateProductData()
            {
                var idGuid = new Guid("3fac7300-30f2-0000-0000-e1a639e54100");
                var fakeProductToUpdate = new Product
                {
                    Id = idGuid,
                    CategoryId = Guid.NewGuid(),
                    Title = "Original Product",
                    Description = "Original Description",
                    Price = 9.99m,
                    Images = new List<Image>
                    {
                        new Image
                        {
                            ImageUrl = "http://originalImage.com",
                        }
                    }
                };
                var updateObject = new ProductUpdateDto
                {
                    Title = "Updated Product",
                    Description = "Updated Description",
                    Price = 19.99m,
                    Images = new List<ImageCreateDto>
                    {
                        new ImageCreateDto
                        {
                            ImageUrl = "http://originalImage.com",
                        }
                    }
                };
                Add(fakeProductToUpdate, updateObject, true);
            }
        }

        [Theory]
        [ClassData(typeof(RemoveProductData))]
        public async Task RemoveProduct_WithRightData_ReturnsProduct(Product productToRemove, bool success)
        {
            var repo = new Mock<IProductRepo>();
            var mapper = new Mock<IMapper>();
            var categoryService = new Mock<ICategoryService>();
            var productService = new ProductService(repo.Object, mapper.Object, categoryService.Object);

            repo.Setup(repo => repo.GetByIdAsync(productToRemove.Id)).ReturnsAsync(productToRemove);
            repo.Setup(repo => repo.DeleteOneAsync(productToRemove.Id)).ReturnsAsync(success);

            var result = await productService.DeleteOneAsync(productToRemove.Id);
            var productAfterDelete = await productService.GetByIdAsync(productToRemove.Id);

            Assert.True(success);
        }

        public class RemoveProductData : TheoryData<Product, bool>
        {
            public RemoveProductData()
            {
                var productId = Guid.NewGuid();
                var productToRemove = new Product
                {
                    Id = productId,
                    CategoryId = Guid.NewGuid(),
                    Title = "Original Product",
                    Description = "Original Description",
                    Price = 9.99m,
                    Images = new List<Image>
                    {
                        new Image
                        {
                            ImageUrl = "http://originalImage.com",
                        }
                    }
                };
                Add(productToRemove, true);
            }
        }


    }
}