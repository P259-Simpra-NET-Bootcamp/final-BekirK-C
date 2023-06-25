Simpra Bootcamp Final-Project
----------
- haftalık Simpra bootcamp sürecinin sonunda verilen bitirme projesidir.
- Proje SOLID prensiplerine uygun bir şekilde hazırlanmıştır.
- Güvenlik için JWT entegre edilmiş, Bağımlıklıkları çözmek için Autofac kullanılmış ve hatalar SeriLog ile loglanmıştır.

Aşağıdaki teknolojiler kullanılmıştır
----------
[![C-Sharp](https://camo.githubusercontent.com/dd433625a6e00049c26f08143705ff9e32d5da44f503f1be133664b11e37e34b/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f432532332d3233393132303f7374796c653d666f722d7468652d6261646765266c6f676f3d632d7368617270266c6f676f436f6c6f723d7768697465)](https://docs.microsoft.com/en-us/dotnet/csharp/) [![Asp-net](https://camo.githubusercontent.com/d2eedef86b5c7700ce36b271700d22a225ed80deb882f1bc627b0b1d3543dd3f/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f4153502e4e45542d3543324439313f7374796c653d666f722d7468652d6261646765266c6f676f3d2e6e6574266c6f676f436f6c6f723d7768697465)](https://dotnet.microsoft.com/apps/aspnet) [![MSSQL](https://camo.githubusercontent.com/4c4e18333e9f48e9f6f4190e08dee3957c75b531a2bb78e9bfe33cbdcf99cdd4/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f4d5353514c2d3030343838303f7374796c653d666f722d7468652d6261646765266c6f676f3d6d6963726f736f66742d73716c2d736572766572266c6f676f436f6c6f723d7768697465)](https://www.microsoft.com/en-us/sql-server/sql-server-2019?rtc=2) [![Entity-Framework](https://camo.githubusercontent.com/1d5fe1015065a89592443eb419d5974655ffbe17c2d9a1e51c73bd0ad9a357ba/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f456e746974792532304672616d65776f726b2d3030343838303f7374796c653d666f722d7468652d6261646765266c6f676f3d6e75676574266c6f676f436f6c6f723d7768697465)](https://docs.microsoft.com/en-us/ef/) [![Autofac](https://camo.githubusercontent.com/660a4e0e53571f8f593a56df74573cb8f09777268a87305057363a9b38a3dd59/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f4175746f6661632d3030343838303f7374796c653d666f722d7468652d6261646765266c6f676f3d6e75676574266c6f676f436f6c6f723d7768697465)](https://autofac.org/) [![Fluent-Validation](https://camo.githubusercontent.com/6deba73d71845daec484b10b754dc0c648cdd13fb24480c38e52becf608f215f/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f466c75656e7425323056616c69646174696f6e2d3030343838303f7374796c653d666f722d7468652d6261646765266c6f676f3d6e75676574266c6f676f436f6c6f723d7768697465)](https://fluentvalidation.net/)

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
  `UPDATE Users SET [Status] = 'Admin' WHERE Id = 1`  
  `UPDATE UserOperationClaims SET OperationClaimId = 1 WHERE Id = 1`  
- Bu şekilde ilk kullanıcı admin olarak eklenmiş olur.

Veri tabanına örnek veriler eklemek için (İsteğe bağlı)
----------
- Örnek ürün ve kategori verileri eklemek için önce aşağıdaki kodu çalıştırıp örnek kategori verileri eklenebilir.  
`INSERT INTO Categories (Name, Description, CreatedAt) VALUES`  
`('Category 1', 'Description of Category 1', GETDATE()),`  
`('Category 2', 'Description of Category 2', GETDATE()),`  
`('Category 3', 'Description of Category 3', GETDATE());`  
- Kategori ekleme işleminden sonra aşağıdaki kod bloğunu çalıştırarak örnek ürün verileri eklenir. (FK çakışmasından dolayı önce kategori sonrasında ürün eklenmelidir)  
`INSERT INTO Products ([Name], [Description], CategoryId, Price, Stock, MaxPoint, PointPercentage, CreatedAt) VALUES`  
  `('Product 1', 'Description of Product 1', 1, 10.5, 100, 10, 15, GETDATE()),`  
  `('Product 2', 'Description of Product 2', 1, 15, 500, 5, 10, GETDATE()),`  
  `('Product 3', 'Description of Product 3', 2, 7.8, 200, 15, 20, GETDATE()),`  
  `('Product 4', 'Description of Product 4', 2, 30, 150, 10, 15, GETDATE()),`  
  `('Product 5', 'Description of Product 3', 3, 21, 80, 15, 30, GETDATE()),`  
  `('Product 6', 'Description of Product 4', 3, 9, 340, 12, 25, GETDATE())`  

Proje Hakkında
----------
- Dokümantasyon Swagger ile gerçekleştirilmiştir. Tüm action'ların açıklaması yapılmış ve Post işlemlerinde örnek veriler eklenmiştir.
- "Admin" ile başlayan tüm controller endpoint'leri sadece "admin" role sahip kullanıcılar tarafından istek atılabilmektedir.
- Ürün-kategori listeleme ve customer-login harici tüm istekler Authenticate gerektirmektedir. 
- Giriş yapıldıktan sonra 30 dk'lık token süresi bulunmaktadır.
- Alışveriş yapmak için öncelikle ürünler sepete eklenmeli ardından sepetteki ürünlerin siparişi verilmelidir.
- Para kullanıcının sanal cüzdanından çekilmektedir. Eğer sanal cüzdanda yeterli para yoksa sanal cüzdana para ekleme işlemi kredi kartı ile yapılmalıdır.



