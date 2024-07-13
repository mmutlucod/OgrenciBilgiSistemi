using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace obsProject.Models
{
    public class Mufredat
    {
        public int MufredatID { get; set; }
        public int BolumID { get; set; }
        public int DersHavuzuID { get; set; }
        public int AkademikYilID { get; set; }
        public int AkademikDonemID {  get; set; }
        public int DersDonemi { get; set; }
        public Bolum Bolum {  get; set; }
        public DersHavuzu DersHavuzu {  get; set; }
        public AkademikYil AkademikYil {  get; set; }
        public AkademikDonem AkademikDonem {  get; set; }
        public ICollection<DersAcma> DersAcmalar { get; set; }

    }
}
