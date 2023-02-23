using FluentAssertions;
using Moq;
using SkyDrive.BLL.Exceptions;
using SkyDrive.BLL.Models;
using SkyDrive.BLL.Services;
using SkyDrive.DAL.Entities;
using SkyDrive.DAL.Interfaces;
using SkyDrive.Tests.FixtureCustomization.Attributes;

namespace SkyDrive.Tests.UnitTests
{
    public class EventServiceTests
    {
        private readonly Mock<IEventRepository> _mockRepository;
        private readonly EventService _service;

        public EventServiceTests()
        {
            _mockRepository = new Mock<IEventRepository>();
            _service = new EventService(_mockRepository.Object);
        }

        [Theory]
        [FixtureWithoutCirculation]
        public async Task GetAllEvents_ReturnListOfEvents(
            List<EventModel> listOfEventModels,
            List<EventEntity> listOfEventEntities)
        {
            //Arrange
            _mockRepository.Setup(x => x.GetAll(CancellationToken.None)).ReturnsAsync(listOfEventEntities);

            //Act
            var result = await _service.GetAllEvents(CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetAll(CancellationToken.None), Times.Once);

            result.Should().NotBeNullOrEmpty().And.HaveSameCount(listOfEventModels);
        }

        [Fact]
        public async Task GetAllEvents_ReturnEmptyList()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetAll(CancellationToken.None)).ReturnsAsync(new List<EventEntity>());

            //Act
            var result = await _service.GetAllEvents(CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetAll(CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeEmpty();
        }

        [Theory]
        [FixtureWithoutCirculation]
        public async Task GetEventById_CorrectId_ReturnEventModel(EventEntity eventEntity)
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(eventEntity.Id, CancellationToken.None)).ReturnsAsync(eventEntity);

            //Act
            var result = await _service.GetEventById(eventEntity.Id, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(eventEntity.Id, CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeOfType<EventModel>();
        }

        [Theory]
        [FixtureWithoutCirculation]
        public async Task GetEventById_IncorrectId_ReturnNull(int eventId)
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(eventId, CancellationToken.None)).ReturnsAsync(value: null);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => _service.GetEventById(eventId, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(eventId, CancellationToken.None), Times.Once);

            result.Should().NotBeNull();

            Assert.Equal($"Event with id: {eventId} not found", result.Message);
        }

        [Theory]
        [FixtureWithoutCirculation]
        public async Task CreateEvent_CorrectModel_ReturnEventModel(
            EventEntity eventEntity,
            EventModel eventModel)
        {
            //Arrange
            _mockRepository.Setup(x => x.Create(It.IsAny<EventEntity>(), CancellationToken.None)).ReturnsAsync(value: eventEntity);

            //Act
            var result = await _service.CreateEvent(eventModel, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.Create(It.IsAny<EventEntity>(), CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeOfType<EventModel>();
        }

        [Theory]
        [FixtureWithoutCirculation]
        public async Task UpdateEvent_CorrectModel_ReturnEventModel(
            EventEntity eventEntity,
            EventModel eventModel)
        {
            //Arrange
            eventEntity.Id = eventModel.Id;

            _mockRepository.Setup(x => x.GetById(eventModel.Id, CancellationToken.None)).ReturnsAsync(eventEntity);
            _mockRepository.Setup(x => x.Update(It.IsAny<EventEntity>(), CancellationToken.None)).ReturnsAsync(eventEntity);

            //Act
            var result = await _service.UpdateEvent(eventModel, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(eventModel.Id, CancellationToken.None), Times.Once);
            _mockRepository.Verify(x => x.Update(It.IsAny<EventEntity>(), CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeOfType<EventModel>();
        }

        [Theory]
        [FixtureWithoutCirculation]
        public async Task UpdateEvent_IncorrectModel_ReturnEntityNotFoundException(EventModel eventModel)
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(eventModel.Id, CancellationToken.None)).ReturnsAsync(value: null);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => _service.UpdateEvent(eventModel, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(eventModel.Id, CancellationToken.None), Times.Once);

            result.Should().NotBeNull();

            Assert.Equal($"Event with id: {eventModel.Id} not found", result.Message);
        }

        [Theory]
        [FixtureWithoutCirculation]
        public async Task DeleteEvent_CorrectId_ReturnNull(EventEntity eventEntity)
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(eventEntity.Id, CancellationToken.None)).ReturnsAsync(eventEntity);
            _mockRepository.Setup(x => x.Delete(It.IsAny<EventEntity>(), CancellationToken.None));

            //Act
            await _service.DeleteEvent(eventEntity.Id, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(eventEntity.Id, CancellationToken.None), Times.Once);
            _mockRepository.Verify(x => x.Delete(It.IsAny<EventEntity>(), CancellationToken.None), Times.Once);
        }

        [Theory]
        [FixtureWithoutCirculation]
        public async Task DeleteEvent_IncorrectId_ReturnArgumentNullException(int eventId)
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(eventId, CancellationToken.None)).ReturnsAsync(value: null);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => _service.DeleteEvent(eventId, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(eventId, CancellationToken.None), Times.Once);

            result.Should().NotBeNull();

            Assert.Equal($"Event with id: {eventId} not found", result.Message);
        }
    }
}