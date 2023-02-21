using FluentAssertions;
using Moq;
using SkyDrive.BLL.Exceptions;
using SkyDrive.BLL.Models;
using SkyDrive.BLL.Services;
using SkyDrive.DAL.Entities;
using SkyDrive.DAL.Interfaces;
using SkyDrive.Tests.FixtureCustomization;

namespace SkyDrive.Tests.UnitTests
{
    public class InstructorServiceTests
    {
        private readonly Mock<IInstructorRepository> _mockRepository;
        private readonly InstructorService _service;

        public InstructorServiceTests()
        {
            _mockRepository = new Mock<IInstructorRepository>();
            _service = new InstructorService(_mockRepository.Object);
        }

        [Theory]
        [FixtureWithoutCirculation]
        public async Task GetAllInstructors_ReturnListOfInstructors(
            List<InstructorModel> listOfInstructorModels,
            List<InstructorEntity> listOfInstructorEntities)
        {
            //Arrange
            _mockRepository.Setup(x => x.GetAll(CancellationToken.None)).ReturnsAsync(listOfInstructorEntities);

            //Act
            var result = await _service.GetAllInstructors(CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetAll(CancellationToken.None), Times.Once);

            result.Should().NotBeNullOrEmpty().And.HaveSameCount(listOfInstructorModels);
        }

        [Fact]
        public async Task GetAllInstructors_ReturnEmptyList()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetAll(CancellationToken.None)).ReturnsAsync(new List<InstructorEntity>());

            //Act
            var result = await _service.GetAllInstructors(CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetAll(CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeEmpty();
        }

        [Theory]
        [FixtureWithoutCirculation]
        public async Task GetInstructorById_CorrectId_ReturnInstructorModel(InstructorEntity instructorEntity)
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(2, CancellationToken.None)).ReturnsAsync(instructorEntity);

            //Act
            var result = await _service.GetInstructorById(2, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(2, CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeOfType<InstructorModel>();
        }

        [Theory]
        [FixtureWithoutCirculation]
        public async Task GetInstructorById_IncorrectId_ReturnNull(int instructorId)
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(instructorId, CancellationToken.None)).ReturnsAsync(value: null);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => _service.GetInstructorById(instructorId, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(instructorId, CancellationToken.None), Times.Once);

            result.Should().NotBeNull();

            Assert.Equal($"Instructor with id: {instructorId} not found", result.Message);
        }

        [Theory]
        [FixtureWithoutCirculation]
        public async Task CreateInstructor_CorrectModel_ReturnEventModel(
            InstructorEntity instructorEntity,
            InstructorModel instructorModel)
        {
            //Arrange
            _mockRepository.Setup(x => x.Create(It.IsAny<InstructorEntity>(), CancellationToken.None)).ReturnsAsync(value: instructorEntity);

            //Act
            var result = await _service.CreateInstructor(instructorModel, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.Create(It.IsAny<InstructorEntity>(), CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeOfType<InstructorModel>();
        }

        [Theory]
        [FixtureWithoutCirculation]
        public async Task UpdateInstructor_CorrectModel_ReturnInstructorModel(
            InstructorEntity instructorEntity,
            InstructorModel instructorModel)
        {
            //Arrange
            instructorEntity.Id = instructorModel.Id;

            _mockRepository.Setup(x => x.GetById(instructorModel.Id, CancellationToken.None)).ReturnsAsync(instructorEntity);
            _mockRepository.Setup(x => x.Update(It.IsAny<InstructorEntity>(), CancellationToken.None)).ReturnsAsync(instructorEntity);

            //Act
            var result = await _service.UpdateInstructor(instructorModel, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(instructorModel.Id, CancellationToken.None), Times.Once);
            _mockRepository.Verify(x => x.Update(It.IsAny<InstructorEntity>(), CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeOfType<InstructorModel>();
        }

        [Theory]
        [FixtureWithoutCirculation]
        public async Task UpdateInstructor_IncorrectModel_ReturnEntityNotFoundException(InstructorModel instructorModel)
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(instructorModel.Id, CancellationToken.None)).ReturnsAsync(value: null);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => _service.UpdateInstructor(instructorModel, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(instructorModel.Id, CancellationToken.None), Times.Once);

            result.Should().NotBeNull();

            Assert.Equal($"Instructor with id: {instructorModel.Id} not found", result.Message);
        }

        [Theory]
        [FixtureWithoutCirculation]
        public async Task DeleteInstructor_CorrectId_ReturnNull(InstructorEntity instructorEntity)
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(instructorEntity.Id, CancellationToken.None)).ReturnsAsync(instructorEntity);
            _mockRepository.Setup(x => x.Delete(It.IsAny<InstructorEntity>(), CancellationToken.None));

            //Act
            await _service.DeleteInstructor(instructorEntity.Id, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(instructorEntity.Id, CancellationToken.None), Times.Once);
            _mockRepository.Verify(x => x.Delete(It.IsAny<InstructorEntity>(), CancellationToken.None), Times.Once);
        }

        [Theory]
        [FixtureWithoutCirculation]
        public async Task DeleteInstructor_IncorrectId_ReturnArgumentNullException(int instructorId)
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(instructorId, CancellationToken.None)).ReturnsAsync(value: null);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => _service.DeleteInstructor(instructorId, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(instructorId, CancellationToken.None), Times.Once);

            result.Should().NotBeNull();

            Assert.Equal($"Instructor with id: {instructorId} not found", result.Message);
        }
    }
}
