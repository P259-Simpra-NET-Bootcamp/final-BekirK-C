Simpra Bootcamp Final-Project
----------
- haftalık Simpra bootcamp sürecinin sonunda verilen bitirme projesidir.
- Proje SOLID prensiplerine uygun bir şekilde hazırlanmıştır.
- Güvenlik için JWT entegre edilmiş, Bağımlıklıkları çözmek için Autofac kullanılmış ve hatalar SeriLog ile loglanmıştır.

Aşağıdaki teknolojiler kullanılmıştır
----------
-   .NET Core
-   Asp.NET for Restful Api
-   MsSql
-   Entity Framework
-   Autofac
-   Fluent Validation

Projeyi çalıştırmak için
----------
- Github'dan projeyi clone'layıp bilgisayarda projeyi başlattıktan sonra ilk olarak DataAccess>Context>SimpraProjectContext içerisinde connectionString bağlantısı güncellenmelidir.
- Migration işlemi gerçekleştirilip veri tabanı tabloları oluşturulmalıdır. (Örnek "add-migration init_mig")
- Proje içerisinde admin/customer rolleri bulunmaktadır. Veri tabanına ilk admin kullanıcı kaydı için birkaç TSQL kodu MSSQL'de çalıştırılmalıdır.
- Aşağıdaki kod bloğunu MSSQL’de çalıştırarak Oepration Claims tablosu oluşturulur. (id=1-”admin” id=2-”customer” olması programın düzgün çalışabilmesi önem arz etmektedir.)
`USE TestOne;`
`INSERT INTO OperationClaims(Name)`
`VALUES ('admin'), ('customer');`
- Aşağıdaki kod bloğunu çalıştırarak ilk kullanıcıyı admin olarak eklemiş oluyorum.
```UPDATE Users SET [Status] = 'Admin' WHERE Id = 1```
```UPDATE UserOperationClaims SET OperationClaimId = 1 WHERE Id = 1```
- Bu şekilde ilk kullanıcı admin olarak eklenmiş olur.

Veri tabanına örnek veriler eklemek için (İsteğe bağlı)
----------
- Örnek ürün ve kategori verileri eklemek için önce aşağıdaki kodu çalıştırıp örnek kategori verileri eklenebilir.
```INSERT INTO Categories (Name, Description, CreatedAt) VALUES```
```('Category 1', 'Description of Category 1', GETDATE()),```
```('Category 2', 'Description of Category 2', GETDATE()),```
```('Category 3', 'Description of Category 3', GETDATE());```
- Kategori ekleme işleminden sonra aşağıdaki kod bloğunu çalıştırarak örnek ürün verileri eklenir. (FK çakışmasından dolayı önce kategori sonrasında ürün eklenmelidir)
```INSERT INTO Products ([Name], [Description], CategoryId, Price, Stock, MaxPoint, PointPercentage, CreatedAt) VALUES```
```('Product 1', 'Description of Product 1', 1, 10.5, 100, 10, 15, GETDATE()),```
```('Product 2', 'Description of Product 2', 1, 15, 500, 5, 10, GETDATE()),```
```('Product 3', 'Description of Product 3', 2, 7.8, 200, 15, 20, GETDATE()),```
```('Product 4', 'Description of Product 4', 2, 30, 150, 10, 15, GETDATE()),```
```('Product 5', 'Description of Product 3', 3, 21, 80, 15, 30, GETDATE()),```
```('Product 6', 'Description of Product 4', 3, 9, 340, 12, 25, GETDATE())```

Proje Hakkında
----------
- Dokümantasyon Swagger ile gerçekleştirilmiştir. Tüm action'ların açıklaması yapılmış ve Post işlemlerinde örnek veriler eklenmiştir.
- "Admin" ile başlayan tüm controller endpoint'leri sadece "admin" role sahip kullanıcılar tarafından istek atılabilmektedir.
- Ürün-kategori listeleme ve customer-login harici tüm istekler Authenticate gerektirmektedir. 
- Giriş yapıldıktan sonra 30 dk'lık token süresi bulunmaktadır.
- Alışveriş yapmak için öncelikle ürünler sepete eklenmeli ardından sepetteki ürünlerin siparişi verilmelidir.
- Para kullanıcının sanal cüzdanından çekilmektedir. Eğer sanal cüzdanda yeterli para yoksa sanal cüzdana para ekleme işlemi kredi kartı ile yapılmalıdır.



