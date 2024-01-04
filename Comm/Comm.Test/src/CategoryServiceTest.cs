using System.Text;
using AutoMapper;
using Comm.Business.src.DTOs;
using Comm.Business.src.Services;
using Comm.Core.src.Entities;
using Comm.Core.src.Interfaces;
using Comm.Core.src.Parameters;
using Moq;

namespace Comm.Test.src
{
    public class CategoryServiceTest
    {
        [Fact]
        public async void GetAllCategories_WithoutParameter_ShouldInvokeRepoMethod()
        {
            var repo = new Mock<ICategoryRepo>();
            var mapper = new Mock<IMapper>();
            var categoryService = new CategoryService(repo.Object, mapper.Object);

            repo.Setup(r => r.GetAllAsync(It.IsAny<GetAllParams>())).ReturnsAsync(new List<Category>());

            await categoryService.GetAllAsync(new GetAllParams());

            repo.Verify(r => r.GetAllAsync(It.IsAny<GetAllParams>()), Times.Once);
        }

        [Fact]
        public async Task GetCategoryById_WithValidCategoryId_ShouldInvokeRepoMethod()
        {
            var repo = new Mock<ICategoryRepo>();
            var mapper = new Mock<IMapper>();
            var categoryService = new CategoryService(repo.Object, mapper.Object);

            var fakeCategory = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Test Category",
                CategoryImage = new CategoryImage { CategoryImageUrl = "http://testCategoryImage.com"},
            };

            repo.Setup(r => r.GetByIdAsync(fakeCategory.Id)).ReturnsAsync(fakeCategory);

            var result = await categoryService.GetByIdAsync(fakeCategory.Id);

            repo.Verify(r => r.GetByIdAsync(fakeCategory.Id), Times.Once);
        }

