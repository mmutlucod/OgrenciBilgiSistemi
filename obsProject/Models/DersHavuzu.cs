using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace obsProject.Models
{
    public class DersHavuzu
    {
        public int DersHavuzuID { get; set; }
        public string DersKodu { get; set; }
        public string DersAdi { get; set; }
        public int DilID { get; set; }
        public int DersSeviyesiID { get; set; }
        public int DersTuruID { get; set; }
        public double Teorik { get; set; }
        public double Uygulama { get; set; }
        public double Kredi { get; set; }
        public int ECTS { get; set; }
        public Dil Dil { get; set; }
        public DersSeviyesi DersSeviyesi { get; set; }
        public DersTuru DersTuru { get; set; }
        public ICollection<Mufredat> Mufredatlar { get; set; }

    }
}
