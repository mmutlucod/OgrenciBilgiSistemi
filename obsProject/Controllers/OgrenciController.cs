
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using obsProject.Data;
using obsProject.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.IO;
using PdfSharp.Fonts;
using PdfSharp.UniversalAccessibility.Drawing;
using Microsoft.Extensions.Hosting.Internal;
using PdfSharpCore;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace obsProject.Controllers
{
    //Kullanıcı Türü sadece 'User' olan kişiler bu sayfaya erişebilir. Yani Öğrenciler
    [Authorize(Roles = "User")]
    public class OgrenciController : Controller
    {
        private CodeFirstDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        PdfDocument document;
        IFontResolver fontResolver;

        //context veri tabanı bağlantısı oluşturmak için yapıcı metotta tanımlandı.
        //hostingEnvironment logo ve diğer dosya konumlarını kullanmak için burada tanımlandı.
        public OgrenciController(CodeFirstDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;

            if (document != null)
            {
                document.Dispose();
            }
        }

        public IActionResult Index()
        {
            //Giriş yaptığını göstermek için oluşturulan sayfa
            //Giriş yapan kişinin Adı Soyadı ve Öğrenci/öğretim Elemanı olduğunu belirten kod

            ViewBag.KullaniciAdi = User.Identity.Name;
            ViewBag.KullaniciTuru = User.Claims.FirstOrDefault(c => c.Type == "KullaniciTuru").Value;

            return View();
        }
        public async Task<IActionResult> CikisYap()
        {
            //async olarak oluşturulan logout metodu session'daki veriyi temizler.
            //cookie bilgileri session'da tutulmaktadır.

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear(); // Session'ı temizleme

            return RedirectToAction("Index");
        }
        public IActionResult Profil()
        {
            
            //Sisteme giriş yapan kullanıcının id'sini cookie den çekiyoruz.
            var kullaniciID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "KullaniciID").Value);

            //Öğrenci profili olduğu için öğrenci için gerekli sorgu
            //Kullanıcı id'ye göre profil bilgilerini çekme
            var query = (from ogrenci in _context.Ogrenci
                         join cinsiyet in _context.Cinsiyet on ogrenci.CinsiyetID equals cinsiyet.CinsiyetID into cinsiyetJoin
                         from cinsiyet in cinsiyetJoin.DefaultIfEmpty()
                         join bolum in _context.Bolum on ogrenci.BolumID equals bolum.BolumID into bolumJoin
                         from bolum in bolumJoin.DefaultIfEmpty()
                         join durum in _context.OgrenciDurum on ogrenci.OgrenciDurumID equals durum.OgrenciDurumID into durumJoin
                         from durum in durumJoin.DefaultIfEmpty()
                         join danismanlik in _context.Danismanlik on ogrenci.OgrenciID equals danismanlik.OgrenciID into danismanlikJoin
                         from danismanlik in danismanlikJoin.DefaultIfEmpty()
                         join ogretimElemani in _context.OgretimElemani on danismanlik.OgretimElemaniID equals ogretimElemani.OgretimElemaniID into ogretimElemaniJoin
                         from ogretimElemani in ogretimElemaniJoin.DefaultIfEmpty()
                         where ogrenci.KullaniciID == kullaniciID
                         select new
                         {
                             Adi = ogrenci.Adi ?? "--",
                             Soyadi = ogrenci.Soyadi ?? "--",
                             TCKimlikNo = ogrenci.TCKimlikNo ?? "--",
                             DogumTarihi = ogrenci.DogumTarihi == "0" ? "--" : ogrenci.DogumTarihi,
                             BolumAdi = bolum.BolumAdi ?? "--",
                             KayitTarihi = ogrenci.KayitTarihi == "0" ? "--" : ogrenci.KayitTarihi,
                             AyrilmaTarihi = ogrenci.AyrilmaTarihi == "0" ? "--" : ogrenci.AyrilmaTarihi,
                             DanismanAdi = ogretimElemani.Adi ?? "--",
                             DanismanSoyadi = ogretimElemani.Soyadi ?? "--",
                             OgrenciDurum = durum.OgrenciDurumAdi ?? "--",
                             OgrenciNo = ogrenci.OgrenciNo ?? "--",
                         });


            var result = query.ToList();

            ViewBag.resultData = result; //view a aktarılıyor.

            return View();

        }
        [HttpGet]
        public IActionResult Mufredat()
        {
            //Sisteme giriş yapan kullanıcının id'sini cookie den çekiyoruz.
            var kullaniciID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "KullaniciID").Value);

            //eklenen dönemlere göre son dönemi çekiyoruz.
            var sonDonem = _context.AkademikDonem.OrderByDescending(d => d.AkademikDonemID).FirstOrDefault();
            
            //Giriş yapan öğrencinin kim olduğunu çeken sorgu!!!
            //Daha sonra bu öğrenci üzerinden işlemler yapılacak
            var ogrenci = _context.Ogrenci.FirstOrDefault(x => x.KullaniciID == kullaniciID);

            //sondonem'e ait müfredatların çekilmesi
            var query = from mufredat in _context.Mufredat
                        join dersHavuzu in _context.DersHavuzu on mufredat.DersHavuzuID equals dersHavuzu.DersHavuzuID
                        join dersTuru in _context.DersTuru on dersHavuzu.DersTuruID equals dersTuru.DersTuruID
                        where mufredat.BolumID == ogrenci.BolumID && mufredat.AkademikDonemID == sonDonem.AkademikDonemID
                        select new
                        {
                            DersKodu = dersHavuzu.DersKodu,
                            DersAdi = dersHavuzu.DersAdi,
                            DersTuruAdi = dersTuru.DersTuruAdi,
                            DersDonemi = mufredat.DersDonemi,
                            Teorik = dersHavuzu.Teorik,
                            Uygulama = dersHavuzu.Uygulama,
                            Kredi = dersHavuzu.Kredi,
                            ECTS = dersHavuzu.ECTS
                        };
            var result = query.ToList();

            ViewBag.resultData = result; //View a veri gönderimi

            //Akademik Dönemlerin select içerisinde listelenmesi
            var akademikDonemler = _context.AkademikDonem.ToList();
            ViewBag.akademikDonemler = akademikDonemler;


            ViewBag.AkademikDonemID = sonDonem.AkademikDonemID; //Son dönem !!

            return View();
        }
        [HttpPost]
        public IActionResult Mufredat([FromBody] DonemModel model)
        {
            //Select den seçilne akademik döneme göre müfredatları çekme
            //JavaScript tarafında Ajax ile post işlemi

            var kullaniciID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "KullaniciID").Value);

            var ogrenci = _context.Ogrenci.FirstOrDefault(x => x.KullaniciID == kullaniciID);

            var sonDonem = Convert.ToInt32(model.SelectedDonem);

            var query = from mufredat in _context.Mufredat
                        join dersHavuzu in _context.DersHavuzu on mufredat.DersHavuzuID equals dersHavuzu.DersHavuzuID
                        join dersTuru in _context.DersTuru on dersHavuzu.DersTuruID equals dersTuru.DersTuruID
                        where mufredat.BolumID == ogrenci.BolumID && mufredat.AkademikDonemID == sonDonem
                        select new
                        {
                            DersKodu = dersHavuzu.DersKodu,
                            DersAdi = dersHavuzu.DersAdi,
                            DersTuruAdi = dersTuru.DersTuruAdi,
                            DersDonemi = mufredat.DersDonemi,
                            Teorik = dersHavuzu.Teorik,
                            Uygulama = dersHavuzu.Uygulama,
                            Kredi = dersHavuzu.Kredi,
                            ECTS = dersHavuzu.ECTS
                        };
            var result = query.ToList();

            return Json(result); // JSON formatında veri döndür
            //JSON formatında dönen veri viewda kullanılacak!!
        }
        [HttpGet]
        public IActionResult Sinav()
        {
            var kullaniciID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "KullaniciID").Value);
            var sonDonem = _context.AkademikDonem.OrderByDescending(d => d.AkademikDonemID).FirstOrDefault();

            //Öğrencinin girilen sınav notlarına göre Sınav Notlarını çekme //Vize,Final,Bütünleme,TekDersSınavı
            var query = from d in _context.Degerlendirme
                        join s in _context.Sinav on d.SinavID equals s.SinavID
                        join st in _context.SinavTuru on s.SinavTuruID equals st.SinavTuruID
                        join o in _context.Ogrenci on d.OgrenciID equals o.OgrenciID
                        join da in _context.DersAcma on s.DersAcmaID equals da.DersAcmaID
                        join m in _context.Mufredat on da.MufredatID equals m.MufredatID
                        join dh in _context.DersHavuzu on m.DersHavuzuID equals dh.DersHavuzuID
                        join k in _context.Kullanici on o.KullaniciID equals k.KullaniciID
                        join ad in _context.AkademikDonem on m.AkademikDonemID equals ad.AkademikDonemID
                        where o.KullaniciID == kullaniciID && ad.AkademikDonemID == sonDonem.AkademikDonemID
                        group new { d, s, st } by new { dh.DersAdi, dh.DersKodu } into grouped
                        select new
                        {
                            DersAdi = grouped.Key.DersAdi,
                            DersKodu = grouped.Key.DersKodu,
                            SinavTuruAdi1 = grouped.Where(g => g.st.SinavTuruID == 1).Select(g => g.st.SinavTuruAdi).FirstOrDefault(),
                            SinavTuruAdi2 = grouped.Where(g => g.st.SinavTuruID == 2).Select(g => g.st.SinavTuruAdi).FirstOrDefault(),
                            SinavTuruAdi3 = grouped.Where(g => g.st.SinavTuruID == 3).Select(g => g.st.SinavTuruAdi).FirstOrDefault(),
                            SinavTuruAdi4 = grouped.Where(g => g.st.SinavTuruID == 4).Select(g => g.st.SinavTuruAdi).FirstOrDefault(),
                            VizeNotu = grouped.Where(g => g.st.SinavTuruID == 1).Select(g => g.d.SinavNotu).FirstOrDefault(),
                            FinalNotu = grouped.Where(g => g.st.SinavTuruID == 2).Select(g => g.d.SinavNotu).FirstOrDefault(),
                            ButunlemeNotu = grouped.Where(g => g.st.SinavTuruID == 3).Select(g => g.d.SinavNotu).FirstOrDefault(),
                            TekDersNotu = grouped.Where(g => g.st.SinavTuruID == 4).Select(g => g.d.SinavNotu).FirstOrDefault(),
                            VizeEtkiOrani = grouped.Where(g => g.st.SinavTuruID == 1).Select(g => g.s.EtkiOrani).FirstOrDefault(),
                            FinalEtkiOrani = grouped.Where(g => g.st.SinavTuruID == 2).Select(g => g.s.EtkiOrani).FirstOrDefault(),
                            ButunlemeEtkiOrani = grouped.Where(g => g.st.SinavTuruID == 3).Select(g => g.s.EtkiOrani).FirstOrDefault(),
                            TekDersSinaviEtkiOrani = grouped.Where(g => g.st.SinavTuruID == 4).Select(g => g.s.EtkiOrani).FirstOrDefault(),
                        };

            var result = query.ToList();

            ViewBag.resultData = result;

            return View();
        }
        [HttpGet]
        public IActionResult DersKayit()
        {
            var kullaniciID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "KullaniciID").Value);

            var sonDonem = _context.AkademikDonem.OrderByDescending(d => d.AkademikDonemID).FirstOrDefault();

            //DersKayit sayfasındaki açılan derslerin öğrenci tarafında çeken sorgu
            
            var query = from dersAcma in _context.DersAcma
                        join akademikDonem in _context.AkademikDonem on dersAcma.AkademikDonemID equals akademikDonem.AkademikDonemID
                        join mufredat in _context.Mufredat on dersAcma.MufredatID equals mufredat.MufredatID
                        join dersHavuzu in _context.DersHavuzu on mufredat.DersHavuzuID equals dersHavuzu.DersHavuzuID
                        join dersTuru in _context.DersTuru on dersHavuzu.DersTuruID equals dersTuru.DersTuruID
                        join bolum in _context.Bolum on mufredat.BolumID equals bolum.BolumID
                        where akademikDonem.AkademikDonemID == sonDonem.AkademikDonemID 
                        select new
                        {
                            DersAcmaID = dersAcma.DersAcmaID,
                            DersKodu = dersHavuzu.DersKodu,
                            DersAdi = dersHavuzu.DersAdi,
                            DersTuruAdi = dersTuru.DersTuruAdi,
                            Teorik = dersHavuzu.Teorik,
                            Uygulama = dersHavuzu.Uygulama,
                            Kredi = dersHavuzu.Kredi,
                            ECTS = dersHavuzu.ECTS
                        };

            


            var result = query.ToList();

            ViewBag.resultData = result;

            var query1 = from dersAlma in _context.DersAlma
                        join dersAcma in _context.DersAcma on dersAlma.DersAcmaID equals dersAcma.DersAcmaID
                        join akademikDonem in _context.AkademikDonem on dersAcma.AkademikDonemID equals akademikDonem.AkademikDonemID
                        join mufredat in _context.Mufredat on dersAcma.MufredatID equals mufredat.MufredatID
                        join dersHavuzu in _context.DersHavuzu on mufredat.DersHavuzuID equals dersHavuzu.DersHavuzuID
                        join dersTuru in _context.DersTuru on dersHavuzu.DersTuruID equals dersTuru.DersTuruID
                        join bolum in _context.Bolum on mufredat.BolumID equals bolum.BolumID
                        join ogrenci in _context.Ogrenci on dersAlma.OgrenciID equals ogrenci.OgrenciID
                        join dersDurum in _context.DersDurum on dersAlma.DersDurumID equals dersDurum.DersDurumID
                        where akademikDonem.AkademikDonemID == sonDonem.AkademikDonemID && ogrenci.KullaniciID == kullaniciID
                        select new
                        {
                            DersDurumID = dersDurum.DersDurumID,
                            DersAlmaID = dersAlma.DersAlmaID,
                            DersAcmaID = dersAcma.DersAcmaID,
                            DersDurumAdi = dersDurum.DersDurumAdi,
                            DersKodu = dersHavuzu.DersKodu,
                            DersAdi = dersHavuzu.DersAdi,
                            DersTuruAdi = dersTuru.DersTuruAdi,
                            Teorik = dersHavuzu.Teorik,
                            Uygulama = dersHavuzu.Uygulama,
                            Kredi = dersHavuzu.Kredi,
                            ECTS = dersHavuzu.ECTS
                        };

            var result1 = query1.ToList();

            ViewBag.resultData1 = result1;

            var ogr = _context.Ogrenci.FirstOrDefault(x => x.KullaniciID == kullaniciID);

            ViewBag.ogrenci = ogr;

            return View();
        }
        [HttpPost]
        public IActionResult DersEkle(DersAlma model)
        {
            if(model != null)
            {
                var sonDonem = _context.AkademikDonem.OrderByDescending(d => d.AkademikDonemID).FirstOrDefault();

                //Seçilen dersin tekrar seçilip seçilmediğini kontrol etme
                var mevcutKayit = _context.DersAlma
                    .FirstOrDefault(x => x.DersAcmaID == model.DersAcmaID && x.OgrenciID == model.OgrenciID);

                //Eğer o ders seçilmemişse
                if (mevcutKayit == null)
                {
                    _context.DersAlma.Add(model);
                    _context.SaveChanges();

                    //Sistemde giriş yapmış öğrencinin seçtiği derslerin toplam akts sini kontrol et.
                    var aktsKontrol = (from dersAlma in _context.DersAlma
                                       join dersAcma in _context.DersAcma on dersAlma.DersAcmaID equals dersAcma.DersAcmaID
                                       join mufredat in _context.Mufredat on dersAcma.MufredatID equals mufredat.MufredatID
                                       join dersHavuzu in _context.DersHavuzu on mufredat.DersHavuzuID equals dersHavuzu.DersHavuzuID
                                       where dersAlma.OgrenciID == model.OgrenciID && mufredat.AkademikDonemID == sonDonem.AkademikDonemID
                                       select dersHavuzu.ECTS).Sum();

                    //AKTS 30dan büyükse dersi ekleme.
                    if (aktsKontrol > 30)
                    {
                        _context.DersAlma.Remove(model);
                        _context.SaveChanges();
                        TempData["Message"] = "Seçilen derslerin Toplam AKTS'si 30'u geçemez!";
                        return RedirectToAction("DersKayit", "Ogrenci");
                    }
                    else //30'dan küçükse dersi ekle ve redirect yap
                    {
                        return RedirectToAction("DersKayit", "Ogrenci");
                    }

                }
                else// Ders Seçilmişse TempData ile view a uyarı mesajı gönder
                {
                    TempData["Message"] = "Bu dersi zaten seçtiniz!";
                    return RedirectToAction("DersKayit", "Ogrenci");
                }
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult DersSil(DersAlma model)
        {
            //Seçilen dersi çıkarma işlemi
            var drsAlma = _context.DersAlma.FirstOrDefault(x=> x.DersAlmaID == model.DersAlmaID);
            if(drsAlma != null)
            {
                _context.DersAlma.Remove(drsAlma);
                _context.SaveChanges();
                return RedirectToAction("DersKayit", "Ogrenci");
            }
            return RedirectToAction("NotFound");
        }
        [HttpGet]
        public IActionResult SinavTakvimi()
        {
            var kullaniciID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "KullaniciID").Value);

            var sonDonem = _context.AkademikDonem.OrderByDescending(d => d.AkademikDonemID).FirstOrDefault();


            //Sınavı olan dersleri Sınav Tarihine göre çekme //Yeniden eskiye
            var query = from dersAlma in _context.DersAlma
                        join dersAcma in _context.DersAcma on dersAlma.DersAcmaID equals dersAcma.DersAcmaID
                        join ogrenci in _context.Ogrenci on dersAlma.OgrenciID equals ogrenci.OgrenciID
                        join sinav in _context.Sinav on dersAcma.DersAcmaID equals sinav.DersAcmaID
                        join mufredat in _context.Mufredat on dersAcma.MufredatID equals mufredat.MufredatID
                        join dersHavuzu in _context.DersHavuzu on mufredat.DersHavuzuID equals dersHavuzu.DersHavuzuID
                        join sinavTuru in _context.SinavTuru on sinav.SinavTuruID equals sinavTuru.SinavTuruID
                        join derslik in _context.Derslik on sinav.DerslikID equals derslik.DerslikID
                        join akademikDonem in _context.AkademikDonem on dersAcma.AkademikDonemID equals akademikDonem.AkademikDonemID
                        where ogrenci.KullaniciID == kullaniciID && akademikDonem.AkademikDonemID == sonDonem.AkademikDonemID
                        orderby sinav.SinavTarihi descending
                        select new
                        {
                            DersAdi = dersHavuzu.DersAdi,
                            DersKodu = dersHavuzu.DersKodu,
                            SinavTuruAdi = sinavTuru.SinavTuruAdi,
                            SinavTarihi = sinav.SinavTarihi,
                            EtkiOrani = sinav.EtkiOrani,
                            SinavSaati = sinav.SinavSaati,
                            DerslikAdi = derslik.DerslikAdi
                        };

            var result = query.ToList();

            ViewBag.resultData = result;

            return View();
        }
        [HttpGet]
        public IActionResult DersProgrami()
        {
            var kullaniciID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "KullaniciID").Value);

            var sonDonem = _context.AkademikDonem.OrderByDescending(d => d.AkademikDonemID).FirstOrDefault();


            //Sisteme girilen ders programını çekme //Sadece öğrenci tarrafından alınan dersin ders programı çekilmektedir.
            var query = (from dersAlma in _context.DersAlma
                        join dersAcma in _context.DersAcma on dersAlma.DersAcmaID equals dersAcma.DersAcmaID
                        join akademikDonem in _context.AkademikDonem on dersAcma.AkademikDonemID equals akademikDonem.AkademikDonemID
                        join akademikYil in _context.AkademikYil on dersAcma.AkademikYilID equals akademikYil.AkademikYilID
                        join ogretimElemani in _context.OgretimElemani on dersAcma.OgretimElemaniID equals ogretimElemani.OgretimElemaniID
                        join mufredat in _context.Mufredat on dersAcma.MufredatID equals mufredat.MufredatID
                        join dersHavuzu in _context.DersHavuzu on mufredat.DersHavuzuID equals dersHavuzu.DersHavuzuID
                        join dersTuru in _context.DersTuru on dersHavuzu.DersTuruID equals dersTuru.DersTuruID
                        join dersAcmaDersSaati in _context.DersAcmaDersSaati on dersAcma.DersAcmaID equals dersAcmaDersSaati.DersAcmaID
                        join dersSaati in _context.DersSaati on dersAcmaDersSaati.DersSaatiID equals dersSaati.DersSaatiID
                        join dersProgrami in _context.DersProgrami on dersAcma.DersAcmaID equals dersProgrami.DersAcmaID
                        join gun in _context.Gun on dersProgrami.GunID equals gun.GunID
                        join ogrenci in _context.Ogrenci on dersAlma.OgrenciID equals ogrenci.OgrenciID 
                        // Öğrenci ilişkisini ekleyerek, sadece öğrencinin ders programını al
                        where ogrenci.KullaniciID == kullaniciID && akademikDonem.AkademikDonemID == sonDonem.AkademikDonemID && dersAlma.DersDurumID == 3
                        select new
                        {
                            GunId = gun.GunID,
                            GunAdi = gun.GunAdi,
                            DersSaatiAdi = dersSaati.DersSaatiAdi,
                            DersSaatiID = dersSaati.DersSaatiID,
                            DersId = dersHavuzu.DersHavuzuID,
                            DersAdi = dersHavuzu.DersAdi,
                            OgretimElemaniId = ogretimElemani.OgretimElemaniID,
                            OgretimElemaniAdi = ogretimElemani.Adi,
                            OgretimElemaniSoyadi = ogretimElemani.Soyadi
                        }).OrderBy(x => x.DersSaatiID)
                        .OrderBy(x => x.GunId);

            var result = query.ToList();
            ViewBag.resultData = result;

            return View();
        }
        [HttpGet]
        public IActionResult Ders()
        {
            var kullaniciID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "KullaniciID").Value);
            var sonDonem = _context.AkademikDonem.OrderByDescending(d => d.AkademikDonemID).FirstOrDefault();

            //Alınan ders ve derse ait bilgileri çeken sorgu
            var query = from dersAlma in _context.DersAlma
                        join dersAcma in _context.DersAcma on dersAlma.DersAcmaID equals dersAcma.DersAcmaID
                        join akademikDonem in _context.AkademikDonem on dersAcma.AkademikDonemID equals akademikDonem.AkademikDonemID
                        join akademikYil in _context.AkademikYil on dersAcma.AkademikYilID equals akademikYil.AkademikYilID
                        join mufredat in _context.Mufredat on dersAcma.MufredatID equals mufredat.MufredatID
                        join dersHavuzu in _context.DersHavuzu on mufredat.DersHavuzuID equals dersHavuzu.DersHavuzuID
                        join dersTuru in _context.DersTuru on dersHavuzu.DersTuruID equals dersTuru.DersTuruID
                        join ogrenci in _context.Ogrenci on dersAlma.OgrenciID equals ogrenci.OgrenciID
                        where ogrenci.KullaniciID == kullaniciID && akademikDonem.AkademikDonemID == sonDonem.AkademikDonemID
                        select new
                        {
                            DersKodu = dersHavuzu.DersKodu,
                            DersAdi = dersHavuzu.DersAdi,
                            DersTuruAdi = dersTuru.DersTuruAdi,
                            DersDonemi = mufredat.DersDonemi,
                            Teorik = dersHavuzu.Teorik,
                            Uygulama = dersHavuzu.Uygulama,
                            Kredi = dersHavuzu.Kredi,
                            ECTS = dersHavuzu.ECTS
                        };
            var result = query.ToList();

            ViewBag.ResultData = result;

            return View();
        }
        [HttpGet]
        public IActionResult TranskriptDosyasi()
        {
            //return File : Buradan sonuç file olarak dönmektedir.View olarak değil !
            //Önce üst kısımda başlık ve altında öğrenci bilgilerinin olduğu bir kutu çizildi.
            //Ardından alt kısımda her rowda 2 box çizildi boxlar arası =>>> margin:10 => top/bottom and left/right
            //Her kutu içerisine dersin dönem bilgisine göre (tek ise güz, çift ise bahar) veriler çekildi.
            //DersDönemi tek ise => Öğrenci sınıfı = ((dersDonemi + 1) /2)
            //DersDönemi çift ise =>  Öğrenci sınıfı = ((dersDonemi) /2)
            //Daha sonra alt tarafta ilgili dersin girilen sınav notlarına göre önce ortalama hesaplandı
            //ardından hesaplanan ortalamaya göre harf notu hesaplandı.
            //Son olarak her satırda ders kodu, ders adı, ortalama, harf notu gibi veriler her kutuya eklendi


            var kullaniciID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "KullaniciID").Value);
            var sonDonem = _context.AkademikDonem.OrderByDescending(d => d.AkademikDonemID).FirstOrDefault();

            fontResolver = new CustomFontResolver();

            if (GlobalFontSettings.FontResolver == null)
            {
                GlobalFontSettings.FontResolver = fontResolver;
            }

            // Yeni bir PDF belgesi oluştur
            document = new PdfDocument();
            //Döküman Başlığı!!
            document.Info.Title = "Report";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Ardından XFont'u istediğiniz gibi oluşturabilirsiniz
            XFont font = new XFont("Arial", 12); // Doğru XFont syntax'ını kullanın

            XStringFormat format = new XStringFormat();
            format.Alignment = XStringAlignment.Center; // Metni yatay olarak ortala
            format.LineAlignment = XLineAlignment.Center; // Metni dikey olarak ortala


            //LOGO EKLEME İŞLEMLERİ !!!!
            string logoPath = _hostingEnvironment.WebRootPath + "/images/logo.png";

            XImage image = XImage.FromFile(logoPath);

            double imageWidth = 50;
            double imageHeight = 50;

            double x1 = (page.Width.Point - 540) / 2; // Sol kenardan 5 piksel boşluk
            double y1 = 12; // Üst kenardan 10 piksel boşluk

            gfx.DrawImage(image, x1, y1, imageWidth, imageHeight);


            string[] lines = { "T.C.", "Bandırma Onyedi Eylül Üniversitesi", "Mühendislik ve Doğa Bilimleri Fakültesi", "TRANSKRİPT BELGESİ" };
            double yPos = 5; // Başlangıç Y pozisyonu

            foreach (string line in lines)
            {
                XSize size = gfx.MeasureString(line, font);
                double xPos = (page.Width.Point - size.Width) / 2; // Metnin yatay ortalaması
                gfx.DrawString(line, font, XBrushes.Black, new XRect(xPos, yPos, size.Width, size.Height), format);
                yPos += size.Height; // Bir sonraki satırın Y pozisyonu
            }


            var ogrenci = _context.Ogrenci.FirstOrDefault(o => o.KullaniciID == kullaniciID); // Örnek bir sorgu, öğrenci bilgilerini almak için gerekli sorguyu yapmalısınız
            var ogrenciDurum = _context.OgrenciDurum.FirstOrDefault(od => od.OgrenciDurumID == ogrenci.OgrenciDurumID);
            var ogrenciBolum = _context.Bolum.FirstOrDefault(b => b.BolumID == ogrenci.BolumID);

            double boxWidth = 540;
            double boxHeight = 100;
            double boxX = (page.Width.Point - boxWidth) / 2;
            double boxY = yPos + 10;

            gfx.DrawRectangle(XPens.Black, XBrushes.Transparent, boxX, boxY, boxWidth, boxHeight); // Kutuyu çiz

            XStringFormat textFormat = new XStringFormat();
            textFormat.Alignment = XStringAlignment.Near;
            textFormat.LineAlignment = XLineAlignment.Near;

            //// Soldaki bilgileri yerleştirme
            double textX = boxX + 20;
            double textY = boxY + 10;

            gfx.DrawString($"Adı Soyadı: {ogrenci.Adi}  {ogrenci.Soyadi}", font, XBrushes.Black, new XRect(textX, textY, boxWidth - 20, boxHeight), textFormat);
            gfx.DrawString($"TC Kimlik No: {ogrenci.TCKimlikNo}", font, XBrushes.Black, new XRect(textX, textY + 20, boxWidth - 20, boxHeight), textFormat);
            gfx.DrawString($"Öğrenci No: {ogrenci.OgrenciNo}", font, XBrushes.Black, new XRect(textX, textY + 40, boxWidth - 20, boxHeight), textFormat);
            gfx.DrawString($"Bölüm: {ogrenciBolum.BolumAdi}", font, XBrushes.Black, new XRect(textX, textY + 60, boxWidth - 20, boxHeight), textFormat);

            // Sağdaki bilgileri yerleştirme
            textX = boxX + boxWidth / 2 + 20;
            textY = boxY + 10;

            gfx.DrawString($"Tarih: {DateTime.Now}", font, XBrushes.Black, new XRect(textX, textY, boxWidth - 20, boxHeight), textFormat);
            gfx.DrawString($"Kayıt Tarihi: {(ogrenci.KayitTarihi == "0" ? "" : ogrenci.KayitTarihi)}", font, XBrushes.Black, new XRect(textX, textY + 20, boxWidth - 20, boxHeight), textFormat);
            gfx.DrawString($"Ayrılma Tarihi: {(ogrenci.AyrilmaTarihi == "0" ? "" : ogrenci.AyrilmaTarihi)}", font, XBrushes.Black, new XRect(textX, textY + 40, boxWidth - 20, boxHeight), textFormat);
            gfx.DrawString($"Öğrenim Durumu: {ogrenciDurum.OgrenciDurumAdi}", font, XBrushes.Black, new XRect(textX, textY + 60, boxWidth - 20, boxHeight), textFormat);

                var query = from d in _context.Degerlendirme
                            join s in _context.Sinav on d.SinavID equals s.SinavID
                            join st in _context.SinavTuru on s.SinavTuruID equals st.SinavTuruID
                            join o in _context.Ogrenci on d.OgrenciID equals o.OgrenciID
                            join da in _context.DersAcma on s.DersAcmaID equals da.DersAcmaID
                            join m in _context.Mufredat on da.MufredatID equals m.MufredatID
                            join dh in _context.DersHavuzu on m.DersHavuzuID equals dh.DersHavuzuID
                            join k in _context.Kullanici on o.KullaniciID equals k.KullaniciID
                            join ad in _context.AkademikDonem on m.AkademikDonemID equals ad.AkademikDonemID
                            where o.KullaniciID == kullaniciID
                            group new { d, s, st, m } by new { dh.DersAdi, dh.DersKodu, m.DersDonemi } into grouped
                            select new
                            {
                                DersAdi = grouped.Key.DersAdi,
                                DersKodu = grouped.Key.DersKodu,
                                SinavTuruAdi1 = grouped.Where(g => g.st.SinavTuruID == 1).Select(g => g.st.SinavTuruAdi).FirstOrDefault(),
                                SinavTuruAdi2 = grouped.Where(g => g.st.SinavTuruID == 2).Select(g => g.st.SinavTuruAdi).FirstOrDefault(),
                                SinavTuruAdi3 = grouped.Where(g => g.st.SinavTuruID == 3).Select(g => g.st.SinavTuruAdi).FirstOrDefault(),
                                SinavTuruAdi4 = grouped.Where(g => g.st.SinavTuruID == 4).Select(g => g.st.SinavTuruAdi).FirstOrDefault(),
                                VizeNotu = grouped.Where(g => g.st.SinavTuruID == 1).Select(g => g.d.SinavNotu).FirstOrDefault(),
                                FinalNotu = grouped.Where(g => g.st.SinavTuruID == 2).Select(g => g.d.SinavNotu).FirstOrDefault(),
                                ButunlemeNotu = grouped.Where(g => g.st.SinavTuruID == 3).Select(g => g.d.SinavNotu).FirstOrDefault(),
                                TekDersNotu = grouped.Where(g => g.st.SinavTuruID == 4).Select(g => g.d.SinavNotu).FirstOrDefault(),
                                VizeEtkiOrani = grouped.Where(g => g.st.SinavTuruID == 1).Select(g => g.s.EtkiOrani).FirstOrDefault(),
                                FinalEtkiOrani = grouped.Where(g => g.st.SinavTuruID == 2).Select(g => g.s.EtkiOrani).FirstOrDefault(),
                                ButunlemeEtkiOrani = grouped.Where(g => g.st.SinavTuruID == 3).Select(g => g.s.EtkiOrani).FirstOrDefault(),
                                TekDersSinaviEtkiOrani = grouped.Where(g => g.st.SinavTuruID == 4).Select(g => g.s.EtkiOrani).FirstOrDefault(),
                                DersDonemi = grouped.Key.DersDonemi
                            };

            var result = query.ToList();

            double boxWidth1 = 530 / 2; // Kutu genişliği
            double boxHeight1 = 130; // Kutu yüksekliği
            double boxMargin = 10; // Kutular arası boşluğu

            double startX = (page.Width.Point - boxWidth) / 2; // Başlangıç X pozisyonu
            double startY = boxY + boxHeight + 15;

            var dersDonemleri = (from d in _context.Degerlendirme
                                 join s in _context.Sinav on d.SinavID equals s.SinavID
                                 join st in _context.SinavTuru on s.SinavTuruID equals st.SinavTuruID
                                 join o in _context.Ogrenci on d.OgrenciID equals o.OgrenciID
                                 join da in _context.DersAcma on s.DersAcmaID equals da.DersAcmaID
                                 join m in _context.Mufredat on da.MufredatID equals m.MufredatID
                                 join dh in _context.DersHavuzu on m.DersHavuzuID equals dh.DersHavuzuID
                                 join ad in _context.AkademikDonem on m.AkademikDonemID equals ad.AkademikDonemID
                                 where o.KullaniciID == kullaniciID
                                 group m by m.DersDonemi into grouped
                                 select grouped.Key).ToList();
                
            foreach(var item in dersDonemleri)
            {
                if (item % 2 == 1)
                {
                    gfx.DrawRectangle(XPens.Black, XBrushes.Transparent, startX, startY, boxWidth1, boxHeight1);

                    string sinifBaslik = $"{(item + 1) / 2}. Sınıf Güz";
                    XSize sinifBaslikSize = gfx.MeasureString(sinifBaslik, font);
                    double sinifBaslikX = startX + ((boxWidth1 - sinifBaslikSize.Width) / 2);

                    gfx.DrawString(sinifBaslik, new XFont("Arial", 10), XBrushes.Black, new XRect(sinifBaslikX, startY, sinifBaslikSize.Width, sinifBaslikSize.Height), textFormat);

                    string[] basliklar = { "Ders Kodu", "Ders Adı", "", "", "", "Ortalama", "Harf Notu" };
                    double x = startX + 10;
                    double y = startY + 10;
                    double colWidth = (boxWidth1 - 15) / basliklar.Length;

                    foreach (var baslik in basliklar)
                    {
                        gfx.DrawString(baslik, new XFont("Arial", 8), XBrushes.Black, new XRect(x, y, colWidth, 20), textFormat);
                        x += colWidth;
                    }

                    double dataY = y + 12;


                    foreach (var item1 in result.Where(x => x.DersDonemi == item && x.SinavTuruAdi2 != null))
                    {
                        x = startX + 10;
                        gfx.DrawString(item1.DersKodu, new XFont("Arial", 8), XBrushes.Black, new XRect(x, dataY, colWidth, 20), textFormat);
                        x += colWidth;

                        gfx.DrawString(item1.DersAdi, new XFont("Arial", 8), XBrushes.Black, new XRect(x, dataY, colWidth, 20), textFormat);
                        x += colWidth + 115;

                        gfx.DrawString(OrtalamaHesapla(item1.VizeNotu, item1.FinalNotu, item1.ButunlemeNotu, item1.TekDersNotu).ToString("0.00"), new XFont("Arial", 8), XBrushes.Black, new XRect(x, dataY, colWidth, 20), textFormat);
                        x += colWidth;

                        gfx.DrawString(HarfNotuHesapla(OrtalamaHesapla(item1.VizeNotu, item1.FinalNotu, item1.ButunlemeNotu, item1.TekDersNotu)), new XFont("Arial", 8), XBrushes.Black, new XRect(x, dataY, colWidth, 20), textFormat);
                        x += colWidth;

                        dataY += 12;
                    }
                    startX += boxWidth1 + boxMargin;
                }
                if(item % 2 == 0)
                {
                    gfx.DrawRectangle(XPens.Black, XBrushes.Transparent, startX , startY, boxWidth1, boxHeight1);

                    string sinifBaslik = $"{item / 2}. Sınıf Bahar";
                    XSize sinifBaslikSize = gfx.MeasureString(sinifBaslik, font);
                    double sinifBaslikX = startX + ((boxWidth1 - sinifBaslikSize.Width) / 2);

                    gfx.DrawString(sinifBaslik, new XFont("Arial", 10), XBrushes.Black, new XRect(sinifBaslikX, startY, sinifBaslikSize.Width, sinifBaslikSize.Height), textFormat);

                    string[] basliklar = { "Ders Kodu", "Ders Adı", "", "", "", "Ortalama", "Harf Notu" };
                    double x = startX + 10;
                    double y = startY + 10;
                    double colWidth = (boxWidth1 - 15) / basliklar.Length;

                    foreach (var baslik in basliklar)
                    {
                        gfx.DrawString(baslik, new XFont("Arial", 8), XBrushes.Black, new XRect(x, y, colWidth, 20), textFormat);
                        x += colWidth;
                    }

                    double dataY = y + 12;

                    foreach (var item1 in result.Where(x => x.DersDonemi == item && x.SinavTuruAdi2 != null))
                    {
                        x = startX + 10;
                        gfx.DrawString(item1.DersKodu, new XFont("Arial", 8), XBrushes.Black, new XRect(x, dataY, colWidth, 20), textFormat);
                        x += colWidth;

                        gfx.DrawString(item1.DersAdi, new XFont("Arial", 8), XBrushes.Black, new XRect(x, dataY, colWidth, 20), textFormat);
                        x += colWidth + 115;

                        gfx.DrawString(OrtalamaHesapla(item1.VizeNotu, item1.FinalNotu, item1.ButunlemeNotu, item1.TekDersNotu).ToString("0.00"), new XFont("Arial", 8), XBrushes.Black, new XRect(x, dataY, colWidth, 20), textFormat);
                        x += colWidth;

                        gfx.DrawString(HarfNotuHesapla(OrtalamaHesapla(item1.VizeNotu, item1.FinalNotu, item1.ButunlemeNotu, item1.TekDersNotu)), new XFont("Arial", 8), XBrushes.Black, new XRect(x, dataY, colWidth, 20), textFormat);
                        x += colWidth;

                        dataY += 12;
                    }

                    startX = (page.Width.Point - boxWidth) / 2;
                    startY += boxHeight1 + boxMargin;
                }
            }


            // Diğer kodlar...
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;

            Response.Headers["Content-Disposition"] = "inline; filename=Transkript.pdf";

            // MemoryStream'i byte dizisine dönüştürüp base64'e kodlayarak View'e aktar
            byte[] bytes = stream.ToArray();
            string pdfBase64 = Convert.ToBase64String(bytes);

            // Base64 formatındaki PDF'i View'e aktar
            return File(Convert.FromBase64String(pdfBase64), "application/pdf");
        }
        [HttpGet]
        public IActionResult Transkript()
        {
            //view da TranskriptDosyasi'ndan iframe çekildi.
            return View();
        }






        //Ortalamayı hesaplayan fonksiyon
        public double OrtalamaHesapla(double? vize, double? final, double? butunleme, double? tekDers)
        {
            if (vize == null && final == null && butunleme == null && tekDers == null)
            {
                // Eğer hiçbir değer girilmemişse, varsayılan bir değer döndürebilirsiniz.
                return 0; // Veya istediğiniz herhangi bir varsayılan değeri verebilirsiniz.
            }

            if(vize != null && final != null && butunleme == 0 && tekDers == 0)
            {
                return (vize.Value * 0.4 + final.Value * 0.6);
            }
            if (vize != null && final != null && butunleme != null && tekDers == 0)
            {
                return (vize.Value * 0.4 + butunleme.Value * 0.6);
            }
            if (vize != null && final != null && butunleme != null && tekDers != null)
            {
                return (tekDers.Value);
            }
            else
            {
                return 0;
            }


        }
        //Harf Notu hesaplayan fonksiyon
        public string HarfNotuHesapla(double deger)
        {
            if(deger >= 90 && deger <= 100)
            {
                return "AA";
            }
            else if (deger >= 85 && deger < 90)
            {
                return "BA";
            }
            else if (deger >= 80 && deger < 85)
            {
                return "BB";
            }
            else if (deger >= 70 && deger < 80)
            {
                return "CB";
            }
            else if (deger >= 60 && deger < 70)
            {
                return "CC";
            }
            else if (deger >= 55 && deger < 60)
            {
                return "DC";
            }
            else if (deger >= 45 && deger < 55)
            {
                return "DD";
            }
            else
            {
                return "FF";
            }
        }
        //ajax ile select'den dönemin çekilmesi
        public class DonemModel
        {
            public string SelectedDonem { get; set; }
        }
    }
}
