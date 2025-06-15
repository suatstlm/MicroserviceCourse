# MicroserviceCourse

Bu eğitimi baştan sona tamamladım ve projeyi adım adım geliştirerek, Microservice mimarinin tüm yapı taşlarını uygulamalı olarak öğrenme fırsatı buldum.

Kurs sürecinde hem teorik bilgiler edindim hem de gerçek bir proje geliştirerek öğrendiklerimi pratiğe döktüm. Udemy benzeri bir kurs satış platformunu microservice mimaride sıfırdan inşa ettim.

🔧 Öğrendiğim ve Uyguladığım Başlıca Konular:
Microservice'ler arasında senkron ve asenkron iletişim kurmayı öğrendim ve uyguladım.

OAuth 2.0 ve OpenID Connect protokollerini kullanarak güvenli kimlik doğrulama altyapısı kurdum.

Farklı servislerin kendi veritabanlarını kullandığı bir yapıda Eventual Consistency (Nihai Tutarlılık) modelini uyguladım.

Her mikroservisi Docker container olarak ayağa kaldırdım.

Docker Compose ile tüm servisleri orkestre ettim.

MongoDB, Redis, PostgreSQL ve SQL Server gibi veritabanlarını container olarak yapılandırdım.

🚀 Geliştirdiğim Microservice’ler:
📚 Catalog Microservice
Kurs bilgilerini yöneten servis.

MongoDB kullanarak One-to-Many ve One-to-One ilişkiler kurdum.

🛒 Basket Microservice
Redis ile çalışan, sepet işlemlerini yöneten servis.

🎁 Discount Microservice
PostgreSQL üzerinde çalışan, kullanıcıya özel kupon tanımlayan servis.

📦 Order Microservice
Domain Driven Design yaklaşımı ile yapılandırdım.

CQRS mimarisi için MediatR kütüphanesini kullandım.

Veritabanı olarak SQL Server kullandım.

💳 FakePayment Microservice
Ödeme simülasyonunu gerçekleştiren basit bir servis.

🔐 IdentityServer Microservice
SQL Server veritabanı ile kullanıcı bilgilerini tutuyor.

Token ve RefreshToken üretimini burada gerçekleştirdim.

OAuth 2.0 ve OpenID Connect protokollerini entegre ettim.

🖼️ PhotoStock Microservice
Kurs fotoğraflarını yöneten servis.

🌐 API Gateway
Tüm servisleri Ocelot ile tek bir API üzerinden yönettim.

Access Token doğrulaması ile güvenli iletişim sağladım.

📬 Mesajlaşma Altyapısı
Microservice'ler arası asenkron iletişim için RabbitMQ kullandım.

RabbitMQ ile haberleşmek için MassTransit kütüphanesini entegre ettim.
