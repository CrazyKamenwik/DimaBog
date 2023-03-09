using FluentAssertions;
using Mapster;
using SkyDrive.BLL.Models;
using SkyDrive.DAL.Entities;
using SkyDrive.Tests.FixtureCustomization.Attributes;
using SkyDrive.ViewModels;

namespace SkyDrive.Tests.UnitTests.MapperTests
{
    public class MemberTests
    {
        [Theory]
        [FixtureWithoutCirculation]
        public void AdaptFromModelToEntity_ReturnEntity(MemberModel memberModel)
        {
            //Act
            var memberEntity = memberModel.Adapt<MemberEntity>();

            //Assert
            memberEntity.Should().BeEquivalentTo(memberModel);
        }

        [Theory]
        [FixtureWithoutCirculation]
        public void AdaptFromEntityToModel_ReturnModel(MemberEntity memberEntity)
        {
            //Act
            var memberModel = memberEntity.Adapt<MemberModel>();

            //Assert
            memberModel.Should().BeEquivalentTo(memberEntity);
        }

        [Theory]
        [FixtureWithoutCirculation]
        public void AdaptFromModelToViewModel_ReturnViewModel(MemberModel memberModel)
        {
            //Act
            var memberViewModel = memberModel.Adapt<MemberViewModel>();

            //Assert
            memberViewModel.Should().BeEquivalentTo(memberModel);
        }

        [Theory]
        [FixtureWithoutCirculation]
        public void AdaptFromViewModelToModel_ReturnModel(MemberViewModel memberViewModel)
        {
            //Act
            var memberModel = memberViewModel.Adapt<MemberModel>();

            //Assert
            memberModel.Should().BeEquivalentTo(memberViewModel);
        }
    }
}
