using Base.DataAccess.EntityFramework;
using Base.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Context;

namespace DataAccess.Concrete.EntityFramework;

public class EfUserOperationClaimDal : EfGenericRepository<UserOperationClaim, SimpraProjectContext>, IUserOperationClaimDal
{
}
