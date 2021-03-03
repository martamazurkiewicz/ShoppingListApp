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

        [TestCase("supermarket")]
        [TestCase("Amazon")]
        [TestCase("drugstore")]
        [TestCase("Decathlon")]
        public void AddCategoryTest(string name)
        {
            _shoppingList.AddCategory(name);
            CollectionAssert.Contains(_shoppingList.GetCategories(), new Category(name));
        }

        [TestCase("bakery")]
        public void AddExistingCategoryTest(string name)
        {
            _shoppingList.AddCategory(name);
            Assert.Throws<CategoryAlreadyExistsException>(() => { _shoppingList.AddCategory(name); });
        }

        [TestCase("Clean Code", "Amazon")]
        public void AddExistingProductTest(string name, string categoryName)
        {
            var category = _shoppingList.GetCategory(categoryName);
            _shoppingList.AddProduct(category, name);
            Assert.Throws<ProductAlreadyExistsException>(() => { _shoppingList.AddProduct(category, name); });
        }

        [TestCase("Vinted")]
        public void RemoveCategoryTest(string name)
        {
            _shoppingList.AddCategory(name);
            _shoppingList.RemoveCategory(name);
            CollectionAssert.DoesNotContain(_shoppingList.GetCategories(), new Category(name));
        }
        
        [TestCase("supermarket")]
        [TestCase("Amazon")]
        public void GetCategoryTest(string name)
        {
            var category = _shoppingList.GetCategory(name);
            Assert.AreEqual(new Category(name), category);
        }

        [TestCase("almond flour", "supermarket")]
        [TestCase("apricots", "supermarket")]
        [TestCase("bandages", "drugstore")]
        [TestCase("Grand Tour Season 2 DVD", "Amazon")]
        [TestCase("protein bars", "Decathlon")]
        public void AddProductTest(string name, string categoryName)
        {
            var category = _shoppingList.GetCategory(categoryName);
            _shoppingList.AddProduct(name, category);
            CollectionAssert.Contains(_shoppingList.GetProductNames(category), name);
        }

        [TestCase("supermarket", "market")]
        public void UpdateCategoryTest(string oldName, string newName)
        {
            var category = _shoppingList.GetCategory(oldName);
            var productNames = _shoppingList.GetProductNames(category);
            category.UpdateCategory(newName);
            category = _shoppingList.GetCategory(newName);
            CollectionAssert.AreEquivalent(productNames, _shoppingList.GetProductNames(category));
        }

        [TestCase("protein powder", "Decathlon")]
        [TestCase("apples", "market")]
        public void RemoveProductTest(string name, string categoryName)
        {
            var category = _shoppingList.GetCategory(categoryName);
            _shoppingList.AddProduct(category, name);
            _shoppingList.RemoveProduct(category, name);
            CollectionAssert.DoesNotContain(_shoppingList.GetProductNames(category), name);
        }

        [TestCase("protein bars", "Decathlon", "market")]
        [TestCase("painkiller", "market", "drugstore")]
        public void ChangeProductCategoryTest(string name, string oldCategoryName, string newCategoryName)
        {
            var oldCategory = _shoppingList.GetCategory(oldCategoryName);
            var newCategory = _shoppingList.GetCategory(newCategoryName);
            _shoppingList.ChangeProductCategory(name, oldCategory, newCategory);
            CollectionAssert.DoesNotContain(_shoppingList.GetProductNames(oldCategory), name);
            CollectionAssert.Contains(_shoppingList.GetProductsNames(newCategory), name);
        }

        
    }
}