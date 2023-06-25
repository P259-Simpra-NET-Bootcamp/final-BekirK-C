using Entities.Models.Requests;

namespace Business.Abstract;

public interface ICreditCardPolicyService
{
    bool ValidateCreditCardInfo(CreditCardInfoRequest creditCardInfo);
}
