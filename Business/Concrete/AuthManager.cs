using Base.Entities.Concrete;
using Base.Utilities.Results;
using Base.Utilities.Security.Hashing;
using Base.Utilities.Security.JWT;
using Business.Abstract;
using Business.Constants;
using DataAccess.Abstract;
using Entities.Enums;
using Entities.Models.Requests;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Business.Concrete;

public class AuthManager : IAuthService
{
    private IUserService _userService;
    private ITokenHelper _tokenHelper;
    private IUserDal _userDal;
    private IUserOperationClaimDal _userOperationClaimDal;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IValidator<UserRegisterRequest>? _userRegistervalidator;

    public AuthManager(IUserService userService, ITokenHelper tokenHelper, IUserOperationClaimDal userOperationClaimDal, IUserDal userDal, IHttpContextAccessor httpContextAccessor, IValidator<UserRegisterRequest>? userRegistervalidator)
    {
        _userService = userService;
        _tokenHelper = tokenHelper;
        _userOperationClaimDal = userOperationClaimDal;
        _userDal = userDal;
        _httpContextAccessor = httpContextAccessor;
        _userRegistervalidator = userRegistervalidator;
    }

    public IDataResult<User> Register(UserRegisterRequest userRegisterRequest, bool isAdmin = false)
    {
        ValidationResult result = _userRegistervalidator.Validate(userRegisterRequest);
        if (!result.IsValid)
            return new ErrorDataResult<User>(result.Errors.FirstOrDefault()?.ErrorMessage);

        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(userRegisterRequest.Password, out passwordHash, out passwordSalt);
        var user = new User
        {
            Email = userRegisterRequest.Email,
            FirstName = userRegisterRequest.FirstName,
            LastName = userRegisterRequest.LastName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Status = UserRoles.Customer.ToString(),
            CreatedAt = DateTime.UtcNow,
            ShippingAddress = userRegisterRequest.ShippingAddress,
            EarnedPoints = 0,
            VirtualWallet = 0,
        };
        _userService.Add(user);
        var userOperationClaim = new UserOperationClaim();
        userOperationClaim.UserId = user.Id;
        if (isAdmin)
        {
            userOperationClaim.OperationClaimId = (int)UserRoles.Admin;
            user.Status = UserRoles.Admin.ToString();
            _userDal.Update(user);
        }
        else
            userOperationClaim.OperationClaimId = (int)UserRoles.Customer;

        userOperationClaim.UserMail = user.Email;
        _userOperationClaimDal.Add(userOperationClaim);
        return new SuccessDataResult<User>(user, UserMessages.UserRegistered);
    }

    public IResult Update(UserRegisterRequest userRegisterRequest)
    {
        ValidationResult result = _userRegistervalidator.Validate(userRegisterRequest);
        if (!result.IsValid)
            return new ErrorDataResult<User>(result.Errors.FirstOrDefault()?.ErrorMessage);

        byte[] passwordHash, passwordSalt;
        var customerEmail = GetCustomerEmailFromAccessToken();
        var customer = _userDal.Get(u => u.Email == customerEmail);

        var newEmailControl = UserExists(userRegisterRequest.Email);
        if (userRegisterRequest.Email == customerEmail || newEmailControl.Success)
        {
            HashingHelper.CreatePasswordHash(userRegisterRequest.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Id = customer.Id,
                Email = userRegisterRequest.Email,
                FirstName = userRegisterRequest.FirstName,
                LastName = userRegisterRequest.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = customer.Status,
                CreatedAt = customer.CreatedAt,
                ShippingAddress = customer.ShippingAddress,
                EarnedPoints = customer.EarnedPoints,
                VirtualWallet = customer.VirtualWallet,
            };
            _userDal.Update(user);

            return new SuccessResult(UserMessages.UserUpdated);
        }
        return new ErrorResult(UserMessages.EmailAlreadyExists);
    }

    public IDataResult<User> Login(UserLoginRequest userLoginRequest)
    {
        var userToCheck = _userService.GetByMail(userLoginRequest.Email);
        if (userToCheck.Data == null)
            return new ErrorDataResult<User>(UserMessages.UserNotFound);

        if (!HashingHelper.VerifyPasswordHash(userLoginRequest.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            return new ErrorDataResult<User>(UserMessages.PasswordError);

        return new SuccessDataResult<User>(userToCheck.Data, UserMessages.SuccessfulLogin);
    }

    public IResult UserExists(string email)
    {
        if (_userService.GetByMail(email).Data != null)
            return new ErrorResult(UserMessages.UserAlreadyExists);

        return new SuccessResult();
    }

    public IDataResult<AccessToken> CreateAccessToken(User user)
    {
        var claims = _userService.GetClaims(user);
        var accessToken = _tokenHelper.CreateToken(user, claims.Data);
        return new SuccessDataResult<AccessToken>(accessToken, UserMessages.AccessTokenCreated);
    }

    private string GetCustomerEmailFromAccessToken()
    {
        string customerEmail = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        return customerEmail;
    }
}
