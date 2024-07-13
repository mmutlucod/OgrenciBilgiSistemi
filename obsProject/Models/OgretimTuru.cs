using System.ComponentModel.DataAnnotations;

namespace obsProject.Models
{
    public class OgretimTuru
    {
        public int OgretimTuruID { get; set; }
        public string OgretimTuruAdi { get; set; }
        public ICollection<Bolum> Bolumler {  get; set; }
    }
}
