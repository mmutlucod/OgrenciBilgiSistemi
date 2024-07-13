namespace obsProject.Models
{
    public class AkademikYil
    {
        public int AkademikYilID { get; set; }
        public string AkademikYilAdi { get; set; }
        public ICollection<Mufredat> Mufredatlar { get; set; }
        public ICollection<DersAcma> DersAcmalar { get; set;}

    }
}
