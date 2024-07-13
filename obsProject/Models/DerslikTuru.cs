using System.ComponentModel.DataAnnotations;

namespace obsProject.Models
{
    public class DerslikTuru
    {
        public int DerslikTuruID { get; set; }
        public string DerslikTuruAdi { get; set; }
        public ICollection<Derslik> Derslikler {  get; set; }
    }
}
