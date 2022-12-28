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
        public void WhenUnitHasHealth_5_AndTakingDamage_2_ThenHealthShouldBe_3_AndTookDamageEventCalled()
        {
            // Arrange
            var wasTookDamageEventCalled = false;
            IUnit unit = new UnitModel(5);

            // Act
            unit.OnTookDamage += _ => wasTookDamageEventCalled = true;
            unit.TakeDamage(2);

            // Assert
            unit.Health.Should().Be(3);
            wasTookDamageEventCalled.Should().Be(true);
        }

        [Test]
        public void WhenUnitHasHealth_10_AndTaking12Damage_ThenUnitShouldBeDestroyed()
        {
            // Arrange
            var wasDestroyedEventCalled = false;
            IUnit unit = new UnitModel(5);

            // Act
            unit.OnDestroyed += () => wasDestroyedEventCalled = true;
            unit.TakeDamage(12);

            // Assert
            wasDestroyedEventCalled.Should().Be(true);
        }
    }
}