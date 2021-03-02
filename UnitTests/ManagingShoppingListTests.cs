using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using ShoppingListApp.Entities;

namespace UnitTests
{
    public class ManagingShoppingListTests
    {
        private ShoppingList _shoppingList;

        [SetUp]
        public void Setup()
        {
            RemoveDefaultAppFile();
            _shoppingList = new ShoppingList();
        }

        private void RemoveDefaultAppFile()
        {
            var file = @"shoppingList.txt";
            if (File.Exists(file))
                File.Delete(file);
        }

        [TestCase("market")]
        [TestCase("Amazon")]
        [TestCase("drugstore")]
        [TestCase("Decathlon")]
        public void AddCategoryTest(string name)
        {
            _shoppingList.AddCategory(name);
            Assert.Contains(new Category(name), _shoppingList.GetCategories());
        }

        [TestCase("Vinted")]
        public void RemoveCategoryTest(string name)
        {
            _shoppingList.AddCategory(name);
            _shoppingList.RemoveCategory(name);
            CollectionAssert.DoesNotContain(_shoppingList.GetCategories(), new Category(name));
        }
        
        [TestCase("market")]
        [TestCase("Amazon")]
        public void GetCategoryTest(string name)
        {
            var category = _shoppingList.GetCategory(categoryName);
            Assert.AreEqual(new Category(name), category);
        }

        [TestCase("almond flour", "market")]
        [TestCase("apricots", "market")]
        [TestCase("bandages", "drugstore")]
        [TestCase("Grand Tour Season 2 DVD", "Amazon")]
        public void AddProductTest(string name, string categoryName)
        {
            var category = _shoppingList.GetCategory(categoryName);
            var product = new Product(name);
            _shoppingList.AddProduct(name, category);
            Assert.Contains(product, _shoppingList.GetProductsWithCategory(category));
        }
        
        [TestCase("protein powder", "Decathlon")]
        [TestCase("apples", "market")]
        public void RemoveProductTest(string name, string categoryName)
        {
            var category = _shoppingList.GetCategory(categoryName);
            var product = new Product(name);
            _shoppingList.AddProduct(name, category);
            _shoppingList.RemoveProduct(name, category);
            CollectionAssert.DoesNotContain(_shoppingList.GetProductsWithCategory(category), product);
        }

        [TestCase("protein bars", "Decathlon", "market")]
        [TestCase("painkiller", "market", "drugstore")]
        public void ChangeProductCategoryTest(string name, string oldCategoryName, string newCategoryName)
        {
            var oldCategory = _shoppingList.GetCategory(oldCategoryName);
            var category = _shoppingList.GetCategory(oldCategoryName);
            var product = new Product(name, category);
            _shoppingList.ChangeProductsCategory(oldCategory);
            _shoppingList.RemoveProduct(product);
        }

        
    }
}