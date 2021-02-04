using System.Collections.Generic;

namespace ShoppingListApp.Entities
{
    public class Category
    {
        private readonly string _name;
        public List<Product> Products { get; }

        public Category(string name)
        {
            _name = name;
            Products = new List<Product>();
        }

        public void RemoveProduct(Product product) => Products.Remove(product);
        public override string ToString() => _name;
    }
}