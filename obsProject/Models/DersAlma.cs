namespace obsProject.Models
{
    public class DersAlma
    {
        public int DersAlmaID { get; set; }
        public int DersAcmaID { get; set; }
        public int OgrenciID { get; set; }
        public int DersDurumID { get; set; }
        public DersAcma DersAcma { get; set; }
        public Ogrenci Ogrenci { get; set; }
        public DersDurum DersDurum { get; set; }
    }
}
