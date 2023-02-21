using FluentAssertions;
using Mapster;
using SkyDrive.BLL.Models;
using SkyDrive.DAL.Entities;
using SkyDrive.Tests.FixtureCustomization;
using SkyDrive.ViewModels;

namespace SkyDrive.Tests.UnitTests.MapperTests
{
    public class MemberTests
    {
        [Theory]
        [FixtureWithoutCirculation]
        public Task AdaptFromModelToEntity_ReturnEntity(MemberModel memberModel)
        {
            //Act
            var memberEntity = memberModel.Adapt<MemberEntity>();

            //Assert
            memberEntity.Should().BeEquivalentTo(memberModel);

            return Task.CompletedTask;
        }

        [Theory]
        [FixtureWithoutCirculation]
        public Task AdaptFromEntityToModel_ReturnModel(MemberEntity memberEntity)
        {
            //Act
            var memberModel = memberEntity.Adapt<MemberModel>();

            //Assert
            memberModel.Should().BeEquivalentTo(memberEntity);

            return Task.CompletedTask;
        }

        [Theory]
        [FixtureWithoutCirculation]
        public Task AdaptFromModelToViewModel_ReturnViewModel(MemberModel memberModel)
        {
            //Act
            var memberViewModel = memberModel.Adapt<MemberViewModel>();

            //Assert
            memberViewModel.Should().BeEquivalentTo(memberModel);

            return Task.CompletedTask;
        }

        [Theory]
        [FixtureWithoutCirculation]
        public Task AdaptFromViewModelToModel_ReturnModel(MemberViewModel memberViewModel)
        {
            //Act
            var memberModel = memberViewModel.Adapt<MemberModel>();

            //Assert
            memberModel.Should().BeEquivalentTo(memberViewModel);

            return Task.CompletedTask;
        }
    }
}
