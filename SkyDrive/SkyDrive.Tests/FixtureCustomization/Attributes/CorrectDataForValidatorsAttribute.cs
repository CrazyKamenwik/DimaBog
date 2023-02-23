using AutoFixture;
using SkyDrive.Tests.FixtureCustomization.Customization;

namespace SkyDrive.Tests.FixtureCustomization.Attributes
{
    public class CorrectDataForValidatorsAttribute : FixtureWithoutCirculationAttribute
    {
        public CorrectDataForValidatorsAttribute()
        : this(new Fixture())
        { }

        public CorrectDataForValidatorsAttribute(IFixture fixture)
        : base(fixture)
        {
            fixture.Customize(new SetCorrectData());
        }
    }
}
