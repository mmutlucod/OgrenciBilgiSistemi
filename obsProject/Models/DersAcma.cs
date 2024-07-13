using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace obsProject.Models
{
    public class DersAcma
    {
        public int DersAcmaID { get; set; }
        public int AkademikYilID {  get; set; }
        public int AkademikDonemID {  get; set; }
        public int MufredatID {  get; set; }
        public int Kontenjan { get; set; }
        public int OgretimElemaniID { get; set; }
        public AkademikDonem AkademikDonem {  get; set; }
        public AkademikYil AkademikYil {  get; set; }
        public Mufredat Mufredat {  get; set; }
        public OgretimElemani OgretimElemani {  get; set; }
        public ICollection<DersAlma> DersAlmalar { get; set;}
        public ICollection<Sinav> Sinavlar { get; set;}
        public ICollection<DersProgrami> DersProgramlari { get; set; }
        public ICollection<DersAcmaDersSaati> DersAcmaDersSaatleri { get; set; }

    }
}
