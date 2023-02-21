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
    public class MemberServiceTests
    {
        private readonly Mock<IMemberRepository> _mockRepository;
        private readonly MemberService _service;

        public MemberServiceTests()
        {
            _mockRepository = new Mock<IMemberRepository>();
            _service = new MemberService(_mockRepository.Object);
        }

        [Theory]
        [MyFixture]
        public async Task GetAllMembers_ReturnListOfMembers(
            List<MemberModel> listOfMemberModels,
            List<MemberEntity> listOfMemberEntities)
        {
            //Arrange
            _mockRepository.Setup(x => x.GetAll(CancellationToken.None)).ReturnsAsync(listOfMemberEntities);

            //Act
            var result = await _service.GetAllMembers(CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetAll(CancellationToken.None), Times.Once);

            result.Should().NotBeNullOrEmpty().And.HaveSameCount(listOfMemberModels);
        }

        [Fact]
        public async Task GetAllMembers_ReturnEmptyList()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetAll(CancellationToken.None)).ReturnsAsync(new List<MemberEntity>());

            //Act
            var result = await _service.GetAllMembers(CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetAll(CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeEmpty();
        }

        [Theory]
        [MyFixture]
        public async Task GetMemberById_CorrectId_ReturnMemberModel(MemberEntity memberEntity)
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(memberEntity.Id, CancellationToken.None)).ReturnsAsync(memberEntity);

            //Act
            var result = await _service.GetMemberById(memberEntity.Id, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(memberEntity.Id, CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeOfType<MemberModel>();
        }

        [Theory]
        [MyFixture]
        public async Task GetMemberById_IncorrectId_ReturnNull(int memberId)
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(memberId, CancellationToken.None)).ReturnsAsync(value: null);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => _service.GetMemberById(memberId, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(memberId, CancellationToken.None), Times.Once);

            result.Should().NotBeNull();

            Assert.Equal($"Member with id: {memberId} not found", result.Message);
        }

        [Theory]
        [MyFixture]
        public async Task CreateMember_CorrectModel_ReturnEventModel(
            MemberModel memberModel,
            MemberEntity memberEntity)
        {
            //Arrange
            _mockRepository.Setup(x => x.Create(It.IsAny<MemberEntity>(), CancellationToken.None)).ReturnsAsync(value: memberEntity);

            //Act
            var result = await _service.CreateMember(memberModel, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.Create(It.IsAny<MemberEntity>(), CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeOfType<MemberModel>();
        }

        [Theory]
        [MyFixture]
        public async Task UpdateMember_CorrectModel_ReturnMemberModel(
            MemberEntity memberEntity,
            MemberModel memberModel)
        {
            //Arrange
            memberEntity.Id = memberModel.Id;

            _mockRepository.Setup(x => x.GetById(memberModel.Id, CancellationToken.None)).ReturnsAsync(memberEntity);
            _mockRepository.Setup(x => x.Update(It.IsAny<MemberEntity>(), CancellationToken.None)).ReturnsAsync(memberEntity);

            //Act
            var result = await _service.UpdateMember(memberModel, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(memberModel.Id, CancellationToken.None), Times.Once);
            _mockRepository.Verify(x => x.Update(It.IsAny<MemberEntity>(), CancellationToken.None), Times.Once);

            result.Should().NotBeNull().And.BeOfType<MemberModel>();
        }

        [Theory]
        [MyFixture]
        public async Task UpdateMember_IncorrectModel_ReturnEntityNotFoundException(MemberModel memberModel)
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(memberModel.Id, CancellationToken.None)).ReturnsAsync(value: null);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => _service.UpdateMember(memberModel, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(memberModel.Id, CancellationToken.None), Times.Once);

            result.Should().NotBeNull();

            Assert.Equal($"Member with id: {memberModel.Id} not found", result.Message);
        }

        [Theory]
        [MyFixture]
        public async Task DeleteMember_CorrectId_ReturnNull(MemberEntity memberEntity)
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(memberEntity.Id, CancellationToken.None)).ReturnsAsync(memberEntity);
            _mockRepository.Setup(x => x.Delete(It.IsAny<MemberEntity>(), CancellationToken.None));

            //Act
            await _service.DeleteMember(memberEntity.Id, CancellationToken.None);

            //Assert
            _mockRepository.Verify(x => x.GetById(memberEntity.Id, CancellationToken.None), Times.Once);
            _mockRepository.Verify(x => x.Delete(It.IsAny<MemberEntity>(), CancellationToken.None), Times.Once);
        }

        [Theory]
        [MyFixture]
        public async Task DeleteMember_IncorrectId_ReturnArgumentNullException(int memberId)
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(memberId, CancellationToken.None)).ReturnsAsync(value: null);

            //Act
            var result = await Assert.ThrowsAsync<EntityNotFoundException>(() => _service.DeleteMember(memberId, CancellationToken.None));

            //Assert
            _mockRepository.Verify(x => x.GetById(memberId, CancellationToken.None), Times.Once);

            result.Should().NotBeNull();

            Assert.Equal($"Member with id: {memberId} not found", result.Message);
        }
    }
}
