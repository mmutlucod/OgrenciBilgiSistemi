using System.ComponentModel.DataAnnotations;

namespace obsProject.Models
{
    public class Unvan
    {
        public int UnvanID { get; set; }
        public string UnvanAdi { get; set; }
        public ICollection<OgretimElemani> OgretimElemanlari { get; set; }


    }
}
