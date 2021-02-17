using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;
using ShoppingListApp.Entities;
namespace UnitTests
{
    public class CreatingFileTests
    {
        [TestCase]
        public void CreateNewFile()
        {
            var shoppingListFile = new ShoppingList();
            List<Product> products = shoppingListFile.GetProducts();
            Assert.AreEqual(new List<Product>(), products);
        }

        [TestCase(@"C:\Repo\ShoppingListApp\UnitTests\bin\shoppingList.txt")]
        public void LoadFile(string path)
        {
            var products = new List<Product>
            {
                new Product("AAA batteries", new Category("Ebay")),
                new Product("spinach", new Category("market")),
                new Product("coffee", new Category("super market"))
            };
            CreateJsonFile(path, products);
            var shoppingListFile = new ShoppingList(path);
            Assert.AreEqual(products, shoppingListFile.GetProducts());
        }

        [TestCase]
        public void CreateNewFileWhenFileAlreadyExists()
        {
            CreateJsonFile("shoppingList.txt", new List<Product>());
            Assert.Throws<FileAreadyExistsException>(() =>
            {
                new ShoppingList();
            });
        }
        
        private void CreateJsonFile(string path, List<Product> products)
        {
            using (StreamWriter file = File.CreateText(path))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, products);
            }
        }
    }
    
}