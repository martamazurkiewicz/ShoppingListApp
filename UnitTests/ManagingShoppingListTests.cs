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
            DeleteDefaultAppFile();
            _shoppingList = new ShoppingList();
        }

        private void DeleteDefaultAppFile()
        {
            var file = @"shoppingList.txt";
            if (File.Exists(file))
                File.Delete(file);
        }

        [TestCase("market")]
        [TestCase("drugstore")]
        [TestCase("Amazon")]
        public void AddingCategoryTest(string name)
        {
            _shoppingList.AddCategory(name);
            Assert.Contains(new Category(name), _shoppingList.GetCategories());
        }
    }
}