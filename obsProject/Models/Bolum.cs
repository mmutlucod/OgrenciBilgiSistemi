using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace obsProject.Models
{
    public class Bolum
    {
        public int BolumID { get; set; }
        public string BolumAdi { get; set; }
        public int ProgramTuruID { get; set; }
        public int OgretimTuruID { get; set; }
        public int DilID { get; set; }
        public string WebAdresi { get; set; }
        public ProgramTuru ProgramTuru { get; set; }
        public OgretimTuru OgretimTuru { get; set; }
        public Dil Dil { get; set; }



        public ICollection<Ogrenci> Ogrenciler { get; set; }
        public ICollection<OgretimElemani> OgretimElemanlari { get; set; }
        public ICollection<Mufredat> Mufredatlar { get; set; }
    }
}
