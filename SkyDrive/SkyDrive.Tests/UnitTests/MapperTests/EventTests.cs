using FluentAssertions;
using Mapster;
using SkyDrive.BLL.Models;
using SkyDrive.DAL.Entities;
using SkyDrive.Tests.FixtureCustomization;
using SkyDrive.ViewModels;

namespace SkyDrive.Tests.UnitTests.MapperTests
{
    public class EventTests
    {
        [Theory]
        [FixtureWithoutCirculation]
        public Task AdaptFromModelToEntity_ReturnEntity(EventModel eventModel)
        {
            //Act
            var eventEntity = eventModel.Adapt<EventModel>();

            //Assert
            eventEntity.Should().BeEquivalentTo(eventModel);

            return Task.CompletedTask;
        }

        [Theory]
        [FixtureWithoutCirculation]
        public Task AdaptFromEntityToModel_ReturnModel(EventEntity eventEntity)
        {
            //Act
            var eventModel = eventEntity.Adapt<EventModel>();

            //Assert
            eventModel.Should().BeEquivalentTo(eventEntity);

            return Task.CompletedTask;
        }

        [Theory]
        [FixtureWithoutCirculation]
        public Task AdaptFromModelToViewModel_ReturnViewModel(EventModel eventModel)
        {
            //Act
            var eventViewModel = eventModel.Adapt<EventViewModel>();

            //Assert
            eventViewModel.Should().BeEquivalentTo(eventModel);

            return Task.CompletedTask;
        }

        [Theory]
        [FixtureWithoutCirculation]
        public Task AdaptFromViewModelToModel_ReturnModel(EventViewModel eventViewModel)
        {
            //Act
            var eventModel = eventViewModel.Adapt<EventModel>();

            //Assert
            eventModel.Should().BeEquivalentTo(eventViewModel);

            return Task.CompletedTask;
        }
    }
}
