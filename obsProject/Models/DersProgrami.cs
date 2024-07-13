using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace obsProject.Models
{
    public class DersProgrami
    {
        public int DersProgramiID { get; set; }
        public int DersAcmaID { get; set; }
        public int DerslikID { get; set; }
        public int GunID { get; set; }
        public DersAcma DersAcma { get; set; }
        public Derslik Derslik { get; set; }
        public Gun Gun { get; set; }

    }
}
