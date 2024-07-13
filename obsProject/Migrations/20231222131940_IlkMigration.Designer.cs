﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using obsProject.Data;

#nullable disable

namespace obsProject.Migrations
{
    [DbContext(typeof(CodeFirstDbContext))]
    [Migration("20231222131940_IlkMigration")]
    partial class IlkMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("obsProject.Models.AkademikDonem", b =>
                {
                    b.Property<int>("AkademikDonemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AkademikDonemID"));

                    b.Property<string>("AkademikDonemAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AkademikDonemID");

                    b.ToTable("AkademikDonem", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.AkademikYil", b =>
                {
                    b.Property<int>("AkademikYilID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AkademikYilID"));

                    b.Property<string>("AkademikYilAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AkademikYilID");

                    b.ToTable("AkademikYil", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.Bolum", b =>
                {
                    b.Property<int>("BolumID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BolumID"));

                    b.Property<string>("BolumAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DilID")
                        .HasColumnType("int");

                    b.Property<int>("OgretimTuruID")
                        .HasColumnType("int");

                    b.Property<int>("ProgramTuruID")
                        .HasColumnType("int");

                    b.Property<string>("WebAdresi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BolumID");

                    b.HasIndex("DilID");

                    b.HasIndex("OgretimTuruID");

                    b.HasIndex("ProgramTuruID");

                    b.ToTable("Bolum", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.Cinsiyet", b =>
                {
                    b.Property<int>("CinsiyetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CinsiyetID"));

                    b.Property<string>("CinsiyetAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CinsiyetID");

                    b.ToTable("Cinsiyet", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.Danismanlik", b =>
                {
                    b.Property<int>("DanismanlikID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DanismanlikID"));

                    b.Property<int>("OgrenciID")
                        .HasColumnType("int");

                    b.Property<int>("OgretimElemaniID")
                        .HasColumnType("int");

                    b.HasKey("DanismanlikID");

                    b.HasIndex("OgrenciID");

                    b.HasIndex("OgretimElemaniID");

                    b.ToTable("Danismanlik", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.Degerlendirme", b =>
                {
                    b.Property<int>("DegerlendirmeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DegerlendirmeID"));

                    b.Property<int>("OgrenciID")
                        .HasColumnType("int");

                    b.Property<int>("SinavID")
                        .HasColumnType("int");

                    b.Property<double>("SinavNotu")
                        .HasColumnType("float");

                    b.HasKey("DegerlendirmeID");

                    b.HasIndex("OgrenciID");

                    b.HasIndex("SinavID");

                    b.ToTable("Degerlendirme", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.DersAcma", b =>
                {
                    b.Property<int>("DersAcmaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DersAcmaID"));

                    b.Property<int>("AkademikDonemID")
                        .HasColumnType("int");

                    b.Property<int>("AkademikYilID")
                        .HasColumnType("int");

                    b.Property<int>("Kontenjan")
                        .HasColumnType("int");

                    b.Property<int>("MufredatID")
                        .HasColumnType("int");

                    b.Property<int>("OgretimElemaniID")
                        .HasColumnType("int");

                    b.HasKey("DersAcmaID");

                    b.HasIndex("AkademikDonemID");

                    b.HasIndex("AkademikYilID");

                    b.HasIndex("MufredatID");

                    b.HasIndex("OgretimElemaniID");

                    b.ToTable("DersAcma", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.DersAlma", b =>
                {
                    b.Property<int>("DersAlmaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DersAlmaID"));

                    b.Property<int>("DersAcmaID")
                        .HasColumnType("int");

                    b.Property<int>("DersDurumID")
                        .HasColumnType("int");

                    b.Property<int>("OgrenciID")
                        .HasColumnType("int");

                    b.HasKey("DersAlmaID");

                    b.HasIndex("DersAcmaID");

                    b.HasIndex("DersDurumID");

                    b.HasIndex("OgrenciID");

                    b.ToTable("DersAlma", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.DersDurum", b =>
                {
                    b.Property<int>("DersDurumID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DersDurumID"));

                    b.Property<int>("DersDurumAdi")
                        .HasColumnType("int");

                    b.HasKey("DersDurumID");

                    b.ToTable("DersDurum", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.DersHavuzu", b =>
                {
                    b.Property<int>("DersHavuzuID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DersHavuzuID"));

                    b.Property<string>("DersAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DersKodu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DersSeviyesiID")
                        .HasColumnType("int");

                    b.Property<int>("DersTuruID")
                        .HasColumnType("int");

                    b.Property<int>("DilID")
                        .HasColumnType("int");

                    b.Property<int>("ECTS")
                        .HasColumnType("int");

                    b.Property<double>("Kredi")
                        .HasColumnType("float");

                    b.Property<double>("Teorik")
                        .HasColumnType("float");

                    b.Property<double>("Uygulama")
                        .HasColumnType("float");

                    b.HasKey("DersHavuzuID");

                    b.HasIndex("DersSeviyesiID");

                    b.HasIndex("DersTuruID");

                    b.HasIndex("DilID");

                    b.ToTable("DersHavuzu", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.DersProgrami", b =>
                {
                    b.Property<int>("DersProgramiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DersProgramiID"));

                    b.Property<int>("DersAcmaID")
                        .HasColumnType("int");

                    b.Property<int>("DersSaati")
                        .HasColumnType("int");

                    b.Property<int>("DerslikID")
                        .HasColumnType("int");

                    b.Property<int>("GunID")
                        .HasColumnType("int");

                    b.HasKey("DersProgramiID");

                    b.HasIndex("DersAcmaID");

                    b.HasIndex("DerslikID");

                    b.HasIndex("GunID");

                    b.ToTable("DersProgrami", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.DersSeviyesi", b =>
                {
                    b.Property<int>("DersSeviyesiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DersSeviyesiID"));

                    b.Property<string>("DersSeviyesiAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DersSeviyesiID");

                    b.ToTable("DersSeviyesi", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.DersTuru", b =>
                {
                    b.Property<int>("DersTuruID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DersTuruID"));

                    b.Property<int>("DersTuruAdi")
                        .HasColumnType("int");

                    b.HasKey("DersTuruID");

                    b.ToTable("DersTuru", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.Derslik", b =>
                {
                    b.Property<int>("DerslikID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DerslikID"));

                    b.Property<string>("DerslikAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DerslikTuruID")
                        .HasColumnType("int");

                    b.Property<int>("Kapasite")
                        .HasColumnType("int");

                    b.HasKey("DerslikID");

                    b.HasIndex("DerslikTuruID");

                    b.ToTable("Derslik", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.DerslikTuru", b =>
                {
                    b.Property<int>("DerslikTuruID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DerslikTuruID"));

                    b.Property<string>("DerslikTuruAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DerslikTuruID");

                    b.ToTable("DerslikTuru", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.Dil", b =>
                {
                    b.Property<int>("DilID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DilID"));

                    b.Property<string>("DilAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DilID");

                    b.ToTable("Dil", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.Gun", b =>
                {
                    b.Property<int>("GunID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GunID"));

                    b.Property<string>("GunAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GunID");

                    b.ToTable("Gun", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.Kullanici", b =>
                {
                    b.Property<int>("KullaniciID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KullaniciID"));

                    b.Property<string>("KullaniciAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KullaniciTuruID")
                        .HasColumnType("int");

                    b.Property<string>("Parola")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KullaniciID");

                    b.HasIndex("KullaniciTuruID");

                    b.ToTable("Kullanici", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.KullaniciTuru", b =>
                {
                    b.Property<int>("KullaniciTuruID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KullaniciTuruID"));

                    b.Property<string>("KullaniciTuruAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KullaniciTuruID");

                    b.ToTable("KullaniciTuru", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.Mufredat", b =>
                {
                    b.Property<int>("MufredatID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MufredatID"));

                    b.Property<int>("AkademikDonemID")
                        .HasColumnType("int");

                    b.Property<int>("AkademikYilID")
                        .HasColumnType("int");

                    b.Property<int>("BolumID")
                        .HasColumnType("int");

                    b.Property<int>("DersDonemi")
                        .HasColumnType("int");

                    b.Property<int>("DersHavuzuID")
                        .HasColumnType("int");

                    b.HasKey("MufredatID");

                    b.HasIndex("AkademikDonemID");

                    b.HasIndex("AkademikYilID");

                    b.HasIndex("BolumID");

                    b.HasIndex("DersHavuzuID");

                    b.ToTable("Mufredat", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.Ogrenci", b =>
                {
                    b.Property<int>("OgrenciID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OgrenciID"));

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AyrilmaTarihi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BolumID")
                        .HasColumnType("int");

                    b.Property<int>("CinsiyetID")
                        .HasColumnType("int");

                    b.Property<string>("DogumTarihi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KayitTarihi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KullaniciID")
                        .HasColumnType("int");

                    b.Property<int>("OgrenciDurumID")
                        .HasColumnType("int");

                    b.Property<string>("OgrenciNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soyadi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TCKimlikNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OgrenciID");

                    b.HasIndex("BolumID");

                    b.HasIndex("CinsiyetID");

                    b.HasIndex("KullaniciID");

                    b.HasIndex("OgrenciDurumID");

                    b.ToTable("Ogrenci", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.OgrenciDurum", b =>
                {
                    b.Property<int>("OgrenciDurumID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OgrenciDurumID"));

                    b.Property<string>("OgrenciDurumAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OgrenciDurumID");

                    b.ToTable("OgrenciDurum", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.OgretimElemani", b =>
                {
                    b.Property<int>("OgretimElemaniID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OgretimElemaniID"));

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BolumID")
                        .HasColumnType("int");

                    b.Property<int>("CinsiyetID")
                        .HasColumnType("int");

                    b.Property<string>("DogumTarihi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KullaniciID")
                        .HasColumnType("int");

                    b.Property<string>("KurumSicilNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soyadi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TCKimlikNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnvanID")
                        .HasColumnType("int");

                    b.HasKey("OgretimElemaniID");

                    b.HasIndex("BolumID");

                    b.HasIndex("CinsiyetID");

                    b.HasIndex("KullaniciID");

                    b.HasIndex("UnvanID");

                    b.ToTable("OgretimElemani", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.OgretimTuru", b =>
                {
                    b.Property<int>("OgretimTuruID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OgretimTuruID"));

                    b.Property<string>("OgretimTuruAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OgretimTuruID");

                    b.ToTable("OgretimTuru", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.ProgramTuru", b =>
                {
                    b.Property<int>("ProgramTuruID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProgramTuruID"));

                    b.Property<string>("ProgramTuruAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProgramTuruID");

                    b.ToTable("ProgramTuru", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.Sinav", b =>
                {
                    b.Property<int>("SinavID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SinavID"));

                    b.Property<int>("DersAcmaID")
                        .HasColumnType("int");

                    b.Property<int>("DerslikID")
                        .HasColumnType("int");

                    b.Property<double>("EtkiOrani")
                        .HasColumnType("float");

                    b.Property<int>("OgretimElemaniID")
                        .HasColumnType("int");

                    b.Property<string>("SinavSaati")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SinavTarihi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SinavTuruID")
                        .HasColumnType("int");

                    b.HasKey("SinavID");

                    b.HasIndex("DersAcmaID");

                    b.HasIndex("DerslikID");

                    b.HasIndex("OgretimElemaniID");

                    b.HasIndex("SinavTuruID");

                    b.ToTable("Sinav", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.SinavTuru", b =>
                {
                    b.Property<int>("SinavTuruID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SinavTuruID"));

                    b.Property<string>("SinavTuruAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SinavTuruID");

                    b.ToTable("SinavTuru", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.Unvan", b =>
                {
                    b.Property<int>("UnvanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UnvanID"));

                    b.Property<string>("UnvanAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UnvanID");

                    b.ToTable("Unvan", (string)null);
                });

            modelBuilder.Entity("obsProject.Models.Bolum", b =>
                {
                    b.HasOne("obsProject.Models.Dil", "Dil")
                        .WithMany()
                        .HasForeignKey("DilID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("obsProject.Models.OgretimTuru", "OgretimTuru")
                        .WithMany()
                        .HasForeignKey("OgretimTuruID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("obsProject.Models.ProgramTuru", "ProgramTuru")
                        .WithMany()
                        .HasForeignKey("ProgramTuruID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dil");

                    b.Navigation("OgretimTuru");

                    b.Navigation("ProgramTuru");
                });

            modelBuilder.Entity("obsProject.Models.Danismanlik", b =>
                {
                    b.HasOne("obsProject.Models.Ogrenci", "Ogrenci")
                        .WithMany()
                        .HasForeignKey("OgrenciID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("obsProject.Models.OgretimElemani", "OgretimElemani")
                        .WithMany()
                        .HasForeignKey("OgretimElemaniID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Ogrenci");

                    b.Navigation("OgretimElemani");
                });

            modelBuilder.Entity("obsProject.Models.Degerlendirme", b =>
                {
                    b.HasOne("obsProject.Models.Ogrenci", "Ogrenci")
                        .WithMany()
                        .HasForeignKey("OgrenciID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("obsProject.Models.Sinav", "Sinav")
                        .WithMany()
                        .HasForeignKey("SinavID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Ogrenci");

                    b.Navigation("Sinav");
                });

            modelBuilder.Entity("obsProject.Models.DersAcma", b =>
                {
                    b.HasOne("obsProject.Models.AkademikDonem", "AkademikDonem")
                        .WithMany()
                        .HasForeignKey("AkademikDonemID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("obsProject.Models.AkademikYil", "AkademikYil")
                        .WithMany()
                        .HasForeignKey("AkademikYilID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("obsProject.Models.Mufredat", "Mufredat")
                        .WithMany()
                        .HasForeignKey("MufredatID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("obsProject.Models.OgretimElemani", "OgretimElemani")
                        .WithMany()
                        .HasForeignKey("OgretimElemaniID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AkademikDonem");

                    b.Navigation("AkademikYil");

                    b.Navigation("Mufredat");

                    b.Navigation("OgretimElemani");
                });

            modelBuilder.Entity("obsProject.Models.DersAlma", b =>
                {
                    b.HasOne("obsProject.Models.DersAcma", "DersAcma")
                        .WithMany()
                        .HasForeignKey("DersAcmaID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("obsProject.Models.DersDurum", "DersDurum")
                        .WithMany()
                        .HasForeignKey("DersDurumID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("obsProject.Models.Ogrenci", "Ogrenci")
                        .WithMany()
                        .HasForeignKey("OgrenciID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("DersAcma");

                    b.Navigation("DersDurum");

                    b.Navigation("Ogrenci");
                });

            modelBuilder.Entity("obsProject.Models.DersHavuzu", b =>
                {
                    b.HasOne("obsProject.Models.DersSeviyesi", "DersSeviyesi")
                        .WithMany()
                        .HasForeignKey("DersSeviyesiID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("obsProject.Models.DersTuru", "DersTuru")
                        .WithMany()
                        .HasForeignKey("DersTuruID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("obsProject.Models.Dil", "Dil")
                        .WithMany()
                        .HasForeignKey("DilID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("DersSeviyesi");

                    b.Navigation("DersTuru");

                    b.Navigation("Dil");
                });

            modelBuilder.Entity("obsProject.Models.DersProgrami", b =>
                {
                    b.HasOne("obsProject.Models.DersAcma", "DersAcma")
                        .WithMany()
                        .HasForeignKey("DersAcmaID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("obsProject.Models.Derslik", "Derslik")
                        .WithMany()
                        .HasForeignKey("DerslikID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("obsProject.Models.Gun", "Gun")
                        .WithMany()
                        .HasForeignKey("GunID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("DersAcma");

                    b.Navigation("Derslik");

                    b.Navigation("Gun");
                });

            modelBuilder.Entity("obsProject.Models.Derslik", b =>
                {
                    b.HasOne("obsProject.Models.DerslikTuru", "DerslikTuru")
                        .WithMany()
                        .HasForeignKey("DerslikTuruID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DerslikTuru");
                });

            modelBuilder.Entity("obsProject.Models.Kullanici", b =>
                {
                    b.HasOne("obsProject.Models.KullaniciTuru", "KullaniciTuru")
                        .WithMany()
                        .HasForeignKey("KullaniciTuruID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KullaniciTuru");
                });

            modelBuilder.Entity("obsProject.Models.Mufredat", b =>
                {
                    b.HasOne("obsProject.Models.AkademikDonem", "AkademikDonem")
                        .WithMany()
                        .HasForeignKey("AkademikDonemID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("obsProject.Models.AkademikYil", "AkademikYil")
                        .WithMany()
                        .HasForeignKey("AkademikYilID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("obsProject.Models.Bolum", "Bolum")
                        .WithMany()
                        .HasForeignKey("BolumID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("obsProject.Models.DersHavuzu", "DersHavuzu")
                        .WithMany()
                        .HasForeignKey("DersHavuzuID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AkademikDonem");

                    b.Navigation("AkademikYil");

                    b.Navigation("Bolum");

                    b.Navigation("DersHavuzu");
                });

            modelBuilder.Entity("obsProject.Models.Ogrenci", b =>
                {
                    b.HasOne("obsProject.Models.Bolum", "Bolum")
                        .WithMany()
                        .HasForeignKey("BolumID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("obsProject.Models.Cinsiyet", "Cinsiyet")
                        .WithMany()
                        .HasForeignKey("CinsiyetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("obsProject.Models.Kullanici", "Kullanici")
                        .WithMany()
                        .HasForeignKey("KullaniciID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("obsProject.Models.OgrenciDurum", "OgrenciDurum")
                        .WithMany()
                        .HasForeignKey("OgrenciDurumID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bolum");

                    b.Navigation("Cinsiyet");

                    b.Navigation("Kullanici");

                    b.Navigation("OgrenciDurum");
                });

            modelBuilder.Entity("obsProject.Models.OgretimElemani", b =>
                {
                    b.HasOne("obsProject.Models.Bolum", "Bolum")
                        .WithMany()
                        .HasForeignKey("BolumID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("obsProject.Models.Cinsiyet", "Cinsiyet")
                        .WithMany()
                        .HasForeignKey("CinsiyetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("obsProject.Models.Kullanici", "Kullanici")
                        .WithMany()
                        .HasForeignKey("KullaniciID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("obsProject.Models.Unvan", "Unvan")
                        .WithMany()
                        .HasForeignKey("UnvanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bolum");

                    b.Navigation("Cinsiyet");

                    b.Navigation("Kullanici");

                    b.Navigation("Unvan");
                });

            modelBuilder.Entity("obsProject.Models.Sinav", b =>
                {
                    b.HasOne("obsProject.Models.DersAcma", "DersAcma")
                        .WithMany()
                        .HasForeignKey("DersAcmaID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("obsProject.Models.Derslik", "Derslik")
                        .WithMany()
                        .HasForeignKey("DerslikID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("obsProject.Models.OgretimElemani", "OgretimElemani")
                        .WithMany()
                        .HasForeignKey("OgretimElemaniID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("obsProject.Models.SinavTuru", "SinavTuru")
                        .WithMany()
                        .HasForeignKey("SinavTuruID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("DersAcma");

                    b.Navigation("Derslik");

                    b.Navigation("OgretimElemani");

                    b.Navigation("SinavTuru");
                });
#pragma warning restore 612, 618
        }
    }
}
