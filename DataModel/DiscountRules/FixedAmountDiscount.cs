namespace DataModel.DiscountRules
{
    public class FixedAmountDiscountRule : IDiscountRule
    {
        private decimal _threshold;
        private decimal _discountAmount;

        public FixedAmountDiscountRule(decimal threshold, decimal discountAmount)
        {
            _threshold = threshold;
            _discountAmount = discountAmount;
        }

        public decimal ApplyDiscount(Cart cart)
        {
            decimal subtotal = cart.Items.Sum(item => item.Product.Price * item.Quantity);
            return subtotal >= _threshold ? subtotal - _discountAmount : subtotal;
        }
    }
}
