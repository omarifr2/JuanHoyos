namespace DataModel
{
    public class Product
    {
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }

        public Product(string sku, string name, string category, decimal price)
        {
            SKU = sku;
            Name = name;
            Category = category;
            Price = price;
        }
    }
}
