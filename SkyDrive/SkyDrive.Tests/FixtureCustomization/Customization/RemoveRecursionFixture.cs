using AutoFixture;

namespace SkyDrive.Tests.FixtureCustomization.Customization
{
    public class RemoveRecursionFixture : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
