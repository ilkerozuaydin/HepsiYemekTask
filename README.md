# Proje Açıklaması
- Business, Core, DataAccess, Entities ve HepsiYemekTask_API olmak üzere 5 katmandan oluşmaktadır.
- Sınıflar soyutlanarak dependency injection ile gereken yerlerde kullanıldı.
- MongoDb repository pattern ile implemente edildi.
- Exception Middleware ile hata kontrolü sağlandı.(Genişetilebilir)
- Mapster ile sınıflar arası mapping işlemi yapıldı.
- Liste üzerinde filtreleme işlemleri için OData ve QueryString seçenekleri bulunmaktadır.
- Proje testleri için ana klasörde postman collection bulunmaktadır.

# Proje Gereksinimleri
- MongoDb bulut(AWS) üzerinde oluşturulmuştur ve appsettings.json dosyasında bağlantı bilgileri yer almaktadır.
- Projenin çalıştırılacağı bilgisayarda localhost:6379 ip ve port bilgileriyle Redis kurulu olmalıdır.