        [Fact]
        public async Task UpdateOneAsync_WithExistingCategory_ShouldReturnTrue()
        {
            var categoryId = Guid.NewGuid();
            var updateObject = new CategoryUpdateDto
            {
                Name = "Updated Category",
                CategoryImage = new CategoryImageCreateDto { CategoryImageUrl = "http://testCategoryImage.com"},

            };

            var repoMock = new Mock<ICategoryRepo>();
            var mapperMock = new Mock<IMapper>();
            var categoryService = new CategoryService(repoMock.Object, mapperMock.Object);

            var existingCategory = new Category
            {
                Id = categoryId,
                Name = "Original Category",
                CategoryImage = new CategoryImage { CategoryImageUrl = "http://testCategoryImage.com"},

            };

            repoMock.Setup(repo => repo.GetByIdAsync(categoryId)).ReturnsAsync(existingCategory);
            repoMock.Setup(repo => repo.UpdateOneAsync(existingCategory)).ReturnsAsync(true);
            mapperMock.Setup(mapper => mapper.Map(updateObject, existingCategory));

            var result = await categoryService.UpdateOneAsync(categoryId, updateObject);

            repoMock.Verify(repo => repo.GetByIdAsync(categoryId), Times.Once);
            mapperMock.Verify(mapper => mapper.Map(updateObject, existingCategory), Times.Once);
            repoMock.Verify(repo => repo.UpdateOneAsync(existingCategory), Times.Once);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteOneAsync_WithExistingCategory_ShouldReturnTrue()
        {
            var categoryId = Guid.NewGuid();

            var repoMock = new Mock<ICategoryRepo>();
            var mapperMock = new Mock<IMapper>();
            var categoryService = new CategoryService(repoMock.Object, mapperMock.Object);

            var existingCategory = new Category
            {
                Id = categoryId,
                Name = "Original Category",
                CategoryImage = new CategoryImage { CategoryImageUrl = "http://testCategoryImage.com"},

            };

            repoMock.Setup(repo => repo.GetByIdAsync(categoryId)).ReturnsAsync(existingCategory);
            repoMock.Setup(repo => repo.DeleteOneAsync(categoryId)).ReturnsAsync(true);

            var result = await categoryService.DeleteOneAsync(categoryId);

            repoMock.Verify(repo => repo.GetByIdAsync(categoryId), Times.Once);
            repoMock.Verify(repo => repo.DeleteOneAsync(categoryId), Times.Once);

            Assert.True(result);
        }


        [Theory]
        [ClassData(typeof(GetAllCategoriesData))]
        public async Task GetAllAsync_WithoutParameter_ReturnValidData(List<CategoryReadDto> expectedResult)
        {
            var repo = new Mock<ICategoryRepo>();
            var mapper = new Mock<IMapper>();
            var categoryService = new CategoryService(repo.Object, mapper.Object);

            repo.Setup(repo => repo.GetAllAsync(It.IsAny<GetAllParams>())).ReturnsAsync(new List<Category>());

            var categories = await categoryService.GetAllAsync(new GetAllParams());

            if (expectedResult == null)
            {
                Assert.Null(categories);
            }
            else
            {
                Assert.Equal(expectedResult, categories.ToList());
            }
        }

        public class GetAllCategoriesData : TheoryData<List<CategoryReadDto>>
        {
            public GetAllCategoriesData()
            {
                Add(new List<CategoryReadDto>());
            }
        }

        [Theory]
        [ClassData(typeof(GetCategoryByIdData))]
        public async Task GetCategoryById_WithId_ReturnsCategory(Guid idGuid, Category fakeCategory)
        {
            var repo = new Mock<ICategoryRepo>();
            var mapper = new Mock<IMapper>();
            var categoryService = new CategoryService(repo.Object, mapper.Object);

            var fakeCategoryReadDto = mapper.Object.Map<CategoryReadDto>(fakeCategory);

            repo.Setup(r => r.GetByIdAsync(fakeCategory.Id)).ReturnsAsync(fakeCategory);

            var categoryFound = await categoryService.GetByIdAsync(idGuid);

            if (categoryFound is not null)
            {
                Assert.Contains(fakeCategory, (IEnumerable<Category>)categoryFound);
            }
        }

        public class GetCategoryByIdData : TheoryData<Guid, Category>
        {
            public GetCategoryByIdData()
            {
                var idGuid = new Guid("3fac7300-30f2-0000-0000-e1a639e54100");
                var fakeCategory = new Category
                {
                    Id = idGuid,
                    Name = "Test Category",
                    CategoryImage = new CategoryImage { CategoryImageUrl = "http://testCategoryImage.com"},

                };
                Add(idGuid, fakeCategory);
            }
        }

        [Theory]
        [ClassData(typeof(UpdateCategoryData))]
        public async Task UpdateCategory_WithRightData_ReturnsCategory(Category fakeCategoryToUpdate, CategoryUpdateDto updateObject, bool updateSuccess)
        {
            var repo = new Mock<ICategoryRepo>();
            var mapper = new Mock<IMapper>();
            var categoryService = new CategoryService(repo.Object, mapper.Object);

            mapper.Setup(m => m.Map<Category>(updateObject)).Returns(fakeCategoryToUpdate);

            repo.Setup(r => r.GetByIdAsync(fakeCategoryToUpdate.Id)).ReturnsAsync(fakeCategoryToUpdate);

            if (updateSuccess)
            {
                repo.Setup(r => r.UpdateOneAsync(fakeCategoryToUpdate)).ReturnsAsync(true);
            }
            else
            {
                repo.Setup(r => r.UpdateOneAsync(fakeCategoryToUpdate)).ReturnsAsync(false);
            }

            var categoryUpdated = await categoryService.UpdateOneAsync(fakeCategoryToUpdate.Id, updateObject);

            if (updateSuccess)
            {
                Assert.True(categoryUpdated);
            }
            else
            {
                Assert.False(categoryUpdated);
            }
        }

        public class UpdateCategoryData : TheoryData<Category, CategoryUpdateDto, bool>
        {
            public UpdateCategoryData()
            {
                var idGuid = new Guid("3fac7300-30f2-0000-0000-e1a639e54100");
                var fakeCategoryToUpdate = new Category
                {
                    Id = idGuid,
                    Name = "Original Category",
                    CategoryImage = new CategoryImage { CategoryImageUrl = "http://testCategoryImage.com"},

                };
                var updateObject = new CategoryUpdateDto
                {
                    Name = "Updated Category",
                    CategoryImage = new CategoryImageCreateDto { CategoryImageUrl = "http://testCategoryImage.com"},

                };
                Add(fakeCategoryToUpdate, updateObject, true);
            }
        }
        [Theory]
        [ClassData(typeof(RemoveCategoryData))]
        public async Task RemoveCategory_WithRightData_ReturnsCategory(Category categoryToRemove, bool success)
        {
            var repo = new Mock<ICategoryRepo>();
            var mapper = new Mock<IMapper>();
            var categoryService = new CategoryService(repo.Object, mapper.Object);

            repo.Setup(repo => repo.GetByIdAsync(categoryToRemove.Id)).ReturnsAsync(categoryToRemove);
            repo.Setup(repo => repo.DeleteOneAsync(categoryToRemove.Id)).ReturnsAsync(success);

            var result = await categoryService.DeleteOneAsync(categoryToRemove.Id);
            var categoryAfterDelete = await categoryService.GetByIdAsync(categoryToRemove.Id);

            Assert.True(success);
            Assert.Null(categoryAfterDelete);
        }

        public class RemoveCategoryData : TheoryData<Category, bool>
        {
            public RemoveCategoryData()
            {
                var categoryId = Guid.NewGuid();
                var categoryToRemove = new Category
                {
                    Id = categoryId,
                    Name = "Original Category",
                    CategoryImage = new CategoryImage { CategoryImageUrl = "http://testCategoryImage.com"},

                };
                Add(categoryToRemove, true);
            }
        }
    }
}