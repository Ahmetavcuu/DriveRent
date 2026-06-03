# 🚗 DriveRent - Araç Kiralama Otomasyonu

**DriveRent**, araç kiralama süreçlerini yönetmek, müşteri kayıtlarını tutmak ve kiralama işlemlerini dijital ortamda takip etmek amacıyla geliştirilmiş bir otomasyon projesidir.

## 🚀 Proje Hakkında

Bu proje, bir araç kiralama şirketinin (Rent a Car) günlük operasyonlarını kolaylaştırmak için tasarlanmıştır. Araç filosu yönetimi ve kiralama işlemleri gibi temel işlevleri barındırır. 

## 🛠️ Kullanılan Teknolojiler

* **Programlama Dili:** C# 
* **Veri Erişim Teknolojisi:** Entity Framework (ORM)
* **Veritabanı:** Microsoft SQL Server (veya kullandığınız diğer veritabanı)

## ✨ Temel Özellikler

* **Araç Yönetimi:** Yeni araç ekleme, mevcut araç bilgilerini (marka, model, yıl, plaka, günlük kiralama bedeli vb.) güncelleme ve sistemden kaldırma.
* **Müşteri Yönetimi:** Sisteme müşteri kaydı oluşturma, listeleme ve düzenleme.
* **Kiralama İşlemleri:** Belirli tarih aralıkları için araç kiralama işlemi ve toplam tutar hesaplama.
* **Kullanıcı Dostu Arayüz:** İşlemlerin hızlı ve pratik bir şekilde yapılabildiği arayüz tasarımı.

## ⚙️ Kurulum ve Çalıştırma

Projeyi yerel ortamınızda (Localhost) çalıştırmak için aşağıdaki adımları izleyebilirsiniz:

1.  **Projeyi Klonlayın:**
    ```bash
    git clone https://github.com/Ahmetavcuu/DriveRent.git
    ```
2.  **Bağlantı Ayarlarını (Connection String) Yapın:**
    * Proje içerisindeki `App.config` (veya `appsettings.json`) dosyasını açın.
    * Veritabanı bağlantı dizesini kendi SQL Server sunucu bilgilerinize göre güncelleyin.
3.  **Veritabanını Oluşturun (Migration):**
    * Package Manager Console (PMC) üzerinden aşağıdaki komutu çalıştırarak veritabanını oluşturun:
        ```powershell
        Update-Database
        ```
    *(Eğer Code-First yaklaşımı kullanmadıysanız, projedeki `.sql` script dosyasını veritabanınızda çalıştırın.)*
4.  **Projeyi Çalıştırın:**
    * Visual Studio üzerinde `F5` tuşuna basarak projeyi derleyip çalıştırın.


## 🤝 İletişim & Geliştirici

**Ahmet Avcu**
* GitHub: [@Ahmetavcuu](https://github.com/Ahmetavcuu)
* Eğitim: Burdur Mehmet Akif Ersoy Üniversitesi - Yönetim Bilişim Sistemleri (YBS)
