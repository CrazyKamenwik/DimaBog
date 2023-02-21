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
    public class MemberServiceTests
    {
        private readonly Mock<IMemberRepository> _mockRepository;
        private readonly Fixture _fixture;

        public MemberServiceTests()
        {
            _mockRepository = new Mock<IMemberRepository>();

            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetAllMembers_ReturnListOfMembers()
        {
            //Arrange
            var listOfMemberModels = _fixture.Create<List<MemberModel>>();
            var listOfMemberEntities = _fixture.Create<List<MemberEntity>>();

            _mockRepository.Setup(x => x.GetAll(CancellationToken.None)).ReturnsAsync(listOfMemberEntities);
            var service = new MemberService(_mockRepository.Object);

            //Act
            var result = await service.GetAllMembers(CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetAll(CancellationToken.None), Times.Once);

            result.Should().NotBeNullOrEmpty().And.HaveSameCount(listOfMemberModels);
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

            result.Should().NotBeNull().And.BeEmpty();
        }

        [Fact]
        public async Task GetMemberById_CorrectId_ReturnMemberModel()
        {
            //Arrange
            var memberEntity = _fixture.Create<MemberEntity>();

            _mockRepository.Setup(x => x.GetById(memberEntity.Id, CancellationToken.None)).ReturnsAsync(memberEntity);

            var service = new MemberService(_mockRepository.Object);

            //Act
            var result = await service.GetMemberById(memberEntity.Id, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(memberEntity.Id, CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeOfType<MemberModel>();
        }

        [Fact]
        public async Task GetMemberById_IncorrectId_ReturnNull()
        {
            //Arrange
            var inputId = _fixture.Create<int>();

            _mockRepository.Setup(x => x.GetById(inputId, CancellationToken.None)).ReturnsAsync(value: null);

            var service = new MemberService(_mockRepository.Object);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => service.GetMemberById(inputId, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(inputId, CancellationToken.None), Times.Once);

            result.Should().NotBeNull();

            Assert.Equal($"Member with id: {inputId} not found", result.Message);
        }

        [Fact]
        public async Task CreateMember_CorrectModel_ReturnEventModel()
        {
            //Arrange
            var memberModel = _fixture.Create<MemberModel>();
            var memberEntity = _fixture.Create<MemberEntity>();

            _mockRepository.Setup(x => x.Create(It.IsAny<MemberEntity>(), CancellationToken.None)).ReturnsAsync(value: memberEntity);
            var service = new MemberService(_mockRepository.Object);

            //Act
            var result = await service.CreateMember(memberModel, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.Create(It.IsAny<MemberEntity>(), CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeOfType<MemberModel>();
        }

        [Fact]
        public async Task UpdateMember_CorrectModel_ReturnMemberModel()
        {
            //Arrange
            var memberModel = _fixture.Create<MemberModel>();
            var memberEntity = _fixture.Create<MemberEntity>();

            memberEntity.Id = memberModel.Id;

            _mockRepository.Setup(x => x.GetById(memberModel.Id, CancellationToken.None)).ReturnsAsync(memberEntity);
            _mockRepository.Setup(x => x.Update(It.IsAny<MemberEntity>(), CancellationToken.None)).ReturnsAsync(memberEntity);

            var service = new MemberService(_mockRepository.Object);

            //Act
            var result = await service.UpdateMember(memberModel, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(memberModel.Id, CancellationToken.None), Times.Once);
            _mockRepository.Verify(x => x.Update(It.IsAny<MemberEntity>(), CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeOfType<MemberModel>();
        }

        [Fact]
        public async Task UpdateMember_IncorrectModel_ReturnEntityNotFoundException()
        {
            //Arrange
            var memberModel = _fixture.Create<MemberModel>();

            _mockRepository.Setup(x => x.GetById(memberModel.Id, CancellationToken.None)).ReturnsAsync(value: null);
            var service = new MemberService(_mockRepository.Object);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => service.UpdateMember(memberModel, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(memberModel.Id, CancellationToken.None), Times.Once);

            result.Should().NotBeNull();

            Assert.Equal($"Member with id: {memberModel.Id} not found", result.Message);
        }

        [Fact]
        public async Task DeleteMember_CorrectId_ReturnNull()
        {
            //Arrange
            var memberEntity = _fixture.Create<MemberEntity>();

            _mockRepository.Setup(x => x.GetById(memberEntity.Id, CancellationToken.None)).ReturnsAsync(memberEntity);
            _mockRepository.Setup(x => x.Delete(It.IsAny<MemberEntity>(), CancellationToken.None));

            var service = new MemberService(_mockRepository.Object);

            //Act
            await service.DeleteMember(memberEntity.Id, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(memberEntity.Id, CancellationToken.None), Times.Once);
            _mockRepository.Verify(x => x.Delete(It.IsAny<MemberEntity>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task DeleteMember_IncorrectId_ReturnArgumentNullException()
        {
            //Arrange
            var inputId = _fixture.Create<int>();

            _mockRepository.Setup(x => x.GetById(inputId, CancellationToken.None)).ReturnsAsync(value: null);

            var service = new MemberService(_mockRepository.Object);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => service.DeleteMember(inputId, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(inputId, CancellationToken.None), Times.Once);

            result.Should().NotBeNull();

            Assert.Equal($"Member with id: {inputId} not found", result.Message);
        }
    }
}
