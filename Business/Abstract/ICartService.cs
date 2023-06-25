using Base.Utilities.Results;
using Entities.Concrete;
using Entities.Models.Requests;
using Entities.Models.Responses;

namespace Business.Abstract;

public interface ICartService
{
    IResult AddToCart(CartItemRequest cartItemRequest);
    IResult RemoveProductFromCart(int productId);
    IResult RemoveAllFromCart();
    IResult DecrementQuantityInCart(CartItemRequest cartItemRequest);
    IDataResult<List<CartItemResponse>> GetCartDetails();
}
