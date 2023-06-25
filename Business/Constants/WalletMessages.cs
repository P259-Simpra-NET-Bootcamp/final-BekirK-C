namespace Business.Constants
{
    internal class WalletMessages
    {
        public static string DepositAmountGreaterThanZero = "Para yatıracağınız miktar 0'dan fazla olmalıdır.";
        public static string InvalidCreditCard = "Geçersiz kredi kartı!";
        public static string DepositSuccess = "Sanal cüzdana başarıyla para eklendi";
        public static string VirtualWalletBalanceRetrieved = "Sanal cüzdan bakiyesi getirildi.";

        public static string CardNumberRequired = "Kart numarası gereklidir.";
        public static string ValidCardNumberLength = "Geçerli bir 16 haneli kredi kartı numarası giriniz.";
        public static string CardHolderNameRequired = "Kart sahibi adı gereklidir.";
        public static string ExpirationMonthRequired = "Son kullanma ayı gereklidir.";
        public static string InvalidExpirationMonth = "Geçersiz son kullanma ayı.";
        public static string ExpirationYearRequired = "Son kullanma yılı gereklidir.";
        public static string InvalidExpirationYear = "Geçersiz son kullanma yılı.";
        public static string CVVRequired = "CVV gereklidir.";
        public static string InvalidCVV = "Geçersiz CVV.";
    }
}
