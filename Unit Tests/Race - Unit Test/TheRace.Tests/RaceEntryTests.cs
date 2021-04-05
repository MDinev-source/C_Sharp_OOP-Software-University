using NUnit.Framework;
using System;
using TheRace;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {
        private RaceEntry raceEntry;
        private UnitCar unitCar;
        private UnitDriver unitDriver;

        [SetUp]
        public void Setup()
        {
            this.raceEntry = new RaceEntry();
            this.unitCar = new UnitCar("Test", 10, 3000);
            this.unitDriver = new UnitDriver("TestDriver", unitCar);
        }

        [Test]
        public void methodAddDriverThrowExceptionWhenDriverIsNull()
        {
            Assert.Throws<InvalidOperationException>(()=>raceEntry.AddDriver(null));
        }

        [Test]
        public void methodAddDriverThrowExceptionWhenContainsDriver()
        {
            raceEntry.AddDriver(unitDriver);
            Assert.Throws<InvalidOperationException>(() => raceEntry.AddDriver(unitDriver));
        }

        [Test]
        public void methodAddDriverWorkCorrectly()
        {
            raceEntry.AddDriver(unitDriver);
            Assert.AreEqual(raceEntry.Counter, 1);
        }

        [Test]
        public void methodAddReturnCorrectResult()
        {
            var act=raceEntry.AddDriver(unitDriver);
            var expResult = "Driver TestDriver added in race.";
            Assert.AreEqual(expResult, act);
        }

        [Test]
        public void methodCalculateAverageHorsePowerThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => raceEntry.CalculateAverageHorsePower());
        }


        [Test]
        public void methodCalculateAverageHorsePowerWorkCorrectly()
        {
            raceEntry.AddDriver(unitDriver);
            raceEntry.AddDriver(new UnitDriver("TestDriverSecond", new UnitCar("a", 10, 5000)));

            var act = raceEntry.CalculateAverageHorsePower();
            var exp = 10;
            Assert.AreEqual(exp, act);

        }

        [Test]
        public void CtorUnitCarWorkCorrectly()
        {
            Assert.AreEqual("Test", unitCar.Model);
            Assert.AreEqual(10, unitCar.HorsePower);
            Assert.AreEqual(3000, unitCar.CubicCentimeters);
        }
        [Test]
        public void CtorUniteDriverWork()
        {

            Assert.AreEqual("TestDriver", unitDriver.Name);
            Assert.AreEqual(unitCar, unitDriver.Car);
        }

        [Test]
        public void UnitDriverInvaliName()
        {
            Assert.Throws<ArgumentNullException>(()
                => new UnitDriver(null, unitCar));
        }
    }
}