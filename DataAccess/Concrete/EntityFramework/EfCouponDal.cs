using Base.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework;

public class EfCouponDal : EfGenericRepository<Coupon, SimpraProjectContext>, ICouponDal
{
}
