namespace obsProject.Models
{
    public class DersDurum
    {
        public int DersDurumID { get; set; }
        public string DersDurumAdi { get; set; }
        public ICollection<DersAlma> DersAlmalar { get; set; }
    }
}
