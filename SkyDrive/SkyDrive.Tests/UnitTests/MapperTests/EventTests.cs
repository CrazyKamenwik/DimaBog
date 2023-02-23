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
        public void AdaptFromModelToEntity_ReturnEntity(EventModel eventModel)
        {
            //Act
            var eventEntity = eventModel.Adapt<EventEntity>();

            //Assert
            eventEntity.Should().BeEquivalentTo(eventModel);
        }

        [Theory]
        [FixtureWithoutCirculation]
        public void AdaptFromEntityToModel_ReturnModel(EventEntity eventEntity)
        {
            //Act
            var eventModel = eventEntity.Adapt<EventModel>();

            //Assert
            eventModel.Should().BeEquivalentTo(eventEntity);
        }

        [Theory]
        [FixtureWithoutCirculation]
        public void AdaptFromModelToViewModel_ReturnViewModel(EventModel eventModel)
        {
            //Act
            var eventViewModel = eventModel.Adapt<EventViewModel>();

            //Assert
            eventViewModel.Should().BeEquivalentTo(eventModel);
        }

        [Theory]
        [FixtureWithoutCirculation]
        public void AdaptFromViewModelToModel_ReturnModel(EventViewModel eventViewModel)
        {
            //Act
            var eventModel = eventViewModel.Adapt<EventModel>();

            //Assert
            eventModel.Should().BeEquivalentTo(eventViewModel);
        }
    }
}
