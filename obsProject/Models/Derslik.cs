using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace obsProject.Models
{
    public class Derslik
    {
        public int DerslikID { get; set; }
        public string DerslikAdi { get; set; }
        public int DerslikTuruID {  get; set; }
        public int Kapasite { get; set; }
        public DerslikTuru DerslikTuru { get; set; }
        public ICollection<Sinav> Sinavlar { get; set; }
        public ICollection<DersProgrami> DersProgramlari { get; set; }


    }
}
