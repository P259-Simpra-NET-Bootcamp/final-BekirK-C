using Entities.Concrete;

namespace Business.Constants;

public class OrderMessages
{
    public static string CartIsEmpty = "Sepet boş!";
    public static string ProductNotFound = "Ürün bulunamadı!";
    public static string InsufficientStock = "Ürün stoku yetersiz!";
    public static string OrderPlacedSuccessfully = "Sipariş başarıyla tamamlandı.";
    public static string InvalidCouponCode = "Geçersiz kupon kodu!";
    public static string NotAnyOrder = "Siparişiniz bulunmamaktadır";

    public static string InsufficientBalance(decimal insufficientAmount)
    {
        return $"Insufficient balance. You need an additional amount of {insufficientAmount} to complete the order.";
    }
}
