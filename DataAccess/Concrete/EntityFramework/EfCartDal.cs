using Base.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework;

public class EfCartDal : EfGenericRepository<CartItem, SimpraProjectContext>, ICartDal
{
    public void RemoveFromCart(int customerId)
    {
        using (SimpraProjectContext context = new SimpraProjectContext())
        {
            var cartItems = context.Cart.Where(c => c.CustomerId == customerId).ToList();
            context.Cart.RemoveRange(cartItems);
            context.SaveChanges();
        }
    }
}
