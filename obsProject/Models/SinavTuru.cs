namespace obsProject.Models
{
    public class SinavTuru
    {
        public int SinavTuruID { get; set; }
        public string SinavTuruAdi { get; set; }
        public ICollection<Sinav> Sinavlar { get; set; }
    }
}
