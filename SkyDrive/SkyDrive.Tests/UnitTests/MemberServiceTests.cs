using Moq;
using SkyDrive.BLL.Exceptions;
using SkyDrive.BLL.Models;
using SkyDrive.BLL.Services;
using SkyDrive.DAL.Entities;
using SkyDrive.DAL.Interfaces;

namespace SkyDrive.Tests.UnitTests
{
    public class MemberServiceTests
    {
        private readonly Mock<IMemberRepository> _mockRepository;

        public MemberServiceTests()
        {
            _mockRepository = new Mock<IMemberRepository>();
        }

        [Fact]
        public async Task GetAllMembers_ReturnListOfMembers()
        {
            //Arrange
            var listOfMemberModels = new List<MemberModel>()
            {
                new()
                {
                    Id = 1,
                    FirstName= "FirstName",
                    LastName= "LastName",
                    MiddleName= "MiddleName",
                    Events = null
                },
                new()
                {
                    Id = 2,
                    FirstName= "FirstName 2",
                    LastName= "LastName 2",
                    MiddleName= "MiddleName 2",
                    Events = null
                }
            };
            var listOfMemberEntities = new List<MemberEntity>
            {
                new()
                {
                    Id = 1,
                    FirstName= "FirstName",
                    LastName= "LastName",
                    MiddleName= "MiddleName",
                    Events = null
                },
                new()
                {
                    Id = 2,
                    FirstName= "FirstName 2",
                    LastName= "LastName 2",
                    MiddleName= "MiddleName 2",
                    Events = null
                }
            };

            _mockRepository.Setup(x => x.GetAll(CancellationToken.None)).ReturnsAsync(listOfMemberEntities);
            var service = new MemberService(_mockRepository.Object);

            //Act
            var result = await service.GetAllMembers(CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetAll(CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(result.Count(), listOfMemberModels.Count);
        }

        [Fact]
        public async Task GetAllMembers_ReturnEmptyList()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetAll(CancellationToken.None)).ReturnsAsync(new List<MemberEntity>());
            var service = new MemberService(_mockRepository.Object);

            //Act
            var result = await service.GetAllMembers(CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetAll(CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetMemberById_CorrectId_ReturnMemberModel()
        {
            //Arrange
            var memberEntity = new MemberEntity
            {
                Id = 2,
                FirstName = "FirstName 2",
                LastName = "LastName 2",
                MiddleName = "MiddleName 2",
                Events = null
            };

            _mockRepository.Setup(x => x.GetById(2, CancellationToken.None)).ReturnsAsync(memberEntity);
            var service = new MemberService(_mockRepository.Object);

            //Act
            var result = await service.GetMemberById(2, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(2, CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<MemberModel>(result);
        }

        [Fact]
        public async Task GetMemberById_IncorrectId_ReturnNull()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(2, CancellationToken.None)).ReturnsAsync(value: null);
            var service = new MemberService(_mockRepository.Object);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => service.GetMemberById(2, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(2, CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("Member with id: 2 not found", result.Message);
        }

        [Fact]
        public async Task CreateMember_CorrectModel_ReturnEventModel()
        {
            //Arrange
            var memberModel = new MemberModel
            {
                Id = 0,
                FirstName = "FirstName 2",
                LastName = "LastName 2",
                MiddleName = "MiddleName 2",
                Events = null
            };
            var memberEntity = new MemberEntity
            {
                Id = 0,
                FirstName = "FirstName 2",
                LastName = "LastName 2",
                MiddleName = "MiddleName 2",
                Events = null
            };

            _mockRepository.Setup(x => x.Create(It.IsAny<MemberEntity>(), CancellationToken.None)).ReturnsAsync(value: memberEntity);
            var service = new MemberService(_mockRepository.Object);

            //Act
            var result = await service.CreateMember(memberModel, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.Create(It.IsAny<MemberEntity>(), CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<MemberModel>(result);
        }

        [Fact]
        public async Task UpdateMember_CorrectModel_ReturnMemberModel()
        {
            //Arrange
            var memberModel = new MemberModel
            {
                Id = 3,
                FirstName = "FirstName 2",
                LastName = "LastName 2",
                MiddleName = "MiddleName 2",
                Events = null
            };
            var memberEntity = new MemberEntity
            {
                Id = 3,
                FirstName = "FirstName 2",
                LastName = "LastName 2",
                MiddleName = "MiddleName 2",
                Events = null
            };

            _mockRepository.Setup(x => x.GetById(3, CancellationToken.None)).ReturnsAsync(memberEntity);
            _mockRepository.Setup(x => x.Update(It.IsAny<MemberEntity>(), CancellationToken.None)).ReturnsAsync(memberEntity);
            var service = new MemberService(_mockRepository.Object);

            //Act
            var result = await service.UpdateMember(memberModel, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(3, CancellationToken.None), Times.Once);
            _mockRepository.Verify(x => x.Update(It.IsAny<MemberEntity>(), CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<MemberModel>(result);
        }

        [Fact]
        public async Task UpdateMember_IncorrectModel_ReturnEntityNotFoundException()
        {
            //Arrange
            var memberModel = new MemberModel
            {
                Id = 3,
                FirstName = "FirstName 2",
                LastName = "LastName 2",
                MiddleName = "MiddleName 2",
                Events = null
            };

            _mockRepository.Setup(x => x.GetById(3, CancellationToken.None)).ReturnsAsync(value: null);
            var service = new MemberService(_mockRepository.Object);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => service.UpdateMember(memberModel, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(3, CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("Member with id: 3 not found", result.Message);
        }

        [Fact]
        public async Task DeleteMember_CorrectId_ReturnNull()
        {
            //Arrange
            var memberEntity = new MemberEntity
            {
                Id = 3,
                FirstName = "FirstName 2",
                LastName = "LastName 2",
                MiddleName = "MiddleName 2",
                Events = null
            };

            _mockRepository.Setup(x => x.GetById(3, CancellationToken.None)).ReturnsAsync(memberEntity);
            _mockRepository.Setup(x => x.Delete(It.IsAny<MemberEntity>(), CancellationToken.None));
            var service = new MemberService(_mockRepository.Object);

            //Act
            await service.DeleteMember(3, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(3, CancellationToken.None), Times.Once);
            _mockRepository.Verify(x => x.Delete(It.IsAny<MemberEntity>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task DeleteMember_IncorrectId_ReturnArgumentNullException()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(3, CancellationToken.None)).ReturnsAsync(value: null);
            var service = new MemberService(_mockRepository.Object);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => service.DeleteMember(3, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(3, CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("Member with id: 3 not found", result.Message);
        }
    }
}
