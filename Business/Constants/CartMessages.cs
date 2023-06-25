using Entities.Concrete;

namespace Business.Constants;

public static class CartMessages
{
    public static string ProductNotFound = "Ürün bulunamadı!";
    public static string QuantityGreaterThanZero = "Miktar alanı 0'dan büyük olmalıdır.";
    public static string InsufficientStock = "Ürün stokta yetersiz.";
    public static string ProductAddedToCart = "Ürün sepete eklendi.";
    public static string CartItemNotFound = "Sepet öğesi bulunamadı!";
    public static string QuantityExceedsCartQuantity = "Miktar, sepet içindeki ürünün miktarını aşamaz!";
    public static string QuantityDecrementedInCart = "Sepet içindeki ürünün miktarı azaltıldı.";
    public static string CartCompletelyEmptied = "Sepet tamamen boşaltıldı.";
    public static string ProductRemovedFromCart = "Ürün sepetten çıkarıldı.";
    public static string ProductQuantityUpdatedInCart = "Ürün miktarı sepet içinde güncellendi.";
    public static string NotAnyCartItem = "Sepette herhangi bir ürün bulunmamaktadır.";
}
