using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace obsProject.Migrations
{
    /// <inheritdoc />
    public partial class IlkMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AkademikDonem",
                columns: table => new
                {
                    AkademikDonemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AkademikDonemAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkademikDonem", x => x.AkademikDonemID);
                });

            migrationBuilder.CreateTable(
                name: "AkademikYil",
                columns: table => new
                {
                    AkademikYilID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AkademikYilAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkademikYil", x => x.AkademikYilID);
                });

            migrationBuilder.CreateTable(
                name: "Cinsiyet",
                columns: table => new
                {
                    CinsiyetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CinsiyetAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinsiyet", x => x.CinsiyetID);
                });

            migrationBuilder.CreateTable(
                name: "DersDurum",
                columns: table => new
                {
                    DersDurumID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersDurumAdi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersDurum", x => x.DersDurumID);
                });

            migrationBuilder.CreateTable(
                name: "DerslikTuru",
                columns: table => new
                {
                    DerslikTuruID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DerslikTuruAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DerslikTuru", x => x.DerslikTuruID);
                });

            migrationBuilder.CreateTable(
                name: "DersSeviyesi",
                columns: table => new
                {
                    DersSeviyesiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersSeviyesiAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersSeviyesi", x => x.DersSeviyesiID);
                });

            migrationBuilder.CreateTable(
                name: "DersTuru",
                columns: table => new
                {
                    DersTuruID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersTuruAdi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersTuru", x => x.DersTuruID);
                });

            migrationBuilder.CreateTable(
                name: "Dil",
                columns: table => new
                {
                    DilID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DilAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dil", x => x.DilID);
                });

            migrationBuilder.CreateTable(
                name: "Gun",
                columns: table => new
                {
                    GunID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GunAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gun", x => x.GunID);
                });

            migrationBuilder.CreateTable(
                name: "KullaniciTuru",
                columns: table => new
                {
                    KullaniciTuruID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciTuruAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciTuru", x => x.KullaniciTuruID);
                });

            migrationBuilder.CreateTable(
                name: "OgrenciDurum",
                columns: table => new
                {
                    OgrenciDurumID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgrenciDurumAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrenciDurum", x => x.OgrenciDurumID);
                });

            migrationBuilder.CreateTable(
                name: "OgretimTuru",
                columns: table => new
                {
                    OgretimTuruID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgretimTuruAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgretimTuru", x => x.OgretimTuruID);
                });

            migrationBuilder.CreateTable(
                name: "ProgramTuru",
                columns: table => new
                {
                    ProgramTuruID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramTuruAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramTuru", x => x.ProgramTuruID);
                });

            migrationBuilder.CreateTable(
                name: "SinavTuru",
                columns: table => new
                {
                    SinavTuruID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SinavTuruAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinavTuru", x => x.SinavTuruID);
                });

            migrationBuilder.CreateTable(
                name: "Unvan",
                columns: table => new
                {
                    UnvanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnvanAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unvan", x => x.UnvanID);
                });

            migrationBuilder.CreateTable(
                name: "Derslik",
                columns: table => new
                {
                    DerslikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DerslikAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kapasite = table.Column<int>(type: "int", nullable: false),
                    DerslikTuruID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Derslik", x => x.DerslikID);
                    table.ForeignKey(
                        name: "FK_Derslik_DerslikTuru_DerslikTuruID",
                        column: x => x.DerslikTuruID,
                        principalTable: "DerslikTuru",
                        principalColumn: "DerslikTuruID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DersHavuzu",
                columns: table => new
                {
                    DersHavuzuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersKodu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DersAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Teorik = table.Column<double>(type: "float", nullable: false),
                    Uygulama = table.Column<double>(type: "float", nullable: false),
                    Kredi = table.Column<double>(type: "float", nullable: false),
                    ECTS = table.Column<int>(type: "int", nullable: false),
                    DilID = table.Column<int>(type: "int", nullable: false),
                    DersSeviyesiID = table.Column<int>(type: "int", nullable: false),
                    DersTuruID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersHavuzu", x => x.DersHavuzuID);
                    table.ForeignKey(
                        name: "FK_DersHavuzu_DersSeviyesi_DersSeviyesiID",
                        column: x => x.DersSeviyesiID,
                        principalTable: "DersSeviyesi",
                        principalColumn: "DersSeviyesiID");
                    table.ForeignKey(
                        name: "FK_DersHavuzu_DersTuru_DersTuruID",
                        column: x => x.DersTuruID,
                        principalTable: "DersTuru",
                        principalColumn: "DersTuruID");
                    table.ForeignKey(
                        name: "FK_DersHavuzu_Dil_DilID",
                        column: x => x.DilID,
                        principalTable: "Dil",
                        principalColumn: "DilID");
                });

            migrationBuilder.CreateTable(
                name: "Kullanici",
                columns: table => new
                {
                    KullaniciID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parola = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullaniciTuruID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanici", x => x.KullaniciID);
                    table.ForeignKey(
                        name: "FK_Kullanici_KullaniciTuru_KullaniciTuruID",
                        column: x => x.KullaniciTuruID,
                        principalTable: "KullaniciTuru",
                        principalColumn: "KullaniciTuruID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bolum",
                columns: table => new
                {
                    BolumID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BolumAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramTuruID = table.Column<int>(type: "int", nullable: false),
                    OgretimTuruID = table.Column<int>(type: "int", nullable: false),
                    DilID = table.Column<int>(type: "int", nullable: false),
                    WebAdresi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bolum", x => x.BolumID);
                    table.ForeignKey(
                        name: "FK_Bolum_Dil_DilID",
                        column: x => x.DilID,
                        principalTable: "Dil",
                        principalColumn: "DilID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bolum_OgretimTuru_OgretimTuruID",
                        column: x => x.OgretimTuruID,
                        principalTable: "OgretimTuru",
                        principalColumn: "OgretimTuruID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bolum_ProgramTuru_ProgramTuruID",
                        column: x => x.ProgramTuruID,
                        principalTable: "ProgramTuru",
                        principalColumn: "ProgramTuruID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mufredat",
                columns: table => new
                {
                    MufredatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersDonemi = table.Column<int>(type: "int", nullable: false),
                    BolumID = table.Column<int>(type: "int", nullable: false),
                    DersHavuzuID = table.Column<int>(type: "int", nullable: false),
                    AkademikYilID = table.Column<int>(type: "int", nullable: false),
                    AkademikDonemID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mufredat", x => x.MufredatID);
                    table.ForeignKey(
                        name: "FK_Mufredat_AkademikDonem_AkademikDonemID",
                        column: x => x.AkademikDonemID,
                        principalTable: "AkademikDonem",
                        principalColumn: "AkademikDonemID");
                    table.ForeignKey(
                        name: "FK_Mufredat_AkademikYil_AkademikYilID",
                        column: x => x.AkademikYilID,
                        principalTable: "AkademikYil",
                        principalColumn: "AkademikYilID");
                    table.ForeignKey(
                        name: "FK_Mufredat_Bolum_BolumID",
                        column: x => x.BolumID,
                        principalTable: "Bolum",
                        principalColumn: "BolumID");
                    table.ForeignKey(
                        name: "FK_Mufredat_DersHavuzu_DersHavuzuID",
                        column: x => x.DersHavuzuID,
                        principalTable: "DersHavuzu",
                        principalColumn: "DersHavuzuID");
                });

            migrationBuilder.CreateTable(
                name: "Ogrenci",
                columns: table => new
                {
                    OgrenciID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BolumID = table.Column<int>(type: "int", nullable: false),
                    OgrenciNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OgrenciDurumID = table.Column<int>(type: "int", nullable: false),
                    KayitTarihi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AyrilmaTarihi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyadi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TCKimlikNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CinsiyetID = table.Column<int>(type: "int", nullable: false),
                    DogumTarihi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullaniciID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrenci", x => x.OgrenciID);
                    table.ForeignKey(
                        name: "FK_Ogrenci_Bolum_BolumID",
                        column: x => x.BolumID,
                        principalTable: "Bolum",
                        principalColumn: "BolumID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ogrenci_Cinsiyet_CinsiyetID",
                        column: x => x.CinsiyetID,
                        principalTable: "Cinsiyet",
                        principalColumn: "CinsiyetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ogrenci_Kullanici_KullaniciID",
                        column: x => x.KullaniciID,
                        principalTable: "Kullanici",
                        principalColumn: "KullaniciID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ogrenci_OgrenciDurum_OgrenciDurumID",
                        column: x => x.OgrenciDurumID,
                        principalTable: "OgrenciDurum",
                        principalColumn: "OgrenciDurumID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OgretimElemani",
                columns: table => new
                {
                    OgretimElemaniID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KurumSicilNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyadi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TCKimlikNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DogumTarihi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BolumID = table.Column<int>(type: "int", nullable: false),
                    UnvanID = table.Column<int>(type: "int", nullable: false),
                    CinsiyetID = table.Column<int>(type: "int", nullable: false),
                    KullaniciID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgretimElemani", x => x.OgretimElemaniID);
                    table.ForeignKey(
                        name: "FK_OgretimElemani_Bolum_BolumID",
                        column: x => x.BolumID,
                        principalTable: "Bolum",
                        principalColumn: "BolumID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OgretimElemani_Cinsiyet_CinsiyetID",
                        column: x => x.CinsiyetID,
                        principalTable: "Cinsiyet",
                        principalColumn: "CinsiyetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OgretimElemani_Kullanici_KullaniciID",
                        column: x => x.KullaniciID,
                        principalTable: "Kullanici",
                        principalColumn: "KullaniciID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OgretimElemani_Unvan_UnvanID",
                        column: x => x.UnvanID,
                        principalTable: "Unvan",
                        principalColumn: "UnvanID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Danismanlik",
                columns: table => new
                {
                    DanismanlikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgretimElemaniID = table.Column<int>(type: "int", nullable: false),
                    OgrenciID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Danismanlik", x => x.DanismanlikID);
                    table.ForeignKey(
                        name: "FK_Danismanlik_Ogrenci_OgrenciID",
                        column: x => x.OgrenciID,
                        principalTable: "Ogrenci",
                        principalColumn: "OgrenciID");
                    table.ForeignKey(
                        name: "FK_Danismanlik_OgretimElemani_OgretimElemaniID",
                        column: x => x.OgretimElemaniID,
                        principalTable: "OgretimElemani",
                        principalColumn: "OgretimElemaniID");
                });

            migrationBuilder.CreateTable(
                name: "DersAcma",
                columns: table => new
                {
                    DersAcmaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kontenjan = table.Column<int>(type: "int", nullable: false),
                    AkademikDonemID = table.Column<int>(type: "int", nullable: false),
                    AkademikYilID = table.Column<int>(type: "int", nullable: false),
                    MufredatID = table.Column<int>(type: "int", nullable: false),
                    OgretimElemaniID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersAcma", x => x.DersAcmaID);
                    table.ForeignKey(
                        name: "FK_DersAcma_AkademikDonem_AkademikDonemID",
                        column: x => x.AkademikDonemID,
                        principalTable: "AkademikDonem",
                        principalColumn: "AkademikDonemID");
                    table.ForeignKey(
                        name: "FK_DersAcma_AkademikYil_AkademikYilID",
                        column: x => x.AkademikYilID,
                        principalTable: "AkademikYil",
                        principalColumn: "AkademikYilID");
                    table.ForeignKey(
                        name: "FK_DersAcma_Mufredat_MufredatID",
                        column: x => x.MufredatID,
                        principalTable: "Mufredat",
                        principalColumn: "MufredatID");
                    table.ForeignKey(
                        name: "FK_DersAcma_OgretimElemani_OgretimElemaniID",
                        column: x => x.OgretimElemaniID,
                        principalTable: "OgretimElemani",
                        principalColumn: "OgretimElemaniID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DersAlma",
                columns: table => new
                {
                    DersAlmaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersAcmaID = table.Column<int>(type: "int", nullable: false),
                    OgrenciID = table.Column<int>(type: "int", nullable: false),
                    DersDurumID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersAlma", x => x.DersAlmaID);
                    table.ForeignKey(
                        name: "FK_DersAlma_DersAcma_DersAcmaID",
                        column: x => x.DersAcmaID,
                        principalTable: "DersAcma",
                        principalColumn: "DersAcmaID");
                    table.ForeignKey(
                        name: "FK_DersAlma_DersDurum_DersDurumID",
                        column: x => x.DersDurumID,
                        principalTable: "DersDurum",
                        principalColumn: "DersDurumID");
                    table.ForeignKey(
                        name: "FK_DersAlma_Ogrenci_OgrenciID",
                        column: x => x.OgrenciID,
                        principalTable: "Ogrenci",
                        principalColumn: "OgrenciID");
                });

            migrationBuilder.CreateTable(
                name: "DersProgrami",
                columns: table => new
                {
                    DersProgramiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersSaati = table.Column<int>(type: "int", nullable: false),
                    DersAcmaID = table.Column<int>(type: "int", nullable: false),
                    DerslikID = table.Column<int>(type: "int", nullable: false),
                    GunID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersProgrami", x => x.DersProgramiID);
                    table.ForeignKey(
                        name: "FK_DersProgrami_DersAcma_DersAcmaID",
                        column: x => x.DersAcmaID,
                        principalTable: "DersAcma",
                        principalColumn: "DersAcmaID");
                    table.ForeignKey(
                        name: "FK_DersProgrami_Derslik_DerslikID",
                        column: x => x.DerslikID,
                        principalTable: "Derslik",
                        principalColumn: "DerslikID");
                    table.ForeignKey(
                        name: "FK_DersProgrami_Gun_GunID",
                        column: x => x.GunID,
                        principalTable: "Gun",
                        principalColumn: "GunID");
                });

            migrationBuilder.CreateTable(
                name: "Sinav",
                columns: table => new
                {
                    SinavID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EtkiOrani = table.Column<double>(type: "float", nullable: false),
                    SinavTarihi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SinavSaati = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DersAcmaID = table.Column<int>(type: "int", nullable: false),
                    SinavTuruID = table.Column<int>(type: "int", nullable: false),
                    DerslikID = table.Column<int>(type: "int", nullable: false),
                    OgretimElemaniID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sinav", x => x.SinavID);
                    table.ForeignKey(
                        name: "FK_Sinav_DersAcma_DersAcmaID",
                        column: x => x.DersAcmaID,
                        principalTable: "DersAcma",
                        principalColumn: "DersAcmaID");
                    table.ForeignKey(
                        name: "FK_Sinav_Derslik_DerslikID",
                        column: x => x.DerslikID,
                        principalTable: "Derslik",
                        principalColumn: "DerslikID");
                    table.ForeignKey(
                        name: "FK_Sinav_OgretimElemani_OgretimElemaniID",
                        column: x => x.OgretimElemaniID,
                        principalTable: "OgretimElemani",
                        principalColumn: "OgretimElemaniID");
                    table.ForeignKey(
                        name: "FK_Sinav_SinavTuru_SinavTuruID",
                        column: x => x.SinavTuruID,
                        principalTable: "SinavTuru",
                        principalColumn: "SinavTuruID");
                });

            migrationBuilder.CreateTable(
                name: "Degerlendirme",
                columns: table => new
                {
                    DegerlendirmeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SinavNotu = table.Column<double>(type: "float", nullable: false),
                    SinavID = table.Column<int>(type: "int", nullable: false),
                    OgrenciID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degerlendirme", x => x.DegerlendirmeID);
                    table.ForeignKey(
                        name: "FK_Degerlendirme_Ogrenci_OgrenciID",
                        column: x => x.OgrenciID,
                        principalTable: "Ogrenci",
                        principalColumn: "OgrenciID");
                    table.ForeignKey(
                        name: "FK_Degerlendirme_Sinav_SinavID",
                        column: x => x.SinavID,
                        principalTable: "Sinav",
                        principalColumn: "SinavID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bolum_DilID",
                table: "Bolum",
                column: "DilID");

            migrationBuilder.CreateIndex(
                name: "IX_Bolum_OgretimTuruID",
                table: "Bolum",
                column: "OgretimTuruID");

            migrationBuilder.CreateIndex(
                name: "IX_Bolum_ProgramTuruID",
                table: "Bolum",
                column: "ProgramTuruID");

            migrationBuilder.CreateIndex(
                name: "IX_Danismanlik_OgrenciID",
                table: "Danismanlik",
                column: "OgrenciID");

            migrationBuilder.CreateIndex(
                name: "IX_Danismanlik_OgretimElemaniID",
                table: "Danismanlik",
                column: "OgretimElemaniID");

            migrationBuilder.CreateIndex(
                name: "IX_Degerlendirme_OgrenciID",
                table: "Degerlendirme",
                column: "OgrenciID");

            migrationBuilder.CreateIndex(
                name: "IX_Degerlendirme_SinavID",
                table: "Degerlendirme",
                column: "SinavID");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcma_AkademikDonemID",
                table: "DersAcma",
                column: "AkademikDonemID");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcma_AkademikYilID",
                table: "DersAcma",
                column: "AkademikYilID");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcma_MufredatID",
                table: "DersAcma",
                column: "MufredatID");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcma_OgretimElemaniID",
                table: "DersAcma",
                column: "OgretimElemaniID");

            migrationBuilder.CreateIndex(
                name: "IX_DersAlma_DersAcmaID",
                table: "DersAlma",
                column: "DersAcmaID");

            migrationBuilder.CreateIndex(
                name: "IX_DersAlma_DersDurumID",
                table: "DersAlma",
                column: "DersDurumID");

            migrationBuilder.CreateIndex(
                name: "IX_DersAlma_OgrenciID",
                table: "DersAlma",
                column: "OgrenciID");

            migrationBuilder.CreateIndex(
                name: "IX_DersHavuzu_DersSeviyesiID",
                table: "DersHavuzu",
                column: "DersSeviyesiID");

            migrationBuilder.CreateIndex(
                name: "IX_DersHavuzu_DersTuruID",
                table: "DersHavuzu",
                column: "DersTuruID");

            migrationBuilder.CreateIndex(
                name: "IX_DersHavuzu_DilID",
                table: "DersHavuzu",
                column: "DilID");

            migrationBuilder.CreateIndex(
                name: "IX_Derslik_DerslikTuruID",
                table: "Derslik",
                column: "DerslikTuruID");

            migrationBuilder.CreateIndex(
                name: "IX_DersProgrami_DersAcmaID",
                table: "DersProgrami",
                column: "DersAcmaID");

            migrationBuilder.CreateIndex(
                name: "IX_DersProgrami_DerslikID",
                table: "DersProgrami",
                column: "DerslikID");

            migrationBuilder.CreateIndex(
                name: "IX_DersProgrami_GunID",
                table: "DersProgrami",
                column: "GunID");

            migrationBuilder.CreateIndex(
                name: "IX_Kullanici_KullaniciTuruID",
                table: "Kullanici",
                column: "KullaniciTuruID");

            migrationBuilder.CreateIndex(
                name: "IX_Mufredat_AkademikDonemID",
                table: "Mufredat",
                column: "AkademikDonemID");

            migrationBuilder.CreateIndex(
                name: "IX_Mufredat_AkademikYilID",
                table: "Mufredat",
                column: "AkademikYilID");

            migrationBuilder.CreateIndex(
                name: "IX_Mufredat_BolumID",
                table: "Mufredat",
                column: "BolumID");

            migrationBuilder.CreateIndex(
                name: "IX_Mufredat_DersHavuzuID",
                table: "Mufredat",
                column: "DersHavuzuID");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenci_BolumID",
                table: "Ogrenci",
                column: "BolumID");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenci_CinsiyetID",
                table: "Ogrenci",
                column: "CinsiyetID");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenci_KullaniciID",
                table: "Ogrenci",
                column: "KullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenci_OgrenciDurumID",
                table: "Ogrenci",
                column: "OgrenciDurumID");

            migrationBuilder.CreateIndex(
                name: "IX_OgretimElemani_BolumID",
                table: "OgretimElemani",
                column: "BolumID");

            migrationBuilder.CreateIndex(
                name: "IX_OgretimElemani_CinsiyetID",
                table: "OgretimElemani",
                column: "CinsiyetID");

            migrationBuilder.CreateIndex(
                name: "IX_OgretimElemani_KullaniciID",
                table: "OgretimElemani",
                column: "KullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_OgretimElemani_UnvanID",
                table: "OgretimElemani",
                column: "UnvanID");

            migrationBuilder.CreateIndex(
                name: "IX_Sinav_DersAcmaID",
                table: "Sinav",
                column: "DersAcmaID");

            migrationBuilder.CreateIndex(
                name: "IX_Sinav_DerslikID",
                table: "Sinav",
                column: "DerslikID");

            migrationBuilder.CreateIndex(
                name: "IX_Sinav_OgretimElemaniID",
                table: "Sinav",
                column: "OgretimElemaniID");

            migrationBuilder.CreateIndex(
                name: "IX_Sinav_SinavTuruID",
                table: "Sinav",
                column: "SinavTuruID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Danismanlik");

            migrationBuilder.DropTable(
                name: "Degerlendirme");

            migrationBuilder.DropTable(
                name: "DersAlma");

            migrationBuilder.DropTable(
                name: "DersProgrami");

            migrationBuilder.DropTable(
                name: "Sinav");

            migrationBuilder.DropTable(
                name: "DersDurum");

            migrationBuilder.DropTable(
                name: "Ogrenci");

            migrationBuilder.DropTable(
                name: "Gun");

            migrationBuilder.DropTable(
                name: "DersAcma");

            migrationBuilder.DropTable(
                name: "Derslik");

            migrationBuilder.DropTable(
                name: "SinavTuru");

            migrationBuilder.DropTable(
                name: "OgrenciDurum");

            migrationBuilder.DropTable(
                name: "Mufredat");

            migrationBuilder.DropTable(
                name: "OgretimElemani");

            migrationBuilder.DropTable(
                name: "DerslikTuru");

            migrationBuilder.DropTable(
                name: "AkademikDonem");

            migrationBuilder.DropTable(
                name: "AkademikYil");

            migrationBuilder.DropTable(
                name: "DersHavuzu");

            migrationBuilder.DropTable(
                name: "Bolum");

            migrationBuilder.DropTable(
                name: "Cinsiyet");

            migrationBuilder.DropTable(
                name: "Kullanici");

            migrationBuilder.DropTable(
                name: "Unvan");

            migrationBuilder.DropTable(
                name: "DersSeviyesi");

            migrationBuilder.DropTable(
                name: "DersTuru");

            migrationBuilder.DropTable(
                name: "Dil");

            migrationBuilder.DropTable(
                name: "OgretimTuru");

            migrationBuilder.DropTable(
                name: "ProgramTuru");

            migrationBuilder.DropTable(
                name: "KullaniciTuru");
        }
    }
}
