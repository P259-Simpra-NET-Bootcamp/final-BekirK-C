using Business.Abstract;
using Entities.Models.Requests;

namespace Business.Concrete;

public class CreditCardPolicyManager : ICreditCardPolicyService
{
    public bool ValidateCreditCardInfo(CreditCardInfoRequest creditCardInfo)
    {
        // Bu bir eğitim projesi olduğundan CreditCardPolicyManager'da return true kullanılmıştır.
        return true;
    }
}
