namespace obsProject.Models
{
    public class Sinav
    {
        public int SinavID { get; set; }
        public int DersAcmaID { get; set; }
        public int SinavTuruID { get; set; }
        public double EtkiOrani { get; set; }
        public string SinavTarihi { get; set; }
        public string SinavSaati { get; set; }
        public int DerslikID { get; set; }
        public int OgretimElemaniID { get; set; }
        public DersAcma DersAcma { get; set; }
        public SinavTuru SinavTuru { get; set; }
        public Derslik Derslik {  get; set; }
        public OgretimElemani OgretimElemani { get; set; }
        public ICollection<Degerlendirme> Degerlendirmeler { get;}
    }
}
