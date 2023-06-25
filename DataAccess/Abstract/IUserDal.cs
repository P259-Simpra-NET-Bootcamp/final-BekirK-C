using Base.DataAccess;
using Base.Entities.Concrete;

namespace DataAccess.Abstract;

public interface IUserDal : IGenericRepository<User>
{
    List<OperationClaim> GetClaims(User user);
}
