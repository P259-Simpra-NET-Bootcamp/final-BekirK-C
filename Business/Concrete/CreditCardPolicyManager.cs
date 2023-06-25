using Business.Abstract;
using Entities.Models.Requests;

namespace Business.Concrete;

public class CreditCardPolicyManager : ICreditCardPolicyService
{
    public bool ValidateCreditCardInfo(CreditCardInfoRequest creditCardInfo)
    {
        // Gerçek bir projede Kredi Kartı için yapılması gereken implementasyonlar yetkili makamlardan izim
        // alınarak gerçekleşmektedir. Bu bir eğitim projesi olduğundan policy'de return true kullanılmıştır.
        return true;
    }
}
