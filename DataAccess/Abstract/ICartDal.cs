using Base.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface ICartDal : IGenericRepository<CartItem>
{
    void RemoveFromCart(int customerId);
}
