using System.ComponentModel.DataAnnotations;

namespace obsProject.Models
{
    public class ProgramTuru
    {
        public int ProgramTuruID { get; set; }
        public string ProgramTuruAdi { get; set; }
        public ICollection<Bolum> Bolumler { get; set; }
    }
}
