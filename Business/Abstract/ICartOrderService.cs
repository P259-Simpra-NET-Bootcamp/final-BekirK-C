using Base.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract;

public interface ICartOrderService
{
    IResult PlaceCartOrder(string couponCode);
    IDataResult<List<Order>> GetOrderDetails();
}
