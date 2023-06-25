using Base.Utilities.Results;
using Entities.Models.Requests;

namespace Business.Abstract;

public interface IWalletService
{
    IResult AddMoneyToWallet(CreditCardInfoRequest creditCardInfoRequest, decimal amount);
    IDataResult<decimal> GetWalletBalance();
}
