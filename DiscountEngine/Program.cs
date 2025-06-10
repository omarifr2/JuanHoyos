using DataModel;
using DataModel.DiscountRules;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var discountRepo = new DiscountRepository();

// 1. Percentage Discount - 10% off globally
discountRepo.AddDiscount(new PercentageDiscountRule(10));

// 2. Fixed Amount Discount - $20 off when subtotal exceeds $100
discountRepo.AddDiscount(new FixedAmountDiscountRule(100, 20));

// 3. Buy X Get Y Free - Buy 2 get 1 free on T-SHIRT
discountRepo.AddDiscount(new BuyXGetYFreeRule("T-SHIRT", 2, 1));

// 4. Coupon Code - "WELCOME10" gives 10% off
discountRepo.AddDiscount(new CouponDiscountRule("WELCOME10", 10));

// 5. Category Discount - 15% off "Books"
discountRepo.AddDiscount(new CategoryDiscountRule("Books", 15));



// Register Discount Repository and Engine as singletons
builder.Services.AddSingleton(discountRepo);
builder.Services.AddSingleton<DiscountEngineCore>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
