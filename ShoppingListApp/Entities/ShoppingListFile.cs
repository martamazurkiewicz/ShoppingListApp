using System.Collections.Generic;

namespace ShoppingListApp.Entities
{
    public class ShoppingListFile
    {
        private List<Category> _categories;

        public ShoppingListFile()
        {
            _categories = new List<Category>();
        }

        public void AddProductToShoppingList(Category category, string productName)
        { 
            //TODO
        }

        public void RemoveProductFromShoppingList(Product product) => product.category.RemoveProduct(product);

        public void AddCategory(Category category) => _categories.Add(category);
        
        public void RemoveCategory(Category category)
        {
            //TODO - cascade removal of products
        }

        public List<Product> GetAllProductsFromCategory(Category category) => category.Products;

        public void SynchronizeShoppingLists(string nameOfShoppingListFileToSynchronizeWith)
        {
            //TODO
        }

        public void SaveShoppingList()
        {
            //TODO -> serialize to file
        }
        
        }
}