using AutoFixture;
using FluentAssertions;
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
        private readonly Fixture _fixture;

        public InstructorServiceTests()
        {
            _mockRepository = new Mock<IInstructorRepository>();

            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetAllInstructors_ReturnListOfInstructors()
        {
            //Arrange
            var listOfInstructorModels = _fixture.Create<List<InstructorModel>>();
            var listOfInstructorEntities = _fixture.Create<List<InstructorEntity>>();

            _mockRepository.Setup(x => x.GetAll(CancellationToken.None)).ReturnsAsync(listOfInstructorEntities);

            var service = new InstructorService(_mockRepository.Object);

            //Act
            var result = await service.GetAllInstructors(CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetAll(CancellationToken.None), Times.Once);

            result.Should().NotBeNullOrEmpty().And.HaveSameCount(listOfInstructorModels);
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

            result.Should().NotBeNull().And.BeEmpty();
        }

        [Fact]
        public async Task GetInstructorById_CorrectId_ReturnInstructorModel()
        {
            //Arrange
            var instructorEntity = _fixture.Create<InstructorEntity>();

            _mockRepository.Setup(x => x.GetById(2, CancellationToken.None)).ReturnsAsync(instructorEntity);

            var service = new InstructorService(_mockRepository.Object);

            //Act
            var result = await service.GetInstructorById(2, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(2, CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeOfType<InstructorModel>();
        }

        [Fact]
        public async Task GetInstructorById_IncorrectId_ReturnNull()
        {
            //Arrange
            var inputId = _fixture.Create<int>();

            _mockRepository.Setup(x => x.GetById(inputId, CancellationToken.None)).ReturnsAsync(value: null);

            var service = new InstructorService(_mockRepository.Object);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => service.GetInstructorById(inputId, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(inputId, CancellationToken.None), Times.Once);

            result.Should().NotBeNull();

            Assert.Equal($"Instructor with id: {inputId} not found", result.Message);
        }

        [Fact]
        public async Task CreateInstructor_CorrectModel_ReturnEventModel()
        {
            //Arrange
            var instructorModel = _fixture.Create<InstructorModel>();
            var instructorEntity = _fixture.Create<InstructorEntity>();

            _mockRepository.Setup(x => x.Create(It.IsAny<InstructorEntity>(), CancellationToken.None)).ReturnsAsync(value: instructorEntity);

            var service = new InstructorService(_mockRepository.Object);

            //Act
            var result = await service.CreateInstructor(instructorModel, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.Create(It.IsAny<InstructorEntity>(), CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeOfType<InstructorModel>();
        }

        [Fact]
        public async Task UpdateInstructor_CorrectModel_ReturnInstructorModel()
        {
            //Arrange
            var instructorModel = _fixture.Create<InstructorModel>();
            var instructorEntity = _fixture.Create<InstructorEntity>();

            instructorEntity.Id = instructorModel.Id;

            _mockRepository.Setup(x => x.GetById(instructorModel.Id, CancellationToken.None)).ReturnsAsync(instructorEntity);
            _mockRepository.Setup(x => x.Update(It.IsAny<InstructorEntity>(), CancellationToken.None)).ReturnsAsync(instructorEntity);

            var service = new InstructorService(_mockRepository.Object);

            //Act
            var result = await service.UpdateInstructor(instructorModel, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(instructorModel.Id, CancellationToken.None), Times.Once);
            _mockRepository.Verify(x => x.Update(It.IsAny<InstructorEntity>(), CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeOfType<InstructorModel>();
        }

        [Fact]
        public async Task UpdateInstructor_IncorrectModel_ReturnEntityNotFoundException()
        {
            //Arrange
            var instructorModel = _fixture.Create<InstructorModel>();

            _mockRepository.Setup(x => x.GetById(instructorModel.Id, CancellationToken.None)).ReturnsAsync(value: null);

            var service = new InstructorService(_mockRepository.Object);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => service.UpdateInstructor(instructorModel, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(instructorModel.Id, CancellationToken.None), Times.Once);

            result.Should().NotBeNull();

            Assert.Equal($"Instructor with id: {instructorModel.Id} not found", result.Message);
        }

        [Fact]
        public async Task DeleteInstructor_CorrectId_ReturnNull()
        {
            //Arrange
            var instructorEntity = _fixture.Create<InstructorEntity>();

            _mockRepository.Setup(x => x.GetById(instructorEntity.Id, CancellationToken.None)).ReturnsAsync(instructorEntity);
            _mockRepository.Setup(x => x.Delete(It.IsAny<InstructorEntity>(), CancellationToken.None));

            var service = new InstructorService(_mockRepository.Object);

            //Act
            await service.DeleteInstructor(instructorEntity.Id, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(instructorEntity.Id, CancellationToken.None), Times.Once);
            _mockRepository.Verify(x => x.Delete(It.IsAny<InstructorEntity>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task DeleteInstructor_IncorrectId_ReturnArgumentNullException()
        {
            //Arrange
            var inputId = _fixture.Create<int>();

            _mockRepository.Setup(x => x.GetById(inputId, CancellationToken.None)).ReturnsAsync(value: null);

            var service = new InstructorService(_mockRepository.Object);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => service.DeleteInstructor(inputId, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(inputId, CancellationToken.None), Times.Once);

            result.Should().NotBeNull();

            Assert.Equal($"Instructor with id: {inputId} not found", result.Message);
        }
    }
}
