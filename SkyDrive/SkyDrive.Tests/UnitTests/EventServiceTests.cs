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

        public EventServiceTests()
        {
            _mockRepository = new Mock<IEventRepository>();
        }

        [Fact]
        public async Task GetAllEvents_ReturnListOfEvents()
        {
            //Arrange
            var listOfEventModels = new List<EventModel>()
            {
                new()
                {
                    Id = 1,
                    DateTimeOfEvent = DateTime.Now,
                    Instructor = null,
                    InstructorId = 2,
                    Members = null
                },
                new()
                {
                    Id = 2,
                    DateTimeOfEvent = DateTime.Now.AddMonths(2),
                    Instructor = null,
                    InstructorId = 4,
                    Members = null
                }
            };
            var listOfEventEntities = new List<EventEntity>
            {
                new()
                {
                    Id = 1,
                    DateTimeOfEvent = DateTime.Now,
                    Instructor = null,
                    InstructorId = 2,
                    Members = null
                },
                new()
                {
                    Id = 2,
                    DateTimeOfEvent = DateTime.Now.AddMonths(2),
                    Instructor = null,
                    InstructorId = 4,
                    Members = null
                }
            };

            _mockRepository.Setup(x => x.GetAll(CancellationToken.None)).ReturnsAsync(listOfEventEntities);
            var service = new EventService(_mockRepository.Object);

            //Act
            var result = await service.GetAllEvents(CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetAll(CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(result.Count(), listOfEventModels.Count);
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
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetEventById_CorrectId_ReturnEventModel()
        {
            //Arrange
            var eventEntity = new EventEntity
            {
                Id = 2,
                DateTimeOfEvent = DateTime.Now,
                Members = null,
                Instructor = null,
                InstructorId = 3
            };

            _mockRepository.Setup(x => x.GetById(2, CancellationToken.None)).ReturnsAsync(eventEntity);
            var service = new EventService(_mockRepository.Object);

            //Act
            var result = await service.GetEventById(2, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(2, CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<EventModel>(result);
        }

        [Fact]
        public async Task GetEventById_IncorrectId_ReturnNull()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(2, CancellationToken.None)).ReturnsAsync(value: null);
            var service = new EventService(_mockRepository.Object);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => service.GetEventById(2, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(2, CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("Event with id: 2 not found", result.Message);
        }

        [Fact]
        public async Task CreateEvent_CorrectModel_ReturnEventModel()
        {
            //Arrange
            var eventModel = new EventModel
            {
                Id = 0,
                DateTimeOfEvent = DateTime.Now,
                InstructorId = 2,
                Instructor = null,
                Members = null
            };
            var eventEntity = new EventEntity
            {
                Id = 0,
                DateTimeOfEvent = DateTime.Now,
                InstructorId = 2,
                Instructor = null,
                Members = null
            };

            _mockRepository.Setup(x => x.Create(It.IsAny<EventEntity>(), CancellationToken.None)).ReturnsAsync(value: eventEntity);
            var service = new EventService(_mockRepository.Object);

            //Act
            var result = await service.CreateEvent(eventModel, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.Create(It.IsAny<EventEntity>(), CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<EventModel>(result);
        }

        [Fact]
        public async Task UpdateEvent_CorrectModel_ReturnEventModel()
        {
            //Arrange
            var eventModel = new EventModel
            {
                Id = 3,
                DateTimeOfEvent = DateTime.Now.AddDays(1),
                InstructorId = 2,
                Instructor = new InstructorModel(),
                Members = new List<MemberModel>()
            };
            var eventEntity = new EventEntity
            {
                Id = 3,
                DateTimeOfEvent = DateTime.Now.AddDays(1),
                InstructorId = 2,
                Instructor = new InstructorEntity(),
                Members = new List<MemberEntity>()
            };

            _mockRepository.Setup(x => x.GetById(3, CancellationToken.None)).ReturnsAsync(eventEntity);
            _mockRepository.Setup(x => x.Update(It.IsAny<EventEntity>(), CancellationToken.None)).ReturnsAsync(eventEntity);
            var service = new EventService(_mockRepository.Object);

            //Act
            var result = await service.UpdateEvent(eventModel, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(3, CancellationToken.None), Times.Once);
            _mockRepository.Verify(x => x.Update(It.IsAny<EventEntity>(), CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<EventModel>(result);
        }

        [Fact]
        public async Task UpdateEvent_IncorrectModel_ReturnEntityNotFoundException()
        {
            //Arrange
            var eventModel = new EventModel
            {
                Id = 3,
                DateTimeOfEvent = DateTime.Now.AddDays(1),
                InstructorId = 2,
                Instructor = new InstructorModel(),
                Members = new List<MemberModel>()
            };

            _mockRepository.Setup(x => x.GetById(3, CancellationToken.None)).ReturnsAsync(value: null);
            var service = new EventService(_mockRepository.Object);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => service.UpdateEvent(eventModel, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(3, CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("Event with id: 3 not found", result.Message);
        }

        [Fact]
        public async Task DeleteEvent_CorrectId_ReturnNull()
        {
            //Arrange
            var eventEntity = new EventEntity
            {
                Id = 3,
                DateTimeOfEvent = DateTime.Now.AddDays(1),
                InstructorId = 2,
                Instructor = new InstructorEntity(),
                Members = new List<MemberEntity>()
            };

            _mockRepository.Setup(x => x.GetById(3, CancellationToken.None)).ReturnsAsync(eventEntity);
            _mockRepository.Setup(x => x.Delete(It.IsAny<EventEntity>(), CancellationToken.None));
            var service = new EventService(_mockRepository.Object);

            //Act
            await service.DeleteEvent(3, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(3, CancellationToken.None), Times.Once);
            _mockRepository.Verify(x => x.Delete(It.IsAny<EventEntity>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task DeleteEvent_IncorrectId_ReturnArgumentNullException()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(3, CancellationToken.None)).ReturnsAsync(value: null);
            var service = new EventService(_mockRepository.Object);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => service.DeleteEvent(3, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(3, CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("Event with id: 3 not found", result.Message);
        }
    }
}