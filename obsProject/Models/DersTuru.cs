using System.ComponentModel.DataAnnotations;

namespace obsProject.Models
{
    public class DersTuru
    { 
        public int DersTuruID { get; set; }
        public string DersTuruAdi { get; set; }
        public ICollection<DersHavuzu> DersHavuzlari { get; }

    }
}
