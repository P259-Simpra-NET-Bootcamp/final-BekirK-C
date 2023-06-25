using Base.Entities.Concrete;
using Base.Utilities.Results;
using Entities.Models.Requests;
using Entities.Models.Responses;

namespace Business.Abstract;

public interface IUserService
{
    IDataResult<List<UserResponse>> GetAll();
    IDataResult<User> GetUser(int userId);
    IResult Add(User user);
    IResult Delete(int id);
    IResult Update(UserRequest userRequest);
    IDataResult<List<OperationClaim>> GetClaims(User user);
    IDataResult<User> GetByMail(string mail);
    void AddEarnedPointsToUser(User user, decimal earnedPoints);
    int GetCustomerIdFromAccessToken();
    decimal CalculateEarnedPoints(decimal productPrice, int pointPercentage, decimal maxPoint);
}
