namespace ShoppingListApp.Entities
{
    public class Product
    {
        public Category category { get; }
        private readonly string _name;

        public Product(string name, Category category)
        {
            _name = name;
            this.category = category;
        }

        public override string ToString() => _name;
    }
}