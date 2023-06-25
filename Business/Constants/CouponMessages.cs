namespace Business.Constants;

public static class CouponMessages
{
    public static string InvalidCouponParameters = "Kupon sayısı, indirim ve gün sayısı sıfırdan büyük olmalıdır.";
    public static string CouponsSuccessfullyGenerated = "Kupon(lar) başarıyla oluşturuldu.";
    public static string NoActiveCouponsFound = "Aktif bir kupon bulunmamaktadır!";
    public static string CouponNotFound = "Kupon bulunamadı!";
    public static string ActiveCouponsListed = "Aktif kuponlar listelendi";
    public static string CouponListed = "Kupon listelendi";
    public static string InvalidCouponCode = "Girdiğiniz kupon geçerli değildir!";
    public static string CouponUpdated = "Kupon güncellendi.";
    public static string ValidCoupon = "Girdiğiniz kupon geçerlidir.";
    public static string CouponDeleted = "Kupon silindi!";

    public static string CouponCountGreaterThanZero = "Kupon sayısı 0'dan büyük olmalıdır.";
    public static string DiscountAmountGreaterThanZero = "İndirim miktarı 0'dan büyük olmalıdır.";
    public static string ValidDaysGreaterThanZero = "Geçerlilik süresi 0'dan büyük olmalıdır.";
}
