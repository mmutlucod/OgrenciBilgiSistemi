using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace obsProject.Models
{
    public class Ogrenci
    {
        public int OgrenciID { get; set; }
        public int BolumID { get; set; }
        public string OgrenciNo { get; set; }
        public int OgrenciDurumID { get; set; }
        public string KayitTarihi { get; set; }
        public string AyrilmaTarihi { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string TCKimlikNo { get; set; }
        public int CinsiyetID { get; set; }
        public string DogumTarihi { get; set; }
        public int KullaniciID { get; set; }
        public Bolum Bolum { get; set; }
        public OgrenciDurum OgrenciDurum { get; set; }
        public Cinsiyet Cinsiyet { get; set; }
        public Kullanici Kullanici { get; set; }
        public ICollection<DersAlma> DersAlmalar { get; set; }
        public ICollection<Degerlendirme> Degerlendirmeler { get; }
        public ICollection<Danismanlik> Danismanliklar { get; set; }


    }
}
