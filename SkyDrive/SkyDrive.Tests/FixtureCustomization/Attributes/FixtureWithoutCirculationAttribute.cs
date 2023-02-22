using AutoFixture;
using AutoFixture.Xunit2;
using SkyDrive.Tests.FixtureCustomization.Customization;

namespace SkyDrive.Tests.FixtureCustomization.Attributes
{
    public class FixtureWithoutCirculationAttribute : AutoDataAttribute
    {
        public FixtureWithoutCirculationAttribute()
        : this(new Fixture())
        { }

        public FixtureWithoutCirculationAttribute(IFixture fixture)
            : base(fixture)
        {
            fixture.Customize(new RemoveRecursionFixture());
        }
    }
}
