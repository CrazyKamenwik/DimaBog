using FluentAssertions;
using SkyDrive.Tests.FixtureCustomization.Attributes;
using SkyDrive.ViewModels;
using System.Net;
using System.Net.Http.Json;

namespace SkyDrive.Tests.IntegrationTests.ControllerTests
{
    public class MemberControllerTests : IClassFixture<TestHttpClientFactory<Program>>
    {
        private HttpClient _client;

        public MemberControllerTests(TestHttpClientFactory<Program> appFactory)
        {
            _client = appFactory.CreateClient();
        }

        [Fact]
        public async Task GetAllMembers_ReturnOK()
        {
            var appFactory = new TestHttpClientFactory<Program>();
            var client = appFactory.CreateClient();

            //Act
            var response = await client.GetAsync("Member");
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<MemberViewModel>>();

            //Assert
            result.Should().NotBeNullOrEmpty();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetMemberById_CorrectId_ReturnMemberViewModel()
        {
            //Arrange
            var memberId = 1;

            //Act
            var response = await _client.GetAsync($"Member/{memberId}");
            var responseMemberViewModel = await response.Content.ReadFromJsonAsync<MemberViewModel>();

            //Assert
            responseMemberViewModel.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetMemberById_IncorrectId_ReturnNotFound()
        {
            //Arrange
            var memberId = 92;

            //Act
            var response = await _client.GetAsync($"Member/{memberId}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task PostMember_CorrectMember_ReturnOK(MemberViewModel memberViewModel)
        {
            //Arrange
            memberViewModel.Id = 93;

            //Act
            var response = await _client.PostAsJsonAsync("Member", memberViewModel);

            //Assert
            response.Content.ReadFromJsonAsync<MemberViewModel>().Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task PostMember_IncorrectMember_ReturnBadRequest(MemberViewModel memberViewModel)
        {
            //Arrange
            memberViewModel.Id = 4;
            memberViewModel.LastName = null!;

            //Act
            var response = await _client.PostAsJsonAsync("Member", memberViewModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task PostMember_MemberWithTheSameIdAlreadyExist_ReturnBadRequest(MemberViewModel memberViewModel)
        {
            //Arrange
            memberViewModel.Id = 5;

            //Act
            var response = await _client.PostAsJsonAsync("Member", memberViewModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task PutMember_CorrectMember_ReturnOK(MemberViewModel memberViewModel)
        {
            //Arrange
            memberViewModel.Id = 6;

            //Act
            var response = await _client.PutAsJsonAsync("Member", memberViewModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task PutMember_IncorrectMember_ReturnBadRequest(MemberViewModel memberViewModel)
        {
            //Arrange
            memberViewModel.Id = 7;
            memberViewModel.MiddleName = null!;

            //Act
            var response = await _client.PutAsJsonAsync("Member", memberViewModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task PutMember_MemberNotFound_ReturnNotFound(MemberViewModel memberViewModel)
        {
            //Arrange
            memberViewModel.Id = 98;

            //Act
            var response = await _client.PutAsJsonAsync("Member", memberViewModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteMember_CorrectId_ReturnOK()
        {
            //Arrange
            var memberId = 9;

            //Act
            var response = await _client.DeleteAsync($"Member/{memberId}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteMember_IncorrectId_ReturnNotFound()
        {
            //Arrange
            var memberId = 100;

            //Act
            var response = await _client.DeleteAsync($"Member/{memberId}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
