namespace obsProject.Models
{
    public class AkademikDonem
    {
        public int AkademikDonemID {  get; set; }
        public string AkademikDonemAdi {  get; set; }
        public ICollection<Mufredat> Mufredatlar { get; set; }
        public ICollection<DersAcma> DersAcmalar { get; set; }

    }
}
