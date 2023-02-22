using AutoFixture;
using SkyDrive.ViewModels;

namespace SkyDrive.Tests.FixtureCustomization
{
    public class CorrectDataForValidatorsAttribute : FixtureWithoutCirculationAttribute
    {
        public CorrectDataForValidatorsAttribute()
        : this(new Fixture())
        { }

        public CorrectDataForValidatorsAttribute(IFixture fixture)
        : base(fixture)
        {
            fixture.Customize<InstructorViewModel>(composer => composer
                .With(p => p.FirstName, fixture.Create<string>()[..10])
                .With(p => p.MiddleName, fixture.Create<string>()[..17])
                .With(p => p.LastName, fixture.Create<string>()[..20])
                .With(p => p.Experience, fixture.Create<int>() % 20 + 1));

            fixture.Customize<MemberViewModel>(composer => composer
                .With(p => p.FirstName, fixture.Create<string>()[..10])
                .With(p => p.MiddleName, fixture.Create<string>()[..17])
                .With(p => p.LastName, fixture.Create<string>()[..20]));
        }
    }
}
