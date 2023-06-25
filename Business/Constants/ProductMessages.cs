namespace Business.Constants;

public static class ProductMessages
{
    public static string ProductAdded = "Ürün başarıyla eklendi.";
    public static string ProductDeleted = "Ürün başarıyla silindi.";
    public static string ProductsListed = "Ürünler başarıyla listelendi.";
    public static string ProductsListedWithCategory = "Kategori ile birlikte Ürünler başarıyla eklendi.";
    public static string ProductListed = "Ürün başarıyla listelendi.";
    public static string ProductUpdated = "Ürün başarıyla güncellendi.";

    public static string InvalidName = "Ürün adı boş geçilemez ve 3 karakterden fazla olmalıdır.";
    public static string InvalidDescription = "Ürün açıklaması boş geçilemez.";
    public static string InvalidPrice = "Ürün fiyatı sıfırdan büyük olmalıdır.";
    public static string InvalidStock = "Ürün stok adedi sıfırdan büyük olmalıdır.";
    public static string ProductListedSelectedCategory = "İstenen kategori ürünleri başarıyla listelendi.";

    public static string InvalidCategoryId = "Geçersiz kategori Id. Kategori bulunamadı!";
    public static string ProductNotFound = "Ürün bulunamadı!";
    public static string NoProductInStock = "Stokta ürün bulunmamaktadır!";
    public static string InsufficientStock = "Yetersiz stok!";
    public static string StockDecreasedSuccess = "Stok azaltıldı";
    public static string StockIncreasedSuccess = "Stok arttırıldı";
    public static string ProductsInStockListed = "Stokta bulunan ürünler listelendi.";

    public static string NoProductAbovePrice(decimal price)
    {
        return $"{price} TL üzerinde ürün bulunmamaktadır.";
    }
    public static string ProductsAbovePriceListed(decimal price)
    {
        return $"{price} TL üzerindeki ürünler listelendi.";
    }
}
