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
    public class EventServiceTests
    {
        private readonly Mock<IEventRepository> _mockRepository;
        private readonly Fixture _fixture;

        public EventServiceTests()
        {
            _mockRepository = new Mock<IEventRepository>();

            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetAllEvents_ReturnListOfEvents()
        {
            //Arrange
            var listOfEventModels = _fixture.Create<List<EventModel>>();
            var listOfEventEntities = _fixture.Create<List<EventEntity>>();

            _mockRepository.Setup(x => x.GetAll(CancellationToken.None)).ReturnsAsync(listOfEventEntities);

            var service = new EventService(_mockRepository.Object);

            //Act
            var result = await service.GetAllEvents(CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetAll(CancellationToken.None), Times.Once);

            result.Should().NotBeNullOrEmpty().And.HaveSameCount(listOfEventModels);
        }

        [Fact]
        public async Task GetAllEvents_ReturnEmptyList()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetAll(CancellationToken.None)).ReturnsAsync(new List<EventEntity>());

            var service = new EventService(_mockRepository.Object);

            //Act
            var result = await service.GetAllEvents(CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetAll(CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeEmpty();
        }

        [Fact]
        public async Task GetEventById_CorrectId_ReturnEventModel()
        {
            //Arrange
            var eventEntity = _fixture.Create<EventEntity>();

            _mockRepository.Setup(x => x.GetById(eventEntity.Id, CancellationToken.None)).ReturnsAsync(eventEntity);

            var service = new EventService(_mockRepository.Object);

            //Act
            var result = await service.GetEventById(eventEntity.Id, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(eventEntity.Id, CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeOfType<EventModel>();
        }

        [Fact]
        public async Task GetEventById_IncorrectId_ReturnNull()
        {
            //Arrange
            var inputId = _fixture.Create<int>();

            _mockRepository.Setup(x => x.GetById(inputId, CancellationToken.None)).ReturnsAsync(value: null);

            var service = new EventService(_mockRepository.Object);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => service.GetEventById(inputId, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(inputId, CancellationToken.None), Times.Once);

            result.Should().NotBeNull();

            Assert.Equal($"Event with id: {inputId} not found", result.Message);
        }

        [Fact]
        public async Task CreateEvent_CorrectModel_ReturnEventModel()
        {
            //Arrange
            var eventModel = _fixture.Create<EventModel>();
            var eventEntity = _fixture.Create<EventEntity>();

            _mockRepository.Setup(x => x.Create(It.IsAny<EventEntity>(), CancellationToken.None)).ReturnsAsync(value: eventEntity);

            var service = new EventService(_mockRepository.Object);

            //Act
            var result = await service.CreateEvent(eventModel, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.Create(It.IsAny<EventEntity>(), CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeOfType<EventModel>();
        }

        [Fact]
        public async Task UpdateEvent_CorrectModel_ReturnEventModel()
        {
            //Arrange
            var eventModel = _fixture.Create<EventModel>();
            var eventEntity = _fixture.Create<EventEntity>();

            eventEntity.Id = eventModel.Id;

            _mockRepository.Setup(x => x.GetById(eventModel.Id, CancellationToken.None)).ReturnsAsync(eventEntity);
            _mockRepository.Setup(x => x.Update(It.IsAny<EventEntity>(), CancellationToken.None)).ReturnsAsync(eventEntity);

            var service = new EventService(_mockRepository.Object);

            //Act
            var result = await service.UpdateEvent(eventModel, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(eventModel.Id, CancellationToken.None), Times.Once);
            _mockRepository.Verify(x => x.Update(It.IsAny<EventEntity>(), CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeOfType<EventModel>();
        }

        [Fact]
        public async Task UpdateEvent_IncorrectModel_ReturnEntityNotFoundException()
        {
            //Arrange
            var eventModel = _fixture.Create<EventModel>();

            _mockRepository.Setup(x => x.GetById(eventModel.Id, CancellationToken.None)).ReturnsAsync(value: null);

            var service = new EventService(_mockRepository.Object);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => service.UpdateEvent(eventModel, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(eventModel.Id, CancellationToken.None), Times.Once);

            result.Should().NotBeNull();

            Assert.Equal($"Event with id: {eventModel.Id} not found", result.Message);
        }

        [Fact]
        public async Task DeleteEvent_CorrectId_ReturnNull()
        {
            //Arrange
            var eventEntity = _fixture.Create<EventEntity>();

            _mockRepository.Setup(x => x.GetById(eventEntity.Id, CancellationToken.None)).ReturnsAsync(eventEntity);
            _mockRepository.Setup(x => x.Delete(It.IsAny<EventEntity>(), CancellationToken.None));

            var service = new EventService(_mockRepository.Object);

            //Act
            await service.DeleteEvent(eventEntity.Id, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(eventEntity.Id, CancellationToken.None), Times.Once);
            _mockRepository.Verify(x => x.Delete(It.IsAny<EventEntity>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task DeleteEvent_IncorrectId_ReturnArgumentNullException()
        {
            //Arrange
            var inputId = _fixture.Create<int>();

            _mockRepository.Setup(x => x.GetById(inputId, CancellationToken.None)).ReturnsAsync(value: null);

            var service = new EventService(_mockRepository.Object);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => service.DeleteEvent(inputId, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(inputId, CancellationToken.None), Times.Once);

            result.Should().NotBeNull();

            Assert.Equal($"Event with id: {inputId} not found", result.Message);
        }
    }
}