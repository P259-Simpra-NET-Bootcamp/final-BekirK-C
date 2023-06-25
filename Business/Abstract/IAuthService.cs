using Base.Entities.Concrete;
using Base.Utilities.Results;
using Base.Utilities.Security.JWT;
using Entities.Models.Requests;

namespace Business.Abstract;

public interface IAuthService
{
    IDataResult<User> Register(UserRegisterRequest userRegisterRequest, bool isAdmin = false);
    IResult Update(UserRegisterRequest userRegisterRequest);
    IDataResult<User> Login(UserLoginRequest userLoginRequest);
    IResult UserExists(string email);
    IDataResult<AccessToken> CreateAccessToken(User user);
}
