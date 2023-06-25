using Base.Utilities.Results;
using Business.Abstract;
using Business.Constants;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class CartOrderManager : ICartOrderService
{
    private readonly ICartDal _cartDal;
    private readonly IProductDal _productDal;
    private readonly IUserDal _userDal;
    private readonly IOrderDal _orderDal;
    private readonly IProductService _productManager;
    private readonly IUserService _userManager;
    private readonly ICouponService _couponManager;

    public CartOrderManager(ICartDal cartDal, IProductDal productDal, IUserDal userDal, IOrderDal orderDal, IProductService productManager, IUserService userManager, ICouponService couponManager)
    {
        _cartDal = cartDal;
        _productDal = productDal;
        _userDal = userDal;
        _orderDal = orderDal;
        _productManager = productManager;
        _userManager = userManager;
        _couponManager = couponManager;
    }

    public IResult PlaceCartOrder(string couponCode)
    {
        var customerId = _userManager.GetCustomerIdFromAccessToken();
        var customer = _userDal.Get(u => u.Id == customerId);
        var cartItems = _cartDal.GetAll(c => c.CustomerId == customerId);
        var coupon = _couponManager.GetCouponByCode(couponCode);

        if (cartItems == null || cartItems.Count == 0)
            return new ErrorResult(OrderMessages.CartIsEmpty);

        var orderList = new List<Order>();

        var uniqueOrderId = GenerateOrderNumber();

        foreach (var cartItem in cartItems)
        {
            var product = _productManager.GetById(cartItem.ProductId);

            if (product == null)
                return new ErrorResult(OrderMessages.ProductNotFound);

            if (product.Data.Stock < cartItem.Quantity)
                return new ErrorResult(OrderMessages.InsufficientStock);

            var order = new Order
            {
                ProductId = cartItem.ProductId,
                CustomerId = customer.Id,
                ShippingAddress = customer.ShippingAddress,
                OrderDate = DateTime.Now,
                Quantity = cartItem.Quantity,
                TotalPrice = product.Data.Price * cartItem.Quantity,
                CouponCode = couponCode,
                CouponAmount = coupon != null ? coupon.Data?.Discount : null,
                SpendPoint = customer.EarnedPoints,
                OrderId = uniqueOrderId,
            };

            orderList.Add(order);
        }

        decimal totalPrice = orderList.Sum(o => o.TotalPrice);
        decimal discountedPrice = _couponManager.ApplyCouponDiscount(totalPrice, couponCode);

        if (couponCode != null && discountedPrice == -1)
            return new ErrorResult(OrderMessages.InvalidCouponCode);

        decimal moneyPoints = customer.EarnedPoints;
        decimal remainingAmount = discountedPrice - moneyPoints;

        if (customer.VirtualWallet < remainingAmount)
        {
            decimal insufficientAmount = totalPrice - customer.VirtualWallet;
            return new ErrorResult(OrderMessages.InsufficientBalance(insufficientAmount));
        }

        customer.VirtualWallet -= remainingAmount;
        customer.EarnedPoints -= moneyPoints;

        _userDal.Update(customer);

        foreach (var order in orderList)
        {
            decimal discount = 0;

            if (coupon.Success)
            {
                discount = coupon.Data.Discount;
                coupon.Data.IsActive = false;
                _couponManager.UpdateCoupon(coupon.Data);
            }

            var product = _productDal.Get(p => p.Id == order.ProductId);
            decimal earnedPoints = _userManager.CalculateEarnedPoints(order.TotalPrice - ((discount / orderList.Count) + (moneyPoints / orderList.Count)), product.PointPercentage, product.MaxPoint);

            _orderDal.Add(order);
            _userManager.AddEarnedPointsToUser(customer, earnedPoints);
            product.Stock -= order.Quantity;
            _productDal.Update(product);
        }

        _cartDal.RemoveFromCart(customer.Id);
        return new SuccessResult(OrderMessages.OrderPlacedSuccessfully);
    }

    public IDataResult<List<Order>> GetOrderDetails()
    {
        var customerId = _userManager.GetCustomerIdFromAccessToken();
        var orders = _orderDal.GetAll(o => o.CustomerId == customerId);

        if (orders == null || orders.Count == 0)
            return new ErrorDataResult<List<Order>>(OrderMessages.NotAnyOrder);

        return new SuccessDataResult<List<Order>>(orders);
    }

    public string GenerateOrderNumber()
    {
        var guid = Guid.NewGuid();
        var hash = guid.GetHashCode();
        var absoluteHash = Math.Abs(hash);
        var orderId = absoluteHash.ToString().PadLeft(9, '0');

        if (orderId.Length > 9)
            orderId = orderId.Substring(0, 9);

        return orderId;
    }
}