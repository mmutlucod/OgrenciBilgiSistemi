namespace obsProject.Models
{
    public class DersAcmaDersSaati
    {
        public int DersAcmaDersSaatiID { get; set; }
        public int DersAcmaID { get; set; }
        public DersAcma DersAcma { get; set; }

        public int DersSaatiID { get; set; }
        public DersSaati DersSaati { get; set; }
    }
}
