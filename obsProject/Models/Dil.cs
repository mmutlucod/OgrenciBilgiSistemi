using System.ComponentModel.DataAnnotations;

namespace obsProject.Models
{
    public class Dil
    {
        public int DilID { get; set; }
        public string DilAdi { get; set; }
        public ICollection<Bolum> Bolumler { get; set; }
        public ICollection<DersHavuzu> DersHavuzlari { get;}
    }
}
