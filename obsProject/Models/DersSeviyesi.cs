using System.ComponentModel.DataAnnotations;

namespace obsProject.Models
{
    public class DersSeviyesi
    {
        public int DersSeviyesiID { get; set; }
        public string DersSeviyesiAdi { get; set; }
        public ICollection<DersHavuzu> DersHavuzlari { get; }

    }
}
