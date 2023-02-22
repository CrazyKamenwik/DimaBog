using AutoFixture;

namespace SkyDrive.Tests.FixtureCustomization
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
