using Base.Utilities.Results;
using Business.Abstract;
using Business.Constants;
using DataAccess.Abstract;
using Entities.Models.Requests;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete;

public class WalletManager : IWalletService
{
    private readonly IUserDal _userDal;
    private readonly IUserService _userManager;
    private readonly ICreditCardPolicyService _creditCardPolicyManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IValidator<CreditCardInfoRequest> _creditCardInfoVValidator;

    public WalletManager(IHttpContextAccessor httpContextAccessor, IUserDal userDal, ICreditCardPolicyService creditCardPolicyManager, IValidator<CreditCardInfoRequest> creditCardInfoVValidator, IUserService userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userDal = userDal;
        _creditCardPolicyManager = creditCardPolicyManager;
        _creditCardInfoVValidator = creditCardInfoVValidator;
        _userManager = userManager;
    }

    public IResult AddMoneyToWallet(CreditCardInfoRequest creditCardInfoRequest, decimal amount)
    {
        ValidationResult result = _creditCardInfoVValidator.Validate(creditCardInfoRequest);
        if (!result.IsValid)
            return new ErrorResult(result.Errors.FirstOrDefault()?.ErrorMessage);

        if (amount <= 0)
            return new ErrorResult(WalletMessages.DepositAmountGreaterThanZero);

        bool isCreditCardValid = _creditCardPolicyManager.ValidateCreditCardInfo(creditCardInfoRequest);

        if (!isCreditCardValid)
            return new ErrorResult(WalletMessages.InvalidCreditCard);

        var customerId = _userManager.GetCustomerIdFromAccessToken();
        var customer = _userDal.Get(u => u.Id == customerId);
        customer.VirtualWallet += amount;

        _userDal.Update(customer);
        return new SuccessResult(WalletMessages.DepositSuccess);
    }

    public IDataResult<decimal> GetWalletBalance()
    {
        var customerId = _userManager.GetCustomerIdFromAccessToken();
        var customer = _userDal.Get(u => u.Id == customerId);
        return new SuccessDataResult<decimal>(customer.VirtualWallet, WalletMessages.VirtualWalletBalanceRetrieved);
    }
}
