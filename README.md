# Öğrenci Bilgi Sistemi

Bu proje, ASP.NET MVC yapısı kullanılarak kodlanmış ve web tabanlı olarak hazırlanmıştır. Nesne Yönelimli Programlama (OOP) prensiplerine göre geliştirilmiş olup, ilişkisel bir veritabanı kullanmaktadır.

## Proje Özellikleri

- Öğrenci ve öğretim elemanı kullanıcıları için ayrı ayrı paneller.
- Kullanıcı girişi ve yetkilendirme işlemleri.
- Öğrenciler için ders seçimi, not görüntüleme, transkript oluşturma gibi işlemler.
- Öğretim elemanları için ders programı, sınav notları girişi, danışmanlık işlemleri gibi özellikler.
- Responsive kullanıcı arayüzü.

## Veritabanı Tasarımı

Proje kapsamında aşağıdaki tablolar oluşturulmuştur:

| Tablo Adı        | Açıklama                                                  |
|------------------|-----------------------------------------------------------|
| Kullanici        | Otomasyona erişim yetkisi olan ve rolleri tanımlı kişiler  |
| Bolum            | Lisans programlarının kayıtlı olduğu akademik birimler    |
| Ogrenci          | Akademik programlarda kayıtlı olan kişiler                |
| OgretimElemani   | Akademik personel (Prof. Dr., Doç. Dr., vb.)              |
| Derslik          | Derslerin işlendiği yerler                                 |
| DersHavuzu       | Tüm derslerin yer aldığı tablo                             |
| Mufredat         | Bölümlere ait öğretim planları                             |
| DersAcma         | Açılan dersler                                             |
| DersAlma         | Öğrencilerin almış olduğu dersler                          |
| Sinav            | Sınav tanımları                                            |
| Degerlendirme    | Sınav notlarının girişi                                    |
| DersProgrami     | Haftalık ders çizelgeleri                                  |
| Danismanlik      | Öğretim elemanlarının danışman olduğu öğrenciler           |

## Kullanıcı Panelleri

### Öğrenci Paneli
- **Öğrenci Bilgileri:** Ad, Soyad, T.C. Kimlik No, Doğum Yeri ve Tarihi gibi kimlik bilgileri.
- **Ders Programı:** Öğrencinin haftalık ders programı.
- **Notlar:** Öğrencinin sınav notları.
- **Transkript:** Öğrencinin aldığı derslerin not dökümü.
- **Ders Kayıt:** Öğrencinin ders seçimi yapabileceği sayfa.

### Öğretim Elemanı Paneli
- **Kişisel Bilgiler:** Ad, Soyad, T.C. Kimlik No, Unvan gibi kimlik bilgileri.
- **Verilen Dersler:** Öğretim elemanının verdiği dersler.
- **Ders Programı:** Öğretim elemanının haftalık ders programı.
- **Sınav Tanımlama:** Derslerin sınavlarını tanımlama ve düzenleme.
- **Not Girişi:** Sınav sonuçlarını girme ve güncelleme.
- **Danışmanlık:** Danışmanlık yaptığı öğrencilerin listesi.

## Ekran Görüntüleri

### Öğrenci Paneli

1. **Özlük Bilgileri**
   ![Özlük Bilgileri](screenshots/ogrenci/1.png)
   
2. **Transkript**
   ![Transkript](screenshots/ogrenci/2.png)
   
3. **Ders Programı**
   ![Ders Programı](screenshots/ogrenci/3.png)
   
4. **Dönem Dersleri**
   ![Dönem Dersleri](screenshots/ogrenci/4.png)
   
5. **Bölüm Müfredatı**
   ![Bölüm Müfredatı](screenshots/ogrenci/5.png)
   
6. **Sınav Notları**
   ![Sınav Notları](screenshots/ogrenci/6.png)
   
7. **Sınav Takvimi**
   ![Sınav Takvimi](screenshots/ogrenci/7.png)
   
8. **Ders Kayıt**
   ![Ders Kayıt](screenshots/ogrenci/8.png)

### Öğretim Elemanı Paneli

1. **Özlük Bilgileri**
   ![Özlük Bilgileri](screenshots/egitmen/1.png)
   
2. **Danışmanlık Yapılan Öğrenciler**
   ![Danışmanlık Yapılan Öğrenciler](screenshots/egitmen/2.png)
   
3. **Bölüm Müfredatı**
   ![Bölüm Müfredatı](screenshots/egitmen/3.png)
   
4. **Verilen Dersler**
   ![Verilen Dersler](screenshots/egitmen/4.png)
   
5. **Ders Kayıt Onayı**
   ![Ders Kayıt Onayı](screenshots/egitmen/5.png)
   
6. **Ders Programı**
   ![Ders Programı](screenshots/egitmen/6.png)
   
7. **Sınav Tanımlama**
   ![Sınav Tanımlama](screenshots/egitmen/7.png)
   
8. **Sınav Listesi**
   ![Sınav Listesi](screenshots/egitmen/8.png)
   
9. **Sınav Not Girişi**
   ![Sınav Not Girişi](screenshots/egitmen/9.png)
