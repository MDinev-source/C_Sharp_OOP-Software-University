using NUnit.Framework;
using System;

namespace Computers.Tests
{
    public class Tests
    {
        private Computer computer;
        private ComputerManager computerManager;

        [SetUp]
        public void Setup()
        {
            this.computer = new Computer("Test", "Test", 500);
            this.computerManager = new ComputerManager();
        }

        //[Test]
        //public void ComputerCtorShouldWorkCorrectly()
        //{
        //    var expManufacturer = "Test";
        //    var expModel = "Test";
        //    var expPrice = 500;

        //    var actManufacturer = computer.Manufacturer;
        //    var actModel = computer.Model;
        //    var actPrice = computer.Price;

        //    Assert.AreEqual(expManufacturer, actManufacturer);
        //    Assert.AreEqual(expModel, actModel);
        //    Assert.AreEqual(expPrice, actPrice);
        //}

        [Test]
        public void AddMethodShouldThrowNullExceptionWhenComputerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => computerManager.AddComputer(null));
        }
        [Test]
        public void AddMethodShouldThrowExceptionWhenComputerExist()
        {
            computerManager.AddComputer(computer);

            Assert.Throws<ArgumentException>(() => computerManager.AddComputer(computer));
        }
        [Test]
        public void AddMEthodShouldWorkCorrectly()
        {
            computerManager.AddComputer(computer);

            var expCount = 1;
            var actCount = computerManager.Count;

            Assert.AreEqual(expCount, actCount);
        }

        [Test]
        public void RemoveMethodShouldThrowExceptionWhenManufacturerIsNull()
        {
            Assert.Throws<ArgumentNullException>(()=>computerManager.RemoveComputer(null, "Test"));
        }
        [Test]
        public void RemoveMethodShouldThrowExceptionWhenModelIsNull()
        {
            Assert.Throws<ArgumentNullException>(()=>computerManager.RemoveComputer("Test", null));
        }
        [Test]
        public void RemoveMethodShouldWorkCorrectly()
        {
            this.computerManager.AddComputer(this.computer);
            var test = this.computerManager.RemoveComputer("Test", "Test");
            Assert.That(test.Model == "Test" && test.Manufacturer == "Test");
        }


        [Test]
        public void GetComputerMethodShouldThrowExceptionWhenManufacturerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => computerManager.RemoveComputer(null, "Test"));
        }
        [Test]
        public void GetComputerMethodShouldThrowExceptionWhenModelIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => computerManager.RemoveComputer("Test", null));
        }
        [Test]
        public void GetComputerMethodShouldThrowExceptionWhenComputerIsNull()
        {
            Assert.Throws<ArgumentException>(() => computerManager.GetComputer("a", "a"));
        }
        [Test]
        public void GetComputerMethodShouldWorkCorrectly()
        {
            computerManager.AddComputer(computer);

            var expComputer = computer;

            var actComputer = computerManager.GetComputer("Test", "Test");

            Assert.AreEqual(expComputer, actComputer);
        }

        [Test]
        public void GetComputerByManufacturerShouldThrowExceptionWhenManufacturerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => computerManager.GetComputersByManufacturer(null));
        }
        [Test]
        public void GetComputerByManufacturerShouldWorkCorrectly()
        {
            computerManager.AddComputer(computer);

            var expCount = 1;
            var actCount = computerManager.GetComputersByManufacturer("Test").Count;

            Assert.AreEqual(expCount, actCount);
        }
    }
}