using AutoMapper;
using Base.Entities.Concrete;
using Base.Utilities.Results;
using Business.Abstract;
using Business.Constants;
using DataAccess.Abstract;
using Entities.Models.Requests;
using Entities.Models.Responses;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Business.Concrete;

public class UserManager : IUserService
{
    private readonly IUserDal _userDal;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserManager(IUserDal userDal, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _userDal = userDal;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public IResult Add(User user)
    {
        _userDal.Add(user);
        return new SuccessResult(UserMessages.UserAdded);
    }

    public IResult Delete(int id)
    {
        var user = GetUser(id);
        if (user == null)
            return new ErrorResult(UserMessages.UserNotFound);
        _userDal.Delete(user.Data);
        return new SuccessResult(UserMessages.UserDeleted);
    }

    public IDataResult<List<UserResponse>> GetAll()
    {
        var userList = _userDal.GetAll();
        var mappedUserList = _mapper.Map<List<User>, List<UserResponse>>(userList);
        return new SuccessDataResult<List<UserResponse>>(mappedUserList, UserMessages.UsersListed);
    }

    public IDataResult<User> GetByMail(string mail)
    {
        return new SuccessDataResult<User>(_userDal.Get(u => u.Email == mail), UserMessages.UsersListed);
    }

    public IDataResult<List<OperationClaim>> GetClaims(User user)
    {
        return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
    }

    public IDataResult<User> GetUser(int userId)
    {
        return new SuccessDataResult<User>(_userDal.Get(u => u.Id == userId), UserMessages.UsersListed);
    }

    public IResult Update(UserRequest userRequest)
    {
        var mappedUser = _mapper.Map<UserRequest, User>(userRequest);
        var customer = _userDal.Get(u => u.Id == userRequest.Id);
        if (customer == null)
            return new ErrorResult(UserMessages.UserNotFound);

        MatchUserProperties(mappedUser, customer);

        _userDal.Update(mappedUser);
        return new SuccessResult(UserMessages.UserUpdated);
    }

    public void AddEarnedPointsToUser(User user, decimal earnedPoints)
    {
        user.EarnedPoints += earnedPoints;
        _userDal.Update(user);
    }

    public int GetCustomerIdFromAccessToken()
    {
        string customerIdString = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        int.TryParse(customerIdString, out int customerId);
        return customerId;
    }

    public decimal CalculateEarnedPoints(decimal productPrice, int pointPercentage, decimal maxPoint)
    {
        decimal earnedPoints = productPrice * (pointPercentage / 100m);
        return Math.Min(earnedPoints, maxPoint);
    }

    private void MatchUserProperties(User mappedUser, User customer)
    {
        mappedUser.PasswordHash = customer.PasswordHash;
        mappedUser.PasswordSalt = customer.PasswordSalt;
        mappedUser.Status = customer.Status;
        mappedUser.Email = customer.Email;
        mappedUser.CreatedAt = customer.CreatedAt;
        mappedUser.VirtualWallet = customer.VirtualWallet;
        mappedUser.EarnedPoints = customer.EarnedPoints;
    }
}
