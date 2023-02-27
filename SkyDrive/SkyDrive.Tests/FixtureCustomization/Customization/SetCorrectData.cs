using AutoFixture;
using SkyDrive.ViewModels;

namespace SkyDrive.Tests.FixtureCustomization.Customization
{
    internal class SetCorrectData : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<InstructorViewModel>(composer => composer
                .With(p => p.FirstName, fixture.Create<string>()[..10])
                .With(p => p.MiddleName, fixture.Create<string>()[..17])
                .With(p => p.LastName, fixture.Create<string>()[..20])
                .With(p => p.Experience, fixture.Create<int>() % 20 + 1)
                .With(p => p.Events, value: null));

            fixture.Customize<MemberViewModel>(composer => composer
                .With(p => p.FirstName, fixture.Create<string>()[..5])
                .With(p => p.MiddleName, fixture.Create<string>()[..12])
                .With(p => p.LastName, fixture.Create<string>()[..16])
                .With(p => p.Events, value: null));

            fixture.Customize<EventViewModel>(composer => composer
                .With(p => p.Members, value: null)
                .With(p => p.Instructor, value: null)
                .With(p => p.DateTimeOfEvent, value: DateTime.Now.AddDays(50)));
        }
    }
}
