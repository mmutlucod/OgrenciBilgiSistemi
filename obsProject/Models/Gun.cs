using System.ComponentModel.DataAnnotations;

namespace obsProject.Models
{
    public class Gun
    {
        public int GunID { get; set; }
        public string GunAdi { get; set; }
        public ICollection<DersProgrami> DersProgramlari { get; set; }


    }
}
