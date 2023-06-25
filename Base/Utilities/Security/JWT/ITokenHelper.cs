using Base.Entities.Concrete;

namespace Base.Utilities.Security.JWT;

public interface ITokenHelper
{
    AccessToken CreateToken(User user, List<OperationClaim> operationClaim);
}
