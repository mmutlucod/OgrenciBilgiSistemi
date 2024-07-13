using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace obsProject.Models
{
    public class OgretimElemani
    {
        public int OgretimElemaniID { get; set; }
        public int BolumID { get; set; }
        public string KurumSicilNo { get; set; }
        public int UnvanID { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string TCKimlikNo { get; set; }
        public int CinsiyetID { get; set; }
        public string DogumTarihi { get; set; }
        public int KullaniciID { get; set; }
        public Bolum Bolum {  get; set; }
        public Unvan Unvan {  get; set; }
        public Cinsiyet Cinsiyet { get; set; }
        public Kullanici Kullanici { get; set; }
        public ICollection<DersAcma> DersAcmalar { get; set; }
        public ICollection<Sinav> Sinavlar { get; set; }
        public ICollection<Danismanlik> Danismanliklar { get; set; }


    }
}
