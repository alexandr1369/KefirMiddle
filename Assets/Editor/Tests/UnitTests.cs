using FluentAssertions;
using Model;
using NUnit.Framework;

namespace Editor.Tests
{
    public class UnitTests
    {
        [Test]
        public void WhenCreatingUnit_ThenHealthShouldBe_0()
        {
            // Arrange
            var unit = new UnitModel();

            // Assert
            unit.Health.Should().Be(0);
        }
        [Test]
        public void WhenUnitHasHealth_5_AndTakingDamage_2_ThenHealthShouldBe_3()
        {
            // Arrange
            IUnit unit = new UnitModel(5);

            // Act
            unit.TakeDamage(2);

            // Assert
            unit.Health.Should().Be(3);
        }
    }
}