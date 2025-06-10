namespace DataModel.DiscountRules
{
    public class BuyXGetYFreeRule : IDiscountRule
    {
        private string _sku;
        private int _x;
        private int _y;

        public BuyXGetYFreeRule(string sku, int x, int y)
        {
            _sku = sku;
            _x = x;
            _y = y;
        }

        public decimal ApplyDiscount(Cart cart)
        {
            decimal total = 0;
            foreach (var item in cart.Items)
            {
                int freeItems = (item.Product.SKU == _sku) ? (item.Quantity / (_x + _y)) * _y : 0;
                total += item.Product.Price * (item.Quantity - freeItems);
            }
            return total;
        }
    }
}
