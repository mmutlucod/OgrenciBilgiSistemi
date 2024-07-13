using System.ComponentModel.DataAnnotations;

namespace obsProject.Models
{
    public class Cinsiyet
    {
        public int CinsiyetID { get; set; }
        public string CinsiyetAdi { get; set; }
        public ICollection<Ogrenci> Ogrenciler { get; set; }
        public ICollection<OgretimElemani> OgretimElemanlari { get; set; }

    }
}
