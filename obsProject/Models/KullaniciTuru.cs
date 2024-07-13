using System.ComponentModel.DataAnnotations;

namespace obsProject.Models
{
    public class KullaniciTuru
    {
       
        public int KullaniciTuruID { get; set; }
        public string KullaniciTuruAdi { get; set; }
        public ICollection<Kullanici> Kullanicilar {  get; set; }

    }
}
