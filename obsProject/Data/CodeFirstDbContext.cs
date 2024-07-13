using Microsoft.EntityFrameworkCore;
using obsProject.Models;


namespace obsProject.Data
{
    public class CodeFirstDbContext : DbContext
    {
        public CodeFirstDbContext(DbContextOptions<CodeFirstDbContext> options) : base(options)
        { 

        }
        public DbSet<AkademikDonem> AkademikDonem { get; set; }
        public DbSet<AkademikYil> AkademikYil { get; set; }
        public DbSet<Bolum> Bolum { get; set; }
        public DbSet<Cinsiyet> Cinsiyet { get; set; }
        public DbSet<Danismanlik> Danismanlik { get; set; }
        public DbSet<Degerlendirme> Degerlendirme { get; set; }
        public DbSet<DersAcma> DersAcma { get; set; }
        public DbSet<DersAlma> DersAlma { get; set; }
        public DbSet<DersDurum> DersDurum { get; set; }
        public DbSet<DersHavuzu> DersHavuzu { get; set; }
        public DbSet<Derslik> Derslik { get; set; }
        public DbSet<DerslikTuru> DerslikTuru { get; set; }
        public DbSet<DersProgrami> DersProgrami { get; set; }
        public DbSet<DersSeviyesi> DersSeviyesi { get; set; }
        public DbSet<DersTuru> DersTuru { get; set; }
        public DbSet<Dil> Dil { get; set; }
        public DbSet<Gun> Gun { get; set; }
        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<KullaniciTuru> KullaniciTuru { get; set; }
        public DbSet<Mufredat> Mufredat { get; set; }
        public DbSet<Ogrenci> Ogrenci { get; set; }
        public DbSet<OgrenciDurum> OgrenciDurum { get; set; }
        public DbSet<OgretimElemani> OgretimElemani { get; set; }
        public DbSet<OgretimTuru> OgretimTuru { get; set; }
        public DbSet<ProgramTuru> ProgramTuru { get; set; }
        public DbSet<Sinav> Sinav { get; set; }
        public DbSet<SinavTuru> SinavTuru { get; set; }
        public DbSet<Unvan> Unvan { get; set; }
        public DbSet<DersAcmaDersSaati> DersAcmaDersSaati { get; set; }
        public DbSet<DersSaati> DersSaati { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AkademikDonem>().ToTable("AkademikDonem");
            modelBuilder.Entity<AkademikDonem>().HasKey(a => a.AkademikDonemID);

            modelBuilder.Entity<AkademikYil>().ToTable("AkademikYil");
            modelBuilder.Entity<AkademikYil>().HasKey(a => a.AkademikYilID);

            modelBuilder.Entity<Cinsiyet>().ToTable("Cinsiyet");
            modelBuilder.Entity<Cinsiyet>().HasKey(a => a.CinsiyetID);

            modelBuilder.Entity<DersDurum>().ToTable("DersDurum");
            modelBuilder.Entity<DersDurum>().HasKey(a => a.DersDurumID);

            modelBuilder.Entity<DerslikTuru>().ToTable("DerslikTuru");
            modelBuilder.Entity<DerslikTuru>().HasKey(a => a.DerslikTuruID);

            modelBuilder.Entity<DersSeviyesi>().ToTable("DersSeviyesi");
            modelBuilder.Entity<DersSeviyesi>().HasKey(a => a.DersSeviyesiID);

            modelBuilder.Entity<DersTuru>().ToTable("DersTuru");
            modelBuilder.Entity<DersTuru>().HasKey(a => a.DersTuruID);

            modelBuilder.Entity<Dil>().ToTable("Dil");
            modelBuilder.Entity<Dil>().HasKey(a => a.DilID);

            modelBuilder.Entity<Gun>().ToTable("Gun");
            modelBuilder.Entity<Gun>().HasKey(a => a.GunID);

            modelBuilder.Entity<KullaniciTuru>().ToTable("KullaniciTuru");
            modelBuilder.Entity<KullaniciTuru>().HasKey(a => a.KullaniciTuruID);

            modelBuilder.Entity<OgrenciDurum>().ToTable("OgrenciDurum");
            modelBuilder.Entity<OgrenciDurum>().HasKey(a => a.OgrenciDurumID);

            modelBuilder.Entity<OgretimTuru>().ToTable("OgretimTuru");
            modelBuilder.Entity<OgretimTuru>().HasKey(a => a.OgretimTuruID);

            modelBuilder.Entity<ProgramTuru>().ToTable("ProgramTuru");
            modelBuilder.Entity<ProgramTuru>().HasKey(a => a.ProgramTuruID);

            modelBuilder.Entity<SinavTuru>().ToTable("SinavTuru");
            modelBuilder.Entity<SinavTuru>().HasKey(a => a.SinavTuruID);

            modelBuilder.Entity<Unvan>().ToTable("Unvan");
            modelBuilder.Entity<Unvan>().HasKey(a => a.UnvanID);

            modelBuilder.Entity<DersSaati>().ToTable("DersSaati");
            modelBuilder.Entity<DersSaati>().HasKey(a => a.DersSaatiID);

            modelBuilder.Entity<Kullanici>().ToTable("Kullanici");
            modelBuilder.Entity<Kullanici>().HasKey(a => a.KullaniciID);
            modelBuilder.Entity<Kullanici>().HasOne(a => a.KullaniciTuru).WithMany(a => a.Kullanicilar).HasForeignKey(a => a.KullaniciTuruID);

            modelBuilder.Entity<Bolum>().ToTable("Bolum");
            modelBuilder.Entity<Bolum>().HasKey(a => a.BolumID);
            modelBuilder.Entity<Bolum>().HasOne(a => a.ProgramTuru).WithMany(a => a.Bolumler).HasForeignKey(a => a.ProgramTuruID);
            modelBuilder.Entity<Bolum>().HasOne(a => a.OgretimTuru).WithMany(a => a.Bolumler).HasForeignKey(a => a.OgretimTuruID);
            modelBuilder.Entity<Bolum>().HasOne(a => a.Dil).WithMany(a => a.Bolumler).HasForeignKey(a => a.DilID);

            modelBuilder.Entity<Ogrenci>().ToTable("Ogrenci");
            modelBuilder.Entity<Ogrenci>().HasKey(a => a.OgrenciID);
            modelBuilder.Entity<Ogrenci>().HasOne(a => a.Bolum).WithMany(a => a.Ogrenciler).HasForeignKey(a => a.BolumID);
            modelBuilder.Entity<Ogrenci>().HasOne(a => a.OgrenciDurum).WithMany(a => a.Ogrenciler).HasForeignKey(a => a.OgrenciDurumID);
            modelBuilder.Entity<Ogrenci>().HasOne(a => a.Cinsiyet).WithMany(a => a.Ogrenciler).HasForeignKey(a => a.CinsiyetID);
            modelBuilder.Entity<Ogrenci>().HasOne(a => a.Kullanici).WithMany(a => a.Ogrenciler).HasForeignKey(a => a.KullaniciID);

            modelBuilder.Entity<OgretimElemani>().ToTable("OgretimElemani");
            modelBuilder.Entity<OgretimElemani>().HasKey(a => a.OgretimElemaniID);
            modelBuilder.Entity<OgretimElemani>().HasOne(a => a.Bolum).WithMany(a => a.OgretimElemanlari).HasForeignKey(a => a.BolumID);
            modelBuilder.Entity<OgretimElemani>().HasOne(a => a.Unvan).WithMany(a => a.OgretimElemanlari).HasForeignKey(a => a.UnvanID);
            modelBuilder.Entity<OgretimElemani>().HasOne(a => a.Cinsiyet).WithMany(a => a.OgretimElemanlari).HasForeignKey(a => a.CinsiyetID);
            modelBuilder.Entity<OgretimElemani>().HasOne(a => a.Kullanici).WithMany(a => a.OgretimElemanlari).HasForeignKey(a => a.KullaniciID);

            modelBuilder.Entity<Derslik>().ToTable("Derslik");
            modelBuilder.Entity<Derslik>().HasKey(a => a.DerslikID);
            modelBuilder.Entity<Derslik>().HasOne(a => a.DerslikTuru).WithMany(a=>a.Derslikler).HasForeignKey(a=> a.DerslikTuruID);

            modelBuilder.Entity<DersHavuzu>().ToTable("DersHavuzu");
            modelBuilder.Entity<DersHavuzu>().HasKey(a => a.DersHavuzuID);
            modelBuilder.Entity<DersHavuzu>().HasOne(a => a.Dil).WithMany(a => a.DersHavuzlari).HasForeignKey(a => a.DilID);
            modelBuilder.Entity<DersHavuzu>().HasOne(a => a.DersSeviyesi).WithMany(a => a.DersHavuzlari).HasForeignKey(a => a.DersSeviyesiID);
            modelBuilder.Entity<DersHavuzu>().HasOne(a => a.DersTuru).WithMany(a => a.DersHavuzlari).HasForeignKey(a => a.DersTuruID);

            modelBuilder.Entity<Mufredat>().ToTable("Mufredat");
            modelBuilder.Entity<Mufredat>().HasKey(a => a.MufredatID);
            modelBuilder.Entity<Mufredat>().HasOne(a => a.Bolum).WithMany(a => a.Mufredatlar).HasForeignKey(a => a.BolumID);
            modelBuilder.Entity<Mufredat>().HasOne(a => a.DersHavuzu).WithMany(a => a.Mufredatlar).HasForeignKey(a => a.DersHavuzuID);
            modelBuilder.Entity<Mufredat>().HasOne(a => a.AkademikYil).WithMany(a => a.Mufredatlar).HasForeignKey(a => a.AkademikYilID);
            modelBuilder.Entity<Mufredat>().HasOne(a => a.AkademikDonem).WithMany(a => a.Mufredatlar).HasForeignKey(a => a.AkademikDonemID);

            modelBuilder.Entity<DersAcma>().ToTable("DersAcma");
            modelBuilder.Entity<DersAcma>().HasKey(a => a.DersAcmaID);
            modelBuilder.Entity<DersAcma>().HasOne(a => a.AkademikYil).WithMany(a => a.DersAcmalar).HasForeignKey(a => a.AkademikYilID);
            modelBuilder.Entity<DersAcma>().HasOne(a => a.AkademikDonem).WithMany(a => a.DersAcmalar).HasForeignKey(a => a.AkademikDonemID);
            modelBuilder.Entity<DersAcma>().HasOne(a => a.Mufredat).WithMany(a => a.DersAcmalar).HasForeignKey(a => a.MufredatID);
            modelBuilder.Entity<DersAcma>().HasOne(a => a.OgretimElemani).WithMany(a => a.DersAcmalar).HasForeignKey(a => a.OgretimElemaniID);

            modelBuilder.Entity<DersAlma>().ToTable("DersAlma");
            modelBuilder.Entity<DersAlma>().HasKey(a => a.DersAlmaID);
            modelBuilder.Entity<DersAlma>().HasOne(a => a.DersAcma).WithMany(a => a.DersAlmalar).HasForeignKey(a => a.DersAcmaID);
            modelBuilder.Entity<DersAlma>().HasOne(a => a.Ogrenci).WithMany(a => a.DersAlmalar).HasForeignKey(a => a.OgrenciID);
            modelBuilder.Entity<DersAlma>().HasOne(a => a.DersDurum).WithMany(a => a.DersAlmalar).HasForeignKey(a => a.DersDurumID);

            modelBuilder.Entity<Sinav>().ToTable("Sinav");
            modelBuilder.Entity<Sinav>().HasKey(a => a.SinavID);
            modelBuilder.Entity<Sinav>().HasOne(a => a.DersAcma).WithMany(a => a.Sinavlar).HasForeignKey(a => a.DersAcmaID);
            modelBuilder.Entity<Sinav>().HasOne(a => a.SinavTuru).WithMany(a => a.Sinavlar).HasForeignKey(a => a.SinavTuruID);
            modelBuilder.Entity<Sinav>().HasOne(a => a.Derslik).WithMany(a => a.Sinavlar).HasForeignKey(a => a.DerslikID);
            modelBuilder.Entity<Sinav>().HasOne(a => a.OgretimElemani).WithMany(a => a.Sinavlar).HasForeignKey(a => a.OgretimElemaniID);

            modelBuilder.Entity<Degerlendirme>().ToTable("Degerlendirme");
            modelBuilder.Entity<Degerlendirme>().HasKey(a => a.DegerlendirmeID);
            modelBuilder.Entity<Degerlendirme>().HasOne(a => a.Sinav).WithMany(a => a.Degerlendirmeler).HasForeignKey(a => a.SinavID);
            modelBuilder.Entity<Degerlendirme>().HasOne(a => a.Ogrenci).WithMany(a => a.Degerlendirmeler).HasForeignKey(a => a.OgrenciID);

            modelBuilder.Entity<DersProgrami>().ToTable("DersProgrami");
            modelBuilder.Entity<DersProgrami>().HasKey(a => a.DersProgramiID);
            modelBuilder.Entity<DersProgrami>().HasOne(a => a.DersAcma).WithMany(a => a.DersProgramlari).HasForeignKey(a => a.DersAcmaID);
            modelBuilder.Entity<DersProgrami>().HasOne(a => a.Derslik).WithMany(a => a.DersProgramlari).HasForeignKey(a => a.DerslikID);
            modelBuilder.Entity<DersProgrami>().HasOne(a => a.Gun).WithMany(a => a.DersProgramlari).HasForeignKey(a => a.GunID);

            modelBuilder.Entity<Danismanlik>().ToTable("Danismanlik");
            modelBuilder.Entity<Danismanlik>().HasKey(a => a.DanismanlikID);
            modelBuilder.Entity<Danismanlik>().HasOne(a => a.OgretimElemani).WithMany(a => a.Danismanliklar).HasForeignKey(a => a.OgretimElemaniID);
            modelBuilder.Entity<Danismanlik>().HasOne(a => a.Ogrenci).WithMany(a => a.Danismanliklar).HasForeignKey(a => a.OgrenciID);

            modelBuilder.Entity<DersAcmaDersSaati>().ToTable("DersAcmaDersSaati");
            modelBuilder.Entity<DersAcmaDersSaati>().HasKey(a => a.DersAcmaDersSaatiID);
            modelBuilder.Entity<DersAcmaDersSaati>().HasOne(a => a.DersAcma).WithMany(a => a.DersAcmaDersSaatleri).HasForeignKey(a => a.DersAcmaID);
            modelBuilder.Entity<DersAcmaDersSaati>().HasOne(a => a.DersSaati).WithMany(a => a.DersAcmaDersSaatleri).HasForeignKey(a => a.DersSaatiID);



            base.OnModelCreating(modelBuilder);
        }



    }
}