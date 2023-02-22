using AutoFixture;
using AutoFixture.Xunit2;

namespace SkyDrive.Tests.FixtureCustomization
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
