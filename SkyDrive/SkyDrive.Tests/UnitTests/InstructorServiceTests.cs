using Moq;
using SkyDrive.BLL.Exceptions;
using SkyDrive.BLL.Models;
using SkyDrive.BLL.Services;
using SkyDrive.DAL.Entities;
using SkyDrive.DAL.Interfaces;

namespace SkyDrive.Tests.UnitTests
{
    public class InstructorServiceTests
    {
        private readonly Mock<IInstructorRepository> _mockRepository;

        public InstructorServiceTests()
        {
            _mockRepository = new Mock<IInstructorRepository>();
        }

        [Fact]
        public async Task GetAllInstructors_ReturnListOfInstructors()
        {
            //Arrange
            var listOfInstructorModels = new List<InstructorModel>()
            {
                new()
                {
                    Id = 1,
                    FirstName= "FirstName",
                    LastName= "LastName",
                    MiddleName= "MiddleName",
                    Experience= 2,
                    Events = null
                },
                new()
                {
                    Id = 2,
                    FirstName= "FirstName 2",
                    LastName= "LastName 2",
                    MiddleName= "MiddleName 2",
                    Experience= 7,
                    Events = null
                }
            };
            var listOfInstructorEntities = new List<InstructorEntity>
            {
                new()
                {
                    Id = 1,
                    FirstName= "FirstName",
                    LastName= "LastName",
                    MiddleName= "MiddleName",
                    Experience= 2,
                    Events = null
                },
                new()
                {
                    Id = 2,
                    FirstName= "FirstName 2",
                    LastName= "LastName 2",
                    MiddleName= "MiddleName 2",
                    Experience= 7,
                    Events = null
                }
            };

            _mockRepository.Setup(x => x.GetAll(CancellationToken.None)).ReturnsAsync(listOfInstructorEntities);
            var service = new InstructorService(_mockRepository.Object);

            //Act
            var result = await service.GetAllInstructors(CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetAll(CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(result.Count(), listOfInstructorModels.Count);
        }

        [Fact]
        public async Task GetAllInstructors_ReturnEmptyList()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetAll(CancellationToken.None)).ReturnsAsync(new List<InstructorEntity>());
            var service = new InstructorService(_mockRepository.Object);

            //Act
            var result = await service.GetAllInstructors(CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetAll(CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetInstructorById_CorrectId_ReturnInstructorModel()
        {
            //Arrange
            var instructorEntity = new InstructorEntity
            {
                Id = 2,
                FirstName = "FirstName 2",
                LastName = "LastName 2",
                MiddleName = "MiddleName 2",
                Experience = 7,
                Events = null
            };

            _mockRepository.Setup(x => x.GetById(2, CancellationToken.None)).ReturnsAsync(instructorEntity);
            var service = new InstructorService(_mockRepository.Object);

            //Act
            var result = await service.GetInstructorById(2, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(2, CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<InstructorModel>(result);
        }

        [Fact]
        public async Task GetInstructorById_IncorrectId_ReturnNull()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(2, CancellationToken.None)).ReturnsAsync(value: null);
            var service = new InstructorService(_mockRepository.Object);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => service.GetInstructorById(2, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(2, CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("Instructor with id: 2 not found", result.Message);
        }

        [Fact]
        public async Task CreateInstructor_CorrectModel_ReturnEventModel()
        {
            //Arrange
            var instructorModel = new InstructorModel
            {
                Id = 0,
                FirstName = "FirstName 2",
                LastName = "LastName 2",
                MiddleName = "MiddleName 2",
                Experience = 7,
                Events = null
            };
            var instructorEntity = new InstructorEntity
            {
                Id = 0,
                FirstName = "FirstName 2",
                LastName = "LastName 2",
                MiddleName = "MiddleName 2",
                Experience = 7,
                Events = null
            };

            _mockRepository.Setup(x => x.Create(It.IsAny<InstructorEntity>(), CancellationToken.None)).ReturnsAsync(value: instructorEntity);
            var service = new InstructorService(_mockRepository.Object);

            //Act
            var result = await service.CreateInstructor(instructorModel, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.Create(It.IsAny<InstructorEntity>(), CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<InstructorModel>(result);
        }

        [Fact]
        public async Task UpdateInstructor_CorrectModel_ReturnInstructorModel()
        {
            //Arrange
            var instructorModel = new InstructorModel
            {
                Id = 3,
                FirstName = "FirstName 2",
                LastName = "LastName 2",
                MiddleName = "MiddleName 2",
                Experience = 7,
                Events = null
            };
            var instructorEntity = new InstructorEntity
            {
                Id = 3,
                FirstName = "FirstName 2",
                LastName = "LastName 2",
                MiddleName = "MiddleName 2",
                Experience = 7,
                Events = null
            };

            _mockRepository.Setup(x => x.GetById(3, CancellationToken.None)).ReturnsAsync(instructorEntity);
            _mockRepository.Setup(x => x.Update(It.IsAny<InstructorEntity>(), CancellationToken.None)).ReturnsAsync(instructorEntity);
            var service = new InstructorService(_mockRepository.Object);

            //Act
            var result = await service.UpdateInstructor(instructorModel, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(3, CancellationToken.None), Times.Once);
            _mockRepository.Verify(x => x.Update(It.IsAny<InstructorEntity>(), CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<InstructorModel>(result);
        }

        [Fact]
        public async Task UpdateInstructor_IncorrectModel_ReturnEntityNotFoundException()
        {
            //Arrange
            var instructorModel = new InstructorModel
            {
                Id = 3,
                FirstName = "FirstName 2",
                LastName = "LastName 2",
                MiddleName = "MiddleName 2",
                Experience = 7,
                Events = null
            };

            _mockRepository.Setup(x => x.GetById(3, CancellationToken.None)).ReturnsAsync(value: null);
            var service = new InstructorService(_mockRepository.Object);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => service.UpdateInstructor(instructorModel, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(3, CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("Instructor with id: 3 not found", result.Message);
        }

        [Fact]
        public async Task DeleteInstructor_CorrectId_ReturnNull()
        {
            //Arrange
            var instructorEntity = new InstructorEntity
            {
                Id = 3,
                FirstName = "FirstName 2",
                LastName = "LastName 2",
                MiddleName = "MiddleName 2",
                Experience = 7,
                Events = null
            };

            _mockRepository.Setup(x => x.GetById(3, CancellationToken.None)).ReturnsAsync(instructorEntity);
            _mockRepository.Setup(x => x.Delete(It.IsAny<InstructorEntity>(), CancellationToken.None));
            var service = new InstructorService(_mockRepository.Object);

            //Act
            await service.DeleteInstructor(3, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(3, CancellationToken.None), Times.Once);
            _mockRepository.Verify(x => x.Delete(It.IsAny<InstructorEntity>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task DeleteInstructor_IncorrectId_ReturnArgumentNullException()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(3, CancellationToken.None)).ReturnsAsync(value: null);
            var service = new InstructorService(_mockRepository.Object);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => service.DeleteInstructor(3, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(3, CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("Instructor with id: 3 not found", result.Message);
        }
    }
}
