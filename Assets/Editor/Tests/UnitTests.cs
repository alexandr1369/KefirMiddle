using FluentAssertions;
using Model;
using NUnit.Framework;

namespace Editor.Tests
{
    public class UnitTests
    {
        [Test]
        public void WhenCreatingUnit_ThenHealthShouldBe_1()
        {
            // Arrange
            var unit = new UnitModel();

            // Assert
            unit.Health.Should().Be(1);
        }
        [Test]
        public void WhenUnitHasHealth_5_AndTakingDamage_2_ThenHealthShouldBe_3_AndTookDamageEventCalled()
        {
            // Arrange
            var wasTookDamageEventCalled = false;
            IUnitModel unitModel = new UnitModel(5);

            // Act
            unitModel.OnTookDamage += _ => wasTookDamageEventCalled = true;
            unitModel.TakeDamage(2);

            // Assert
            unitModel.Health.Should().Be(3);
            wasTookDamageEventCalled.Should().Be(true);
        }

        [Test]
        public void WhenUnitHasHealth_10_AndTaking12Damage_ThenUnitShouldBeDestroyed()
        {
            // Arrange
            var wasDestroyedEventCalled = false;
            IUnitModel unitModel = new UnitModel(5);

            // Act
            unitModel.OnDestroyed += () => wasDestroyedEventCalled = true;
            unitModel.TakeDamage(12);

            // Assert
            wasDestroyedEventCalled.Should().Be(true);
        }
    }
}