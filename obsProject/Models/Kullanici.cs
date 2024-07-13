using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace obsProject.Models
{
    public class Kullanici
    {
        public int KullaniciID { get; set; }
        public string KullaniciAdi { get; set; }
        public string Parola { get; set; }
        public int KullaniciTuruID { get; set; }
        public KullaniciTuru KullaniciTuru { get; set; }
        public ICollection<Ogrenci> Ogrenciler { get; set; }
        public ICollection<OgretimElemani> OgretimElemanlari { get; set; }

    }
}
