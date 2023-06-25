using Base.DataAccess.EntityFramework;
using Base.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Context;

namespace DataAccess.Concrete.EntityFramework;

public class EfUserDal : EfGenericRepository<User, SimpraProjectContext>, IUserDal
{
    public List<OperationClaim> GetClaims(User user)
    {
        using (var context = new SimpraProjectContext())
        {
            var result = from operationClaim in context.OperationClaims
                         join userOperationClaim in context.UserOperationClaims
                         on operationClaim.Id equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == user.Id
                         select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
            return result.ToList();

        }
    }
}
