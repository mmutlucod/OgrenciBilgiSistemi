using System.Reflection;

namespace obsProject.Models
{
    public class Danismanlik
    {
        public int DanismanlikID { get; set; }
        public int OgretimElemaniID { get; set; }
        public int OgrenciID { get; set; }
        public OgretimElemani OgretimElemani { get; set; }
        public Ogrenci Ogrenci { get; set; }
    }
}
