﻿using AutoFixture;
using AutoFixture.Xunit2;

namespace SkyDrive.Tests.FixtureCustomization
{
    public class FixtureWithoutCirculationAttribute : AutoDataAttribute
    {
        public FixtureWithoutCirculationAttribute()
            : base(() =>
            {
                var fixture = new Fixture();

                fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
                fixture.Behaviors.Add(new OmitOnRecursionBehavior());

                return fixture;
            })
        { }
    }
}
