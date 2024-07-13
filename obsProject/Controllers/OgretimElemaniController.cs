using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using obsProject.Data;
using System.Security.Claims;
using obsProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;

namespace obsProject.Controllers
{
    //Kullanıcı Türü sadece 'Admin' olan kişiler bu sayfaya erişebilir. Yani Öğretim Elemanları
    [Authorize(Roles = "Admin")]
    public class OgretimElemaniController : Controller
    {
        private CodeFirstDbContext _context;

        //context veri tabanı bağlantısı oluşturmak için yapıcı metotta tanımlandı.
        public OgretimElemaniController(CodeFirstDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //Giriş yaptığını göstermek için oluşturulan sayfa
            //Giriş yapan kişinin Adı Soyadı ve Öğrenci/öğretim Elemanı olduğunu belirten kod

            ViewBag.KullaniciAdi = User.Identity.Name;
            ViewBag.KullaniciTuru = User.Claims.FirstOrDefault(c => c.Type == "KullaniciTuru").Value;
            //ViewBag.KullaniciTuru = User.Identity.Equals(User.Identity);

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

            //Öğretim elemanı profili olduğu için öğrenci için gerekli sorgu
            //Kullanıcı id'ye göre profil bilgilerini çekme
            var query = from ogretimElemani in _context.OgretimElemani
                        join cinsiyet in _context.Cinsiyet on ogretimElemani.CinsiyetID equals cinsiyet.CinsiyetID into cinsiyetJoin
                        from cinsiyet in cinsiyetJoin.DefaultIfEmpty()
                        join bolum in _context.Bolum on ogretimElemani.BolumID equals bolum.BolumID into bolumJoin
                        from bolum in bolumJoin.DefaultIfEmpty()
                        join unvan in _context.Unvan on ogretimElemani.UnvanID equals unvan.UnvanID into unvanJoin
                        from unvan in unvanJoin.DefaultIfEmpty()
                        where ogretimElemani.KullaniciID == kullaniciID
                        select new
                        {
                            Adi = ogretimElemani.Adi ?? "--",
                            Soyadi = ogretimElemani.Soyadi ?? "--",
                            TCKimlikNo = ogretimElemani.TCKimlikNo ?? "--",
                            DogumTarihi = ogretimElemani.DogumTarihi == "0" ? " --" : ogretimElemani.DogumTarihi,
                            KurumSicilNo = ogretimElemani.KurumSicilNo ?? "--",
                            BolumAdi = bolum.BolumAdi ?? "--",
                            UnvanAdi = unvan.UnvanAdi ?? "--",
                        };

            var result = query.ToList();

            ViewBag.resultData = result;

            return View();
        }
        [HttpGet]
        public IActionResult Mufredat()
        {
            //Sisteme giriş yapan kullanıcının id'sini cookie den çekiyoruz.
            var kullaniciID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "KullaniciID").Value);
            //eklenen dönemlere göre son dönemi çekiyoruz.
            var sonDonem = _context.AkademikDonem.OrderByDescending(d => d.AkademikDonemID).FirstOrDefault();

            //Giriş yapan öğretim elemanının kim olduğunu çeken sorgu!!!
            //Daha sonra bu öğretim elemanı üzerinden işlemler yapılacak
            var ogretimElemani = _context.OgretimElemani.FirstOrDefault(x => x.KullaniciID == kullaniciID);

            //sondonem'e ait müfredatların çekilmesi
            var query = from mufredat in _context.Mufredat
                        join dersHavuzu in _context.DersHavuzu on mufredat.DersHavuzuID equals dersHavuzu.DersHavuzuID
                        join dersTuru in _context.DersTuru on dersHavuzu.DersTuruID equals dersTuru.DersTuruID
                        where mufredat.BolumID == ogretimElemani.BolumID && mufredat.AkademikDonemID == sonDonem.AkademikDonemID
                        group new { dersHavuzu, dersTuru, mufredat } by new { dersHavuzu.DersKodu, dersHavuzu.DersAdi, dersTuru.DersTuruAdi, mufredat.DersDonemi, dersHavuzu.Teorik, dersHavuzu.Uygulama, dersHavuzu.Kredi, dersHavuzu.ECTS } into grouped
                        select new
                        {
                            DersKodu = grouped.Key.DersKodu,
                            DersAdi = grouped.Key.DersAdi,
                            DersTuruAdi = grouped.Key.DersTuruAdi,
                            DersDonemi = grouped.Key.DersDonemi,
                            Teorik = grouped.Key.Teorik,
                            Uygulama = grouped.Key.Uygulama,
                            Kredi = grouped.Key.Kredi,
                            ECTS = grouped.Key.ECTS
                        };

            var result = query.ToList();


            //Akademik Dönemlerin select içerisinde listelenmesi
            var akademikDonemler = _context.AkademikDonem.ToList();

            ViewBag.akademikDonemler = akademikDonemler;

            ViewBag.resultData = result;

            ViewBag.AkademikDonemID = sonDonem.AkademikDonemID;

