using FluentAssertions;
using Mapster;
using SkyDrive.BLL.Models;
using SkyDrive.DAL.Entities;
using SkyDrive.Tests.FixtureCustomization;
using SkyDrive.ViewModels;

namespace SkyDrive.Tests.UnitTests.MapperTests
{
    public class InstructorTests
    {
        [Theory]
        [FixtureWithoutCirculation]
        public Task AdaptFromModelToEntity_ReturnEntity(InstructorModel instructorModel)
        {
            //Act
            var instructorEntity = instructorModel.Adapt<InstructorModel>();

            //Assert
            instructorEntity.Should().BeEquivalentTo(instructorModel);

            return Task.CompletedTask;
        }

        [Theory]
        [FixtureWithoutCirculation]
        public Task AdaptFromEntityToModel_ReturnModel(InstructorEntity instructorEntity)
        {
            //Act
            var instructorModel = instructorEntity.Adapt<InstructorModel>();

            //Assert
            instructorModel.Should().BeEquivalentTo(instructorEntity);

            return Task.CompletedTask;
        }

        [Theory]
        [FixtureWithoutCirculation]
        public Task AdaptFromModelToViewModel_ReturnViewModel(InstructorModel instructorModel)
        {
            //Act
            var instructorViewModel = instructorModel.Adapt<InstructorViewModel>();

            //Assert
            instructorViewModel.Should().BeEquivalentTo(instructorModel);

            return Task.CompletedTask;
        }

        [Theory]
        [FixtureWithoutCirculation]
        public Task AdaptFromViewModelToModel_ReturnModel(InstructorViewModel instructorViewModel)
        {
            //Act
            var instructorModel = instructorViewModel.Adapt<InstructorModel>();

            //Assert
            instructorModel.Should().BeEquivalentTo(instructorViewModel);

            return Task.CompletedTask;
        }
    }
}
