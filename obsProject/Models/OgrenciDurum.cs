using System.ComponentModel.DataAnnotations;

namespace obsProject.Models
{
    public class OgrenciDurum
    {
        public int OgrenciDurumID { get; set; }
        public string OgrenciDurumAdi { get; set; }
        public ICollection<Ogrenci> Ogrenciler {  get; set; }
    }
}