            return View();
        }
        [HttpPost]
        public IActionResult Mufredat([FromBody] DonemModel model)
        {
            //Select den seçilne akademik döneme göre müfredatları çekme
            //JavaScript tarafında Ajax ile post işlemi
            var kullaniciID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "KullaniciID").Value);

            var donemId = Convert.ToInt32(model.SelectedDonem);

            var ogretimElemani = _context.OgretimElemani.FirstOrDefault(x => x.KullaniciID == kullaniciID);

            var query = from mufredat in _context.Mufredat
                        join dersHavuzu in _context.DersHavuzu on mufredat.DersHavuzuID equals dersHavuzu.DersHavuzuID
                        join dersTuru in _context.DersTuru on dersHavuzu.DersTuruID equals dersTuru.DersTuruID
                        where mufredat.BolumID == ogretimElemani.BolumID && mufredat.AkademikDonemID == donemId
                        group new { dersHavuzu, dersTuru, mufredat } by new { dersHavuzu.DersKodu, dersHavuzu.DersAdi, dersTuru.DersTuruAdi, mufredat.DersDonemi, dersHavuzu.Teorik, dersHavuzu.Uygulama, dersHavuzu.Kredi, dersHavuzu.ECTS } into grouped
                        select new
                        {
                            DersKodu = grouped.Key.DersKodu,
                            DersAdi = grouped.Key.DersAdi,
                            DersTuruAdi = grouped.Key.DersTuruAdi,
                            DersDonemi = grouped.Key.DersDonemi,
                            Teorik = grouped.Key.Teorik,
                            Uygulama = grouped.Key.Uygulama,
                            Kredi = grouped.Key.Kredi,
                            ECTS = grouped.Key.ECTS
                        };

            var result = query.ToList();


            return Json(result); // JSON formatında veri döndür
            //JSON formatında dönen veri viewda kullanılacak!!
        }
        [HttpGet]
        public IActionResult DersProgrami()
        {
            var kullaniciID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "KullaniciID").Value);

            var sonDonem = _context.AkademikDonem.OrderByDescending(d => d.AkademikDonemID).FirstOrDefault();

            //Sisteme girilen ders programını çekme //Sadece öğretim elemanı tarrafından verilen dersin ders programı çekilmektedir.
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
                         where ogretimElemani.KullaniciID == kullaniciID && akademikDonem.AkademikDonemID == sonDonem.AkademikDonemID
                         group new
                         {
                             gun.GunID,
                             gun.GunAdi,
                             dersSaati.DersSaatiAdi,
                             dersSaati.DersSaatiID,
                             dersHavuzu.DersHavuzuID,
                             dersHavuzu.DersAdi,
                             ogretimElemani.OgretimElemaniID,
                             ogretimElemani.Adi,
                             ogretimElemani.Soyadi
                         } by new
                         {
                             gun.GunID,
                             gun.GunAdi,
                             dersSaati.DersSaatiAdi,
                             dersSaati.DersSaatiID,
                             dersHavuzu.DersHavuzuID,
                             dersHavuzu.DersAdi,
                             ogretimElemani.OgretimElemaniID,
                             ogretimElemani.Adi,
                             ogretimElemani.Soyadi
                         } into grouped
                         select new
                         {
                             GunId = grouped.Key.GunID,
                             GunAdi = grouped.Key.GunAdi,
                             DersSaatiAdi = grouped.Key.DersSaatiAdi,
                             DersSaatiID = grouped.Key.DersSaatiID,
                             DersId = grouped.Key.DersHavuzuID,
                             DersAdi = grouped.Key.DersAdi,
                             OgretimElemaniId = grouped.Key.OgretimElemaniID,
                             OgretimElemaniAdi = grouped.Key.Adi,
                             OgretimElemaniSoyadi = grouped.Key.Soyadi
                         }).OrderBy(grouped => grouped.DersSaatiID)
                         .OrderBy(x => x.GunId);


            var result = query.ToList();

            ViewBag.resultData = result;

            return View();
        }
        [HttpGet]
        public IActionResult Sinav()
        {
            var kullaniciID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "KullaniciID").Value);

            var sonDonem = _context.AkademikDonem.OrderByDescending(d => d.AkademikDonemID).FirstOrDefault();

            //Öğretim elemanının eklediği sınavların listelenmesi //Yeniden Eskiye
            var query = from sinav in _context.Sinav
                        join dersAcma in _context.DersAcma on sinav.DersAcmaID equals dersAcma.DersAcmaID
                        join sinavTuru in _context.SinavTuru on sinav.SinavTuruID equals sinavTuru.SinavTuruID
                        join ogretimElemani in _context.OgretimElemani on sinav.OgretimElemaniID equals ogretimElemani.OgretimElemaniID
                        join derslik in _context.Derslik on sinav.DerslikID equals derslik.DerslikID
                        join mufredat in _context.Mufredat on dersAcma.MufredatID equals mufredat.MufredatID
                        join dersHavuzu in _context.DersHavuzu on mufredat.DersHavuzuID equals dersHavuzu.DersHavuzuID
                        join akademikDonem in _context.AkademikDonem on mufredat.AkademikDonemID equals akademikDonem.AkademikDonemID
                        join kullanici in _context.Kullanici on ogretimElemani.KullaniciID equals kullanici.KullaniciID
                        where akademikDonem.AkademikDonemID == sonDonem.AkademikDonemID && kullanici.KullaniciID == kullaniciID
                        orderby sinav.SinavTarihi descending
                        select new
                        {
                            SinavID = sinav.SinavID,
                            DersKodu = dersHavuzu.DersKodu,
                            DersAdi = dersHavuzu.DersAdi,
                            SinavTuruAdi = sinavTuru.SinavTuruAdi,
                            EtkiOrani = sinav.EtkiOrani,
                            SinavTarihi = sinav.SinavTarihi,
                            SinavSaati = sinav.SinavSaati,
                            DerslikAdi = derslik.DerslikAdi,
                            GozetmenAdi = ogretimElemani.Adi,
                            GozetmenSoyadi = ogretimElemani.Soyadi
                        };

            var result = query.ToList();

            ViewBag.Sinavlar = result;

            return View();
        }
        [HttpGet]
        public IActionResult SinavEkle()
        {
            var kullaniciID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "KullaniciID").Value);

            var sonDonem = _context.AkademikDonem.OrderByDescending(d => d.AkademikDonemID).FirstOrDefault();

            //Bazı verilerin veri tabanından select'lere taşınması
            ViewBag.DersKodlari = _context.DersHavuzu.Select(x => new SelectListItem { Value = x.DersHavuzuID.ToString(), Text = $"{x.DersKodu} - {x.DersAdi}" }).ToList();
            ViewBag.SinavTurleri = _context.SinavTuru.Select(x => new SelectListItem { Value = x.SinavTuruID.ToString(), Text = x.SinavTuruAdi }).ToList();
            ViewBag.Derslikler = _context.Derslik.Select(x => new SelectListItem { Value = x.DerslikID.ToString(), Text = x.DerslikAdi }).ToList();
            ViewBag.Gozetmenler = _context.OgretimElemani.Select(x => new SelectListItem { Value = x.OgretimElemaniID.ToString(), Text = $"{x.Adi} {x.Soyadi}" }).ToList();

            var query = from m in _context.Mufredat
                        join dh in _context.DersHavuzu on m.DersHavuzuID equals dh.DersHavuzuID
                        join da in _context.DersAcma on m.MufredatID equals da.MufredatID
                        join oe in _context.OgretimElemani on da.OgretimElemaniID equals oe.OgretimElemaniID
                        join k in _context.Kullanici on oe.KullaniciID equals k.KullaniciID
                        join ad in _context.AkademikDonem on m.AkademikDonemID equals ad.AkademikDonemID
                        where k.KullaniciID == kullaniciID && m.AkademikDonemID == sonDonem.AkademikDonemID
                        select new
                        {
                            DersAdi = dh.DersAdi,
                            DersKodu = dh.DersKodu,
                            DersAcmaID = da.DersAcmaID
                        };

            var result = query.ToList();

            ViewBag.Dersler = result; // View'da kullanmak için ViewBag'e atama yapılıyor


            return View();
        }
        [HttpPost]
        public IActionResult SinavEkle(Sinav model)
        {
        // Burada sinav nesnesi veritabanına eklenmeli
        _context.Sinav.Add(model);
        _context.SaveChanges();

        return RedirectToAction("Sinav", "OgretimElemani"); // Ekleme başarılıysa yönlendirme yapılabilir
            
        }
        [HttpGet]
        public IActionResult SinavDuzenle(int id)
        {
            //Düzenlenecek sınavın var oluğ olmadığını kontrol etme
            var sinav = _context.Sinav.FirstOrDefault(a => a.SinavID == id);

            if(sinav == null)
            {
                return NotFound();
            }

            var kullaniciID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "KullaniciID").Value);

            var sonDonem = _context.AkademikDonem.OrderByDescending(d => d.AkademikDonemID).FirstOrDefault();

            ViewBag.DersKodlari = _context.DersHavuzu.Select(x => new SelectListItem { Value = x.DersHavuzuID.ToString(), Text = $"{x.DersKodu} - {x.DersAdi}" }).ToList();
            ViewBag.SinavTurleri = _context.SinavTuru.Select(x => new SelectListItem { Value = x.SinavTuruID.ToString(), Text = x.SinavTuruAdi }).ToList();
            ViewBag.Derslikler = _context.Derslik.Select(x => new SelectListItem { Value = x.DerslikID.ToString(), Text = x.DerslikAdi }).ToList();
            ViewBag.Gozetmenler = _context.OgretimElemani.Select(x => new SelectListItem { Value = x.OgretimElemaniID.ToString(), Text = x.Adi }).ToList();

            var query = from m in _context.Mufredat
                        join dh in _context.DersHavuzu on m.DersHavuzuID equals dh.DersHavuzuID
                        join da in _context.DersAcma on m.MufredatID equals da.MufredatID
                        join oe in _context.OgretimElemani on da.OgretimElemaniID equals oe.OgretimElemaniID
                        join k in _context.Kullanici on oe.KullaniciID equals k.KullaniciID
                        join ad in _context.AkademikDonem on m.AkademikDonemID equals ad.AkademikDonemID
                        where k.KullaniciID == kullaniciID && m.AkademikDonemID == sonDonem.AkademikDonemID
                        select new
                        {
                            DerslikID = sinav.DerslikID,
                            OgretimElemaniID = sinav.OgretimElemaniID,
                            SinavTuruID = sinav.SinavTuruID,
                            DersAdi = dh.DersAdi,
                            DersKodu = dh.DersKodu,
                            DersAcmaID = da.DersAcmaID,
                            SinavTarihi = sinav.SinavTarihi,
                            SinavSaati = sinav.SinavSaati,
                        };

            var result = query.ToList();

            ViewBag.Dersler = result; // View'da kullanmak için ViewBag'e atama yapılıyor
            ViewBag.SinavBilgileri = sinav;

            //sinav bilgilerinin view a aktarılması
            return View(sinav);
        }
        [HttpGet]
        public IActionResult Degerlendirme(int id)
        {
            //Not giriş Ekranı
            //Dersi alan öğrencilerin listesi gelir.
            //Notu girilen öğrenci üzerinde düzenleme yapılır.
            //Notu girilmeyen öğrenciye not ekleme işlemi yapılır.



           var sinav1 = _context.Sinav.FirstOrDefault(x => x.SinavID == id);

           if(sinav1 != null)
            {
                var kullaniciID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "KullaniciID").Value);

                var sonDonem = _context.AkademikDonem.OrderByDescending(d => d.AkademikDonemID).FirstOrDefault();


                //Notu girilen öğrencileri çekme
                var result = (from ogrenci in _context.Ogrenci
                              join degerlendirme in _context.Degerlendirme on ogrenci.OgrenciID equals degerlendirme.OgrenciID
                              join sinav in _context.Sinav on degerlendirme.SinavID equals sinav.SinavID
                              join dersAcma in _context.DersAcma on sinav.DersAcmaID equals dersAcma.DersAcmaID
                              join mufredat in _context.Mufredat on dersAcma.MufredatID equals mufredat.MufredatID
                              where degerlendirme.SinavID == id && mufredat.AkademikDonemID == sonDonem.AkademikDonemID 
                              select new
                              {
                                  OgrenciAdi = ogrenci.Adi,
                                  OgrenciSoyadi = ogrenci.Soyadi,
                                  SinavNotu = degerlendirme.SinavNotu,
                                  OgrenciID = ogrenci.OgrenciID,
                                  DegerlendirmeID = degerlendirme.DegerlendirmeID,
                                  SinavID = sinav.SinavID

                              }).ToList();

                //Notu girilmeyen öğrencileri çekme
                var result1 = (from ogrenci in _context.Ogrenci
                             join dersAlma in _context.DersAlma on ogrenci.OgrenciID equals dersAlma.OgrenciID
                             where dersAlma.DersAcmaID == sinav1.DersAcmaID && dersAlma.DersDurumID == 3 
                             && !(from degerlendirme in _context.Degerlendirme
                               where degerlendirme.SinavID == sinav1.SinavID
                               select degerlendirme.OgrenciID)
                                .Contains(ogrenci.OgrenciID)
                             select new
                             {
                                 OgrenciAdi = ogrenci.Adi,
                                 OgrenciSoyadi = ogrenci.Soyadi,
                                 OgrenciID = ogrenci.OgrenciID,
                                 SinavNotu = "--",
                                 SinavID = id
                             }).ToList();





                ViewBag.resultData = result;
                ViewBag.resultData1 = result1;
                ViewBag.SinavBilgi = (from sinav in _context.Sinav
                                                   join dersAcma in _context.DersAcma on sinav.DersAcmaID equals dersAcma.DersAcmaID
                                                   join mufredat in _context.Mufredat on dersAcma.MufredatID equals mufredat.MufredatID
                                                   join dersHavuzu in _context.DersHavuzu on mufredat.DersHavuzuID equals dersHavuzu.DersHavuzuID
                                                   join sinavTuru in _context.SinavTuru on sinav.SinavTuruID equals sinavTuru.SinavTuruID
                                                   where sinav.SinavID == sinav1.SinavID
                                                   select new
                                                   {
                                                       DersAdi = dersHavuzu.DersAdi,
                                                       SinavTuru = sinavTuru.SinavTuruAdi
                                                   }).FirstOrDefault();


                return View();
            }
           else
            {
                return RedirectToAction("NotFound");
            }
        } 
        [HttpPost]
        public IActionResult SinavDuzenle(Sinav sinav)
        {
            //Sınav Bilgilerinin Düzenlenebileceği sayfa
        var sinav1 = _context.Sinav.FirstOrDefault(a => a.SinavID == sinav.SinavID);

        if(sinav1 != null)
        {
             sinav1.SinavID = sinav.SinavID;
                sinav1.EtkiOrani = sinav.EtkiOrani;
                sinav1.SinavTarihi = sinav.SinavTarihi;
                sinav1.SinavTarihi = sinav.SinavTarihi;
                sinav1.DersAcmaID = sinav.DersAcmaID;
                sinav1.SinavTuruID = sinav.SinavTuruID;
                sinav1.DerslikID = sinav.DerslikID;
                sinav1.OgretimElemaniID = sinav.OgretimElemaniID;

                _context.SaveChanges();

                return RedirectToAction("Sinav", "OgretimElemani");
            }

        return RedirectToAction("NotFound"); // Başka bir sayfaya yönlendirme
        }
        [HttpPost]
        public async Task<IActionResult> SinavSil(int id)
        {
            var sinav = await _context.Sinav.FindAsync(id);

            if (sinav == null)
            {
                return NotFound(); // Eğer veri bulunamazsa 404 Not Found döner
            }

            _context.Sinav.Remove(sinav);
            await _context.SaveChangesAsync();

            return RedirectToAction("Sinav", "OgretimElemani"); // Silme işlemi başarılıysa, Index sayfasına yönlendirir
        }
        [HttpGet]
        public IActionResult NotDuzenle(int id, int ogrenciID, int sinavID)
        {
            //Gelen id'lere göre gerekli kayıtların veri tabanında kontrol edilmesi
            var sinav = _context.Sinav.FirstOrDefault(s => s.SinavID == sinavID);
            var degerlendirme = _context.Degerlendirme.FirstOrDefault(d => d.DegerlendirmeID == Convert.ToInt32(id));


            //Not düzenle sayfasına verilerin çekilmesi
            var dersAdi = (from m in _context.Mufredat
                           join da in _context.DersAcma on m.MufredatID equals da.MufredatID
                           join dh in _context.DersHavuzu on m.DersHavuzuID equals dh.DersHavuzuID
                           where da.DersAcmaID == sinav.DersAcmaID
                           select dh.DersAdi).FirstOrDefault();

            ViewBag.Sinav = sinav;
            ViewBag.DersAdi = dersAdi;
            ViewBag.Degerlendirme = degerlendirme;
            ViewBag.Ogrenci = _context.Ogrenci
                                .Where(o => o.OgrenciID == ogrenciID)
                                .Select(o => new { o.OgrenciID, o.Adi })
                                .FirstOrDefault();


            return View();
        }
        [HttpGet]
        public IActionResult NotEkle(int ogrenciID, int sinavID)
        {

            //gelen id'lere göre sınav bilgisinin çekilmesi
            var sinav = _context.Sinav.FirstOrDefault(s => s.SinavID == sinavID);


            //Gerekli yerlerin çekilmesi ve view'a gönderilmesi
            var dersAdi = (from m in _context.Mufredat
                            join da in _context.DersAcma on m.MufredatID equals da.MufredatID
                            join dh in _context.DersHavuzu on m.DersHavuzuID equals dh.DersHavuzuID
                            where da.DersAcmaID == sinav.DersAcmaID
                            select dh.DersAdi).FirstOrDefault();

            ViewBag.Sinav = sinav;
            ViewBag.DersAdi = dersAdi;

            ViewBag.Ogrenci = _context.Ogrenci
                                .Where(o => o.OgrenciID == ogrenciID)
                                .Select(o => new { o.OgrenciID, o.Adi })
                                .FirstOrDefault();


            return View();
        }
        [HttpPost]
        public IActionResult NotDuzenle(Degerlendirme degerlendirme)
        {
            //Girilen kayıt var mı kontrol et
            var degerlendirme1 = _context.Degerlendirme.FirstOrDefault(d => d.DegerlendirmeID == degerlendirme.DegerlendirmeID);

            //Kayıt varsa
            if(degerlendirme1 != null)
            {
                //Kayıt üzerinde değişiklikler yap
                degerlendirme1.DegerlendirmeID = degerlendirme.DegerlendirmeID;
                degerlendirme1.SinavID = degerlendirme.SinavID;
                degerlendirme1.OgrenciID = degerlendirme.OgrenciID;
                degerlendirme1.SinavNotu = degerlendirme.SinavNotu;

                _context.SaveChanges();

                return RedirectToAction("Degerlendirme", "OgretimElemani", new { id = degerlendirme.SinavID });

            }
            return RedirectToAction("NotFound"); // Eğer nesne bulunamazsa

        }
        [HttpPost]
        public IActionResult NotEkle(Degerlendirme degerlendirme)
        {
            //degerlendirme sınıfına giden veriler viewdaki input name'inden gelmektedir.
            if(degerlendirme != null)
            {
                //Not Ekle
                _context.Degerlendirme.Add(degerlendirme);
                _context.SaveChanges();

                return RedirectToAction("Degerlendirme", "OgretimElemani", new { id = degerlendirme.SinavID});
            }
            return RedirectToAction("NotFound");
        }
        [HttpPost]
        public async Task<IActionResult> DanismanlikSil(int id)
        {
            // Öğrencinin danışmanlık ilişkisi var mı kontrol et
            var danismanlik = await _context.Danismanlik.FindAsync(id);

            if (danismanlik == null)
            {
                // Danışmanlık ilişkisi varsa, bu ilişkiyi kaldır
                return NotFound();
            }
            _context.Danismanlik.Remove(danismanlik);

            // Değişiklikleri kaydet
            await _context.SaveChangesAsync();

            return RedirectToAction("Danismanlik", "OgretimElemani"); // "Danismanlik" action'ına yönlendirme
        }
        [HttpPost]
        public async Task<IActionResult> DanismanlikEkle(Danismanlik model)
        {
            //Öğretim elemanı kendisini öğrenciye danışman olarak ekleyebilir.
            if(model != null)
            { 
                _context.Danismanlik.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Danismanlik", "OgretimElemani");
            }
            return RedirectToAction("NotFound");

        }
        [HttpGet]
        public IActionResult Danismanlik()
        {
            var kullaniciID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "KullaniciID").Value);

            var ogretimElemaniBolumID = (from oe in _context.OgretimElemani
                                         where oe.KullaniciID == kullaniciID
                                         select oe.BolumID).FirstOrDefault();

            //Danışmanlık yapılan öğrencilerin çekilmesi
            //Öğretim elemanına göre sadece kendi danışmanlığını görebilir.
            var query = from ogrenci in _context.Ogrenci
                        join danismanlik in _context.Danismanlik on ogrenci.OgrenciID equals danismanlik.OgrenciID
                        join ogretimElemani in _context.OgretimElemani on danismanlik.OgretimElemaniID equals ogretimElemani.OgretimElemaniID
                        join bolum in _context.Bolum on ogrenci.BolumID equals bolum.BolumID
                        where ogretimElemani.KullaniciID == kullaniciID && ogrenci.BolumID == ogretimElemaniBolumID
                        select new
                        {
                            OgrenciAdi = ogrenci.Adi,
                            OgrenciSoyadi = ogrenci.Soyadi,
                            OgrenciNo = ogrenci.OgrenciNo,
                            BolumAdi = bolum.BolumAdi,
                            DanismanlikID = danismanlik.DanismanlikID
                        };

            var result = query.ToList();
            ViewBag.danismanlik = result;


            //Danışmanlık yapılmayan öğrencileri ekrana çeken sorgu
            var query1 = (from ogrenci in _context.Ogrenci
                          join bolum in _context.Bolum on ogrenci.BolumID equals bolum.BolumID
                          where ogrenci.BolumID == ogretimElemaniBolumID &&
                          !(
                              from danismanlik in _context.Danismanlik
                              select danismanlik.OgrenciID
                          ).Contains(ogrenci.OgrenciID)
                          select new
                          {
                              OgrenciID = ogrenci.OgrenciID,
                              OgrenciNo = ogrenci.OgrenciNo,
                              OgrenciAdi = ogrenci.Adi,
                              OgrenciSoyadi = ogrenci.Soyadi,
                              BolumAdi = bolum.BolumAdi
                          }).ToList();
            var ogretimElemani1 = _context.OgretimElemani.FirstOrDefault(x => x.KullaniciID == kullaniciID);

            ViewBag.resultData = query1;
            ViewBag.OgretimElemaniID = ogretimElemani1.OgretimElemaniID;



            return View();
        }
        [HttpGet]
        public IActionResult Ders()
        {
            var kullaniciID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "KullaniciID").Value);

            var sonDonem = _context.AkademikDonem.OrderByDescending(d => d.AkademikDonemID).FirstOrDefault();


            //Verilen derslerin çekilmesi
            var query = from dersAlma in _context.DersAlma
                        join dersAcma in _context.DersAcma on dersAlma.DersAcmaID equals dersAcma.DersAcmaID
                        join akademikDonem in _context.AkademikDonem on dersAcma.AkademikDonemID equals akademikDonem.AkademikDonemID
                        join akademikYil in _context.AkademikYil on dersAcma.AkademikYilID equals akademikYil.AkademikYilID
                        join ogretimElemani in _context.OgretimElemani on dersAcma.OgretimElemaniID equals ogretimElemani.OgretimElemaniID
                        join mufredat in _context.Mufredat on dersAcma.MufredatID equals mufredat.MufredatID
                        join dersHavuzu in _context.DersHavuzu on mufredat.DersHavuzuID equals dersHavuzu.DersHavuzuID
                        join dersTuru in _context.DersTuru on dersHavuzu.DersTuruID equals dersTuru.DersTuruID
                        where ogretimElemani.KullaniciID == kullaniciID && akademikDonem.AkademikDonemID == sonDonem.AkademikDonemID
                        group new
                        {
                            dersAcma.DersAcmaID,
                            dersHavuzu.DersKodu,
                            dersHavuzu.DersAdi,
                            dersTuru.DersTuruAdi,
                            mufredat.DersDonemi,
                            dersHavuzu.Teorik,
                            dersHavuzu.Uygulama,
                            dersHavuzu.Kredi,
                            dersHavuzu.ECTS
                        } by new
                        {
                            dersAcma.DersAcmaID,
                            dersHavuzu.DersKodu,
                            dersHavuzu.DersAdi,
                            dersTuru.DersTuruAdi,
                            mufredat.DersDonemi,
                            dersHavuzu.Teorik,
                            dersHavuzu.Uygulama,
                            dersHavuzu.Kredi,
                            dersHavuzu.ECTS
                        } into grouped
                        select new
                        {
                            DersAcmaID = grouped.Key.DersAcmaID,
                            DersKodu = grouped.Key.DersKodu,
                            DersAdi = grouped.Key.DersAdi,
                            DersTuruAdi = grouped.Key.DersTuruAdi,
                            DersDonemi = grouped.Key.DersDonemi,
                            Teorik = grouped.Key.Teorik,
                            Uygulama = grouped.Key.Uygulama,
                            Kredi = grouped.Key.Kredi,
                            ECTS = grouped.Key.ECTS
                        };

            var result = query.Select(item => new {
                DersAcmaID = item.DersAcmaID,
                DersKodu = item.DersKodu,
                DersAdi = item.DersAdi,
                DersTuruAdi = item.DersTuruAdi,
                DersDonemi = item.DersDonemi,
                Teorik = item.Teorik,
                Uygulama = item.Uygulama,
                Kredi = item.Kredi,
                ECTS = item.ECTS
            }).ToList();


            ViewBag.resultData = result;

            return View();
        }
        [HttpGet]
        public IActionResult DersOnay(int id)
        {
            //Ders onay sayfasında dersi seçen öğrencileri görme
            var query = from dersAlma in _context.DersAlma
                        join ogrenci in _context.Ogrenci on dersAlma.OgrenciID equals ogrenci.OgrenciID
                        join dersAcma in _context.DersAcma on dersAlma.DersAcmaID equals dersAcma.DersAcmaID
                        join dersDurum in _context.DersDurum on dersAlma.DersDurumID equals dersDurum.DersDurumID
                        where dersAlma.DersAcmaID == id
                        select new
                        {
                            OgrenciNo = ogrenci.OgrenciNo,
                            OgrenciAdi = ogrenci.Adi,
                            OgrenciSoyadi = ogrenci.Soyadi,
                            DersDurum = dersDurum.DersDurumAdi,
                            DersDurumID = dersDurum.DersDurumID,
                            OgrenciID = ogrenci.OgrenciID,
                            DersAcmaID = dersAcma.DersAcmaID,
                            DersAlmaID = dersAlma.DersAlmaID,
                        };
            var result = query.ToList();

            ViewBag.resultData = result;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DersOnay(DersAlma dersAlma)
        {
            //Seçilen dersleri onaylama ve dersAlma tablosuna veri ekleme
            var drsAlma = _context.DersAlma.FirstOrDefault(x => x.DersAlmaID == dersAlma.DersAlmaID);

            if(dersAlma != null)
            {
                drsAlma.DersAlmaID = dersAlma.DersAlmaID;
                drsAlma.DersAcmaID = dersAlma.DersAcmaID;
                drsAlma.OgrenciID = dersAlma.OgrenciID;
                drsAlma.DersDurumID = dersAlma.DersDurumID;
                _context.SaveChanges();

                return RedirectToAction("DersOnay", "OgretimElemani", new { id = dersAlma.DersAcmaID });
            }

            return RedirectToAction("NotFound");
        }
        [HttpPost]
        public async Task<IActionResult> DersRet(DersAlma dersAlma)
        {
            //Dersi reddetme
            var drsAlma = _context.DersAlma.FirstOrDefault(x => x.DersAlmaID == dersAlma.DersAlmaID);

            if (dersAlma != null)
            {
                drsAlma.DersAlmaID = dersAlma.DersAlmaID;
                drsAlma.DersAcmaID = dersAlma.DersAcmaID;
                drsAlma.OgrenciID = dersAlma.OgrenciID;
                drsAlma.DersDurumID = dersAlma.DersDurumID;
                _context.SaveChanges();

                return RedirectToAction("DersOnay", "OgretimElemani", new { id = dersAlma.DersAcmaID });
            }

            return RedirectToAction("NotFound");
        }















        // ADMIN İŞLEMLERİ viewları "/Admin" klasöründe tutulmaktadır. //
        [HttpGet]
        public IActionResult BolumEkle()
        {
            var programTuruListesi = _context.ProgramTuru.ToList();
            ViewBag.programTurleri = programTuruListesi;
            var ogretimTuruListesi = _context.OgretimTuru.ToList();
            ViewBag.ogretimTurleri = ogretimTuruListesi;
            var ogrenimDililistesi = _context.Dil.ToList();
            ViewBag.ogrenimDilleri = ogrenimDililistesi;

            return View("~/Views/OgretimElemani/Admin/BolumEkle.cshtml");
        } //Bolum Ekleme GET
        [HttpPost]
        public IActionResult BolumEkle(Bolum bolum)
        {
            if(bolum != null)
            {
                _context.Bolum.Add(bolum);
                _context.SaveChanges();
                TempData["Message"] = "Bölüm Eklendi";
                return RedirectToAction("BolumEkle", "OgretimElemani"); 
            }
            return RedirectToAction("NotFound");
        } //Bolum Ekleme POST
        [HttpGet]
        public IActionResult OgrenciEkle()
        {
            var bolumListesi = _context.Bolum.ToList();
            ViewBag.bolumListesi = bolumListesi;
            var ogrenciDurumListesi = _context.OgrenciDurum.ToList();
            ViewBag.ogrenciDurumListesi = ogrenciDurumListesi;
            var cinsiyetListesi = _context.Cinsiyet.ToList();
            ViewBag.cinsiyetListesi = cinsiyetListesi;
            var kullaniciListesi = _context.Kullanici.ToList();
            ViewBag.kullaniciListesi = kullaniciListesi;

            return View("~/Views/OgretimElemani/Admin/OgrenciEkle.cshtml");
        } //Ogrenci Ekle GET
        [HttpPost]
        public IActionResult OgrenciEkle(Ogrenci ogrenci)
        {
            if(ogrenci != null)
            {
                _context.Ogrenci.Add(ogrenci);
                _context.SaveChanges();
                TempData["Message"] = "Bölüm Eklendi";
                return RedirectToAction("OgrenciEkle", "OgretimElemani");
            }
            return RedirectToAction("NotFound");
        } //Ogrenci Ekle POST
        [HttpGet]
        public IActionResult KullaniciEkle()
        {
            var kullaniciTurleri = _context.KullaniciTuru.ToList();
            ViewBag.kullaniciTurleri = kullaniciTurleri;

            return View("~/Views/OgretimElemani/Admin/KullaniciEkle.cshtml");
        } //Kullanici EKle GET
        [HttpPost]
        public IActionResult KullaniciEkle(Kullanici kullanici)
        {
            if (kullanici != null)
            {
                _context.Kullanici.Add(kullanici);
                _context.SaveChanges();
                TempData["Message"] = "Kullanıcı Eklendi";
                return RedirectToAction("KullaniciEkle", "OgretimElemani");
            }
            return RedirectToAction("NotFound");
        } //Kullanici Ekle POST
        [HttpGet]
        public IActionResult OgretimElemaniEkle()
        {
            var bolumListesi = _context.Bolum.ToList();
            ViewBag.bolumListesi = bolumListesi;
            var unvanListesi = _context.Unvan.ToList();
            ViewBag.unvanListesi = unvanListesi;
            var cinsiyetListesi = _context.Cinsiyet.ToList();
            ViewBag.cinsiyetListesi = cinsiyetListesi;
            var kullaniciListesi = _context.Kullanici.ToList();
            ViewBag.kullaniciListesi = kullaniciListesi;


            return View("~/Views/OgretimElemani/Admin/OgretimElemaniEkle.cshtml");
        } //Ogretim Elemani Ekle GET
        [HttpPost]
        public IActionResult OgretimElemaniEkle(OgretimElemani ogretimElemani)
        {
            if(ogretimElemani != null)
            {
                _context.OgretimElemani.Add(ogretimElemani);
                _context.SaveChanges();
                return RedirectToAction("OgretimElemaniEkle", "OgretimElemani");
            }
            return RedirectToAction("NotFound");
        } //Ogretim Elemani Ekle POST
        [HttpGet]
        public IActionResult AkademikDonemEkle()
        {
            return View("~/Views/OgretimElemani/Admin/AkademikDonemEkle.cshtml");
        } //Akademik Donem Ekle GET
        [HttpPost]
        public IActionResult AkademikDonemEkle(AkademikDonem akademikDonem)
        {
            if (akademikDonem != null)
            {
                _context.AkademikDonem.Add(akademikDonem);
                _context.SaveChanges();
                return RedirectToAction("OgretimElemaniEkle", "OgretimElemani");
            }
            return RedirectToAction("NotFound");
        } // Akademik Donem Ekle POST
        [HttpGet]
        public IActionResult DersEkle()
        {
            var dersSeviyesiListesi = _context.DersSeviyesi.ToList();
            ViewBag.dersSeviyesiListesi = dersSeviyesiListesi;
            var dilListesi = _context.Dil.ToList();
            ViewBag.dilListesi = dilListesi;
            var dersTuru = _context.DersTuru.ToList();
            ViewBag.dersTuru = dersTuru;

            return View("~/Views/OgretimElemani/Admin/DersEkle.cshtml");
        } //DersHavuzu Ekle GET
        [HttpPost]
        public IActionResult DersEkle(DersHavuzu dersHavuzu)
        {
            if (dersHavuzu != null)
            {
                _context.DersHavuzu.Add(dersHavuzu);
                _context.SaveChanges();
                return RedirectToAction("DersEkle", "OgretimElemani");
            }
            return RedirectToAction("NotFound");
        } //DersHavuzu Ekle POST
        [HttpGet]
        public IActionResult MufredatEkle()
        {
            var bolumListesi = _context.Bolum.ToList();
            ViewBag.bolumListesi = bolumListesi;
            var dersListesi = _context.DersHavuzu.ToList();
            ViewBag.dersListesi = dersListesi;

            var sonDonem = _context.AkademikDonem.OrderByDescending(d => d.AkademikDonemID).FirstOrDefault();

            ViewBag.sonDonem = sonDonem;

            return View("~/Views/OgretimElemani/Admin/MufredatEkle.cshtml");
        } //Mufredat Ekle GET
        [HttpPost]
        public IActionResult MufredatEkle(Mufredat mufredat)
        {
            if (mufredat != null)
            {
                _context.Mufredat.Add(mufredat);
                _context.SaveChanges();
                return RedirectToAction("MufredatEkle", "OgretimElemani");
            }
            return RedirectToAction("NotFound");
        } //Mufredat Ekle POST
        [HttpGet]
        public IActionResult DersAcmaEkle()
        {
            var sonDonem = _context.AkademikDonem.OrderByDescending(d => d.AkademikDonemID).FirstOrDefault();
            ViewBag.sonDonem = sonDonem;

            var result = _context.Mufredat
                        .Join(_context.DersHavuzu,
                              mufredat => mufredat.DersHavuzuID,
                              dersHavuzu => dersHavuzu.DersHavuzuID,
                              (mufredat, dersHavuzu) => new { mufredat, dersHavuzu })
                        .GroupBy(
                            x => new { x.mufredat.MufredatID, x.dersHavuzu.DersAdi },
                            (key, group) => new
                            {
                                MufredatID = key.MufredatID,
                                DersAdi = key.DersAdi
                            }
                        )
                        .ToList();

            ViewBag.mufredatListesi = result;

            var ogretimElemanlari = _context.OgretimElemani
                                .Select(oe => new
                                {
                                    OgretimElemaniID = oe.OgretimElemaniID,
                                    Adi = oe.Adi,
                                    Soyadi = oe.Soyadi
                                })
                                .ToList();
            ViewBag.ogretimElemani = ogretimElemanlari;

            return View("~/Views/OgretimElemani/Admin/DersAcmaEkle.cshtml");
        } //DersAcma Ekle GET
        [HttpPost]
        public IActionResult DersAcmaEkle(DersAcma dersAcma)
        {
            if (dersAcma != null)
            {
                _context.DersAcma.Add(dersAcma);
                _context.SaveChanges();
                return RedirectToAction("DersAcmaEkle", "OgretimElemani");
            }
            return RedirectToAction("NotFound");
        } //DersAcma Ekle POST
        [HttpGet]
        public IActionResult DersProgramiEkle()
        {
            var sonDonem = _context.AkademikDonem.OrderByDescending(d => d.AkademikDonemID).FirstOrDefault();

            var query = from dersAcma in _context.DersAcma
                        join mufredat in _context.Mufredat on dersAcma.MufredatID equals mufredat.MufredatID
                        join dersHavuzu in _context.DersHavuzu on mufredat.DersHavuzuID equals dersHavuzu.DersHavuzuID
                        where mufredat.AkademikDonemID == sonDonem.AkademikDonemID
                        select new
                        {
                            DersAcmaID = dersAcma.DersAcmaID,
                            DersKodu = dersHavuzu.DersKodu,
                            DersAdi = dersHavuzu.DersAdi
                        };
            var result = query.ToList();
            ViewBag.Dersler = result;

            ViewBag.DerslikListesi = _context.Derslik.ToList();
            ViewBag.gunListesi = _context.Gun.ToList();

            return View("~/Views/OgretimElemani/Admin/DersProgramiEkle.cshtml");
        } //DersProgrami Ekle GET
        [HttpPost]
        public IActionResult DersProgramiEkle(DersProgrami dersProgrami)
        {
            var ders = _context.DersProgrami.FirstOrDefault(x => x.DersAcmaID == dersProgrami.DersAcmaID);

            if (ders == null)
            {
                if(dersProgrami != null)
                {
                    _context.DersProgrami.Add(dersProgrami);
                    _context.SaveChanges();
                    return RedirectToAction("DersProgramiEkle", "OgretimElemani");
                }
                return RedirectToAction("NotFound");
            }
            TempData["Message"] = "Bu ders için zaten ders programı kaydı mevcut!";
            return RedirectToAction("DersProgramiEkle", "OgretimElemani");
        } //DersProgrami Ekle POST
        [HttpGet]
        public IActionResult DersProgramiSaatEkle()
        {
            var sonDonem = _context.AkademikDonem.OrderByDescending(d => d.AkademikDonemID).FirstOrDefault();

            var query = from dersProgrami in _context.DersProgrami
                                    join dersAcma in _context.DersAcma on dersProgrami.DersAcmaID equals dersAcma.DersAcmaID
                                    join mufredat in _context.Mufredat on dersAcma.MufredatID equals mufredat.MufredatID
                                    join dersHavuzu in _context.DersHavuzu on mufredat.DersHavuzuID equals dersHavuzu.DersHavuzuID
                                    join derslik in _context.Derslik on dersProgrami.DerslikID equals derslik.DerslikID
                                    join gun in _context.Gun on dersProgrami.GunID equals gun.GunID
                                    where mufredat.AkademikDonemID == sonDonem.AkademikDonemID
                                    group new { dersHavuzu.DersAdi, dersHavuzu.DersKodu, dersAcma.DersAcmaID, gun.GunAdi }
                                    by new { dersHavuzu.DersAdi, dersHavuzu.DersKodu, dersAcma.DersAcmaID, gun.GunAdi }
            into grouped
                                    select new
                                    {
                                        DersAdi = grouped.Key.DersAdi,
                                        DersKodu = grouped.Key.DersKodu,
                                        DersAcmaID = grouped.Key.DersAcmaID,
                                        GunAdi = grouped.Key.GunAdi
                                    };

            ViewBag.Dersler = query.ToList();
            ViewBag.Saatler = _context.DersSaati.ToList();

            return View("~/Views/OgretimElemani/Admin/DersProgramiSaatEkle.cshtml");
        } //DersProgramiSaatEkle GET
        [HttpPost]
        public IActionResult DersProgramiSaatEkle(DersAcmaDersSaati dersAcmaDersSaati)
        {
            var dersSaati = _context.DersAcmaDersSaati.FirstOrDefault(x => x.DersAcmaID == dersAcmaDersSaati.DersAcmaID && x.DersSaatiID == dersAcmaDersSaati.DersSaatiID);
            if(dersSaati == null)
            {
                if(dersAcmaDersSaati != null)
                {
                    _context.DersAcmaDersSaati.Add(dersAcmaDersSaati);
                    _context.SaveChanges();
                    return RedirectToAction("DersProgramiSaatEkle", "OgretimElemani");
                }
                return RedirectToAction("NotFound");
            }
            else
            {
                TempData["Message"] = "Bu saatte zaten bu ders mevcut";
                return RedirectToAction("DersProgramiSaatEkle", "OgretimElemani");
            }
        } //DersProgramiSaatEkle POST



        public class DonemModel
        {
            //ajax ile select'den dönemin çekilmesi
            public string SelectedDonem { get; set; }
        }

    }
}
