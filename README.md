This project is a flexible discount engine for an e-commerce platform, focusing on backend logic for various discount types applied to a shopping cart system. The engine supports multiple discount rules and ensures accurate calculations through unit testing.
Features
- Shopping Cart System with Product, CartItem, and Cart models.
- Discount Types:
- Percentage Discount (applies to all products or a specific category).
- Fixed Amount Discount (applies when cart total exceeds a threshold).
- Buy X Get Y Free (e.g., Buy 2 Get 1 Free for selected SKUs).
- Coupon Code Discount (applies based on promo codes).
- Category Discount (applies a percentage discount to a specific product category).
- Discount Repository for centralized discount rule storage.
- Unit Testing (using xUnit & Moq for robust validations).
