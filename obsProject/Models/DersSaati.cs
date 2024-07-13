namespace obsProject.Models
{
    public class DersSaati
    {
        public int DersSaatiID {  get; set; }
        public string DersSaatiAdi { get; set; }
        public ICollection<DersAcmaDersSaati> DersAcmaDersSaatleri { get; set; }

    }
}
