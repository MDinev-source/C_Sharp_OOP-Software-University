namespace Presents.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    [TestFixture]
    public class PresentsTests
    {
        private Bag bag;
        private Present present;

        [SetUp]
        public void Setup()
        {
            bag = new Bag();
            present = new Present("TestName", 20);
        }

        [Test]
        public void CreateShouldThrowExceptionWhenPresentIsNull()
        {
            Assert.Throws<ArgumentNullException>(()
                => bag.Create(null));
        }

        [Test]
        public void CreateShouldThrowExceptionWhenPresentIsAlreadyExist()
        {
            bag.Create(present);

            Assert.Throws<InvalidOperationException>(()
                => bag.Create(present));
        }

        [Test]
        public void CreateShouldAddPresentCorrectly()
        {
            bag.Create(present);

            var expCount = 1;
            var actCount = bag.GetPresents().Count;

            Assert.AreEqual(expCount, actCount);
        }

        [Test]
        public void RemoveShouldRemovePresentCorrectly()
        {
            bag.Create(present);

            bool result = bag.Remove(present);

            Assert.IsTrue(result);
        }

        [Test]
        public void GetPresentWithLeastMagicShouldReturnPresentWithLeastMagic()
        {
            bag.Create(present);

            Present presentSecond = new Present("TestSecond", 10);

            bag.Create(presentSecond);

            var expPresent = presentSecond;

            var actPresent = bag.GetPresentWithLeastMagic();

            Assert.AreEqual(expPresent, actPresent);
        }

        [Test]
        public void GetPresentShoulReturnPresentWithGivenName()
        {
            bag.Create(present);

            var expPresent = present;

            var actPresent = bag.GetPresent("TestName");

            Assert.AreEqual(expPresent, actPresent);
        }
    }
}
