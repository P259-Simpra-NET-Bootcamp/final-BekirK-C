using AutoMapper;
using Base.Utilities.Results;
using Business.Abstract;
using Business.Constants;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models.Requests;
using Entities.Models.Responses;

namespace Business.Concrete;

public class CartManager : ICartService
{
    private readonly ICartDal _cartDal;
    private readonly IProductDal _productDal;
    private readonly IMapper _mapper;
    private readonly IUserService _userManager;
    
    public CartManager(ICartDal cartRepository, IProductDal productRepository, IMapper mapper, IUserService userManager)
    {
        _cartDal = cartRepository;
        _productDal = productRepository;
        _mapper = mapper;
        _userManager = userManager;
    }
    public IResult AddToCart(CartItemRequest cartItemRequest)
    {
        var cartItem = _mapper.Map<CartItemRequest, CartItem>(cartItemRequest);
        var product = _productDal.Get(p => p.Id == cartItem.ProductId);

        if (product == null)
            return new ErrorResult(CartMessages.ProductNotFound);

        if (cartItem.Quantity <= 0)
            return new ErrorResult(CartMessages.QuantityGreaterThanZero);

        if (product.Stock < cartItem.Quantity)
            return new ErrorResult(CartMessages.InsufficientStock);

        var customerId = _userManager.GetCustomerIdFromAccessToken();
        cartItem.CustomerId = customerId;
        var existingCartItem = _cartDal.Get(c => c.ProductId == cartItem.ProductId && c.CustomerId == customerId);
        cartItem.TotalPrice = product.Price * cartItem.Quantity;

        if (existingCartItem != null)
            return UpdateCartItem(existingCartItem, cartItem);

        cartItem.ProductName = product.Name;
        cartItem.UnitPrice = product.Price;
        cartItem.CreatedAt = DateTime.UtcNow;

        _cartDal.Add(cartItem);
        return new SuccessResult(CartMessages.ProductAddedToCart);
    }

    public IResult DecrementQuantityInCart(CartItemRequest cartItemRequest)
    {
        var cartItem = _mapper.Map<CartItemRequest, CartItem>(cartItemRequest);
        var customerId = _userManager.GetCustomerIdFromAccessToken();
        var existingCartItem = _cartDal.Get(c => c.ProductId == cartItem.ProductId && c.CustomerId == customerId);

        if (existingCartItem == null)
            return new ErrorResult(CartMessages.CartItemNotFound);

        if (cartItem.Quantity > existingCartItem.Quantity)
            return new ErrorResult(CartMessages.QuantityExceedsCartQuantity);

        var product = _productDal.Get(p => p.Id == existingCartItem.ProductId);

        if (product == null)
            return new ErrorResult(CartMessages.ProductNotFound);


        existingCartItem.Quantity -= cartItem.Quantity;
        existingCartItem.TotalPrice = product.Price * existingCartItem.Quantity;

        if (existingCartItem.Quantity == 0)
        {
            RemoveProductFromCart(existingCartItem.ProductId);
            return new SuccessResult(CartMessages.ProductRemovedFromCart);
        }

        _cartDal.Update(existingCartItem);
        return new SuccessResult(CartMessages.QuantityDecrementedInCart);
    }

    public IResult RemoveAllFromCart()
    {
        _cartDal.RemoveFromCart(_userManager.GetCustomerIdFromAccessToken());
        return new SuccessResult(CartMessages.CartCompletelyEmptied);
    }

    public IResult RemoveProductFromCart(int productId)
    {
        var customerId = _userManager.GetCustomerIdFromAccessToken();
        var cartItem = _cartDal.Get(c => c.ProductId == productId && c.CustomerId == customerId);

        if (cartItem == null)
            return new ErrorResult(CartMessages.CartItemNotFound);

        _cartDal.Delete(cartItem);
        return new SuccessResult(CartMessages.ProductRemovedFromCart);
    }

    private IResult UpdateCartItem(CartItem existingCartItem, CartItem newCartItem)
    {
        existingCartItem.Quantity += newCartItem.Quantity;
        existingCartItem.TotalPrice += newCartItem.TotalPrice;

        _cartDal.Update(existingCartItem);
        return new SuccessResult(CartMessages.ProductQuantityUpdatedInCart);
    }

    public IDataResult<List<CartItemResponse>> GetCartDetails()
    {
        var customerId = _userManager.GetCustomerIdFromAccessToken();
        var cartItems = _cartDal.GetAll(o => o.CustomerId == customerId);
        var mappedCartItem = _mapper.Map<List<CartItem>, List<CartItemResponse>>(cartItems);

        if (cartItems == null || cartItems.Count == 0)
            return new ErrorDataResult<List<CartItemResponse>>(CartMessages.NotAnyCartItem);

        return new SuccessDataResult<List<CartItemResponse>>(mappedCartItem);
    }
}
