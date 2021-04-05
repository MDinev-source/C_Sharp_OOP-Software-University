using NUnit.Framework;
using System;

namespace Store.Tests
{
    public class StoreManagerTests
    {
  
        private StoreManager storeManager;
        [SetUp]
        public void Setup()
        {
         
            storeManager = new StoreManager();
        }

        [Test]
        public void AddMethodShouldThrowExceptionWhenProductIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => storeManager.AddProduct(null));
        }

        [Test]
    
       
        public void AddMethodShouldThrowExceptionWhenQuantityIsBelowNull()
        {
          
            Assert.Throws<ArgumentException>(() => storeManager.AddProduct(new Product("a",-1,10)));
        }
        [Test]
        public void AddMethdoShouldWorkCorrectly()
        {
            storeManager.AddProduct(new Product("a",1,1));
            Assert.AreEqual(1, storeManager.Count);
        }

        [Test]
        public void BuyProductShouldThrowExceptionWhenProductIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => storeManager.BuyProduct("a", 3));
        }

        [Test]
        public void BuyProductShouldThrowExceptionWhenQuantityIsLess()
        {
            storeManager.AddProduct(new Product("Cola",2,10));
            Assert.Throws<ArgumentException>(() => storeManager.BuyProduct("Cola", 3));
        }
        [Test]
        public void BuyProductShouldWorkCorrectly()
        {
            storeManager.AddProduct(new Product("Cola",1,10));

            var expPrice = 10;
            var actPrice = storeManager.BuyProduct("Cola", 1);

            Assert.AreEqual(expPrice, actPrice);
        }

        [Test]
        public void BuyProductQuantityShouldWorkCorrectly()
        {
            var product = new Product("Cola", 1, 10);
            storeManager.AddProduct(product);

            var expQuantity = 0;

            storeManager.BuyProduct("Cola", 1);

            var actQuantity = product.Quantity;

            Assert.AreEqual(expQuantity, actQuantity);

        }

        [Test]
        public void GetTheMostExpensiveProductShouldWorkCorrectly()
        {
            var product = new Product("Cola", 1, 10);
            storeManager.AddProduct(product);

            var act = storeManager.GetTheMostExpensiveProduct();

            Assert.AreEqual(product, act);
        }

  
    }
}