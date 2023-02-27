using FluentAssertions;
using Mapster;
using SkyDrive.BLL.Models;
using SkyDrive.DAL.Entities;
using SkyDrive.Tests.FixtureCustomization.Attributes;
using SkyDrive.ViewModels;

namespace SkyDrive.Tests.UnitTests.MapperTests
{
    public class InstructorTests
    {
        [Theory]
        [FixtureWithoutCirculation]
        public void AdaptFromModelToEntity_ReturnEntity(InstructorModel instructorModel)
        {
            //Act
            var instructorEntity = instructorModel.Adapt<InstructorEntity>();

            //Assert
            instructorEntity.Should().BeEquivalentTo(instructorModel);
        }

        [Theory]
        [FixtureWithoutCirculation]
        public void AdaptFromEntityToModel_ReturnModel(InstructorEntity instructorEntity)
        {
            //Act
            var instructorModel = instructorEntity.Adapt<InstructorModel>();

            //Assert
            instructorModel.Should().BeEquivalentTo(instructorEntity);
        }

        [Theory]
        [FixtureWithoutCirculation]
        public void AdaptFromModelToViewModel_ReturnViewModel(InstructorModel instructorModel)
        {
            //Act
            var instructorViewModel = instructorModel.Adapt<InstructorViewModel>();

            //Assert
            instructorViewModel.Should().BeEquivalentTo(instructorModel);
        }

        [Theory]
        [FixtureWithoutCirculation]
        public void AdaptFromViewModelToModel_ReturnModel(InstructorViewModel instructorViewModel)
        {
            //Act
            var instructorModel = instructorViewModel.Adapt<InstructorModel>();

            //Assert
            instructorModel.Should().BeEquivalentTo(instructorViewModel);
        }
    }
}
