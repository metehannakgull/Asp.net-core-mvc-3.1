using Microsoft.EntityFrameworkCore.Migrations;

namespace Blogum2.Data.Migrations
{
    public partial class ilk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ilce",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlceAd = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilce", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KampYeri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KampYeriAd = table.Column<string>(nullable: true),
                    Resim = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KampYeri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SehirdekiYer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SehirdekiYerAd = table.Column<string>(nullable: true),
                    Resim = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SehirdekiYer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tur",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeziTuru = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tur", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Il",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlAd = table.Column<int>(nullable: false),
                    IlceId = table.Column<int>(nullable: false),
                    TurId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Il", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Il_Ilce_IlceId",
                        column: x => x.IlceId,
                        principalTable: "Ilce",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Il_Tur_TurId",
                        column: x => x.TurId,
                        principalTable: "Tur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IlKamp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlId = table.Column<int>(nullable: false),
                    KampYeriId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IlKamp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IlKamp_Il_IlId",
                        column: x => x.IlId,
                        principalTable: "Il",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IlKamp_KampYeri_KampYeriId",
                        column: x => x.KampYeriId,
                        principalTable: "KampYeri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IlSehirYeri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlId = table.Column<int>(nullable: false),
                    SehirdekiYerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IlSehirYeri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IlSehirYeri_Il_IlId",
                        column: x => x.IlId,
                        principalTable: "Il",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IlSehirYeri_SehirdekiYer_SehirdekiYerId",
                        column: x => x.SehirdekiYerId,
                        principalTable: "SehirdekiYer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Il_IlceId",
                table: "Il",
                column: "IlceId");

            migrationBuilder.CreateIndex(
                name: "IX_Il_TurId",
                table: "Il",
                column: "TurId");

            migrationBuilder.CreateIndex(
                name: "IX_IlKamp_IlId",
                table: "IlKamp",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_IlKamp_KampYeriId",
                table: "IlKamp",
                column: "KampYeriId");

            migrationBuilder.CreateIndex(
                name: "IX_IlSehirYeri_IlId",
                table: "IlSehirYeri",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_IlSehirYeri_SehirdekiYerId",
                table: "IlSehirYeri",
                column: "SehirdekiYerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IlKamp");

            migrationBuilder.DropTable(
                name: "IlSehirYeri");

            migrationBuilder.DropTable(
                name: "KampYeri");

            migrationBuilder.DropTable(
                name: "Il");

            migrationBuilder.DropTable(
                name: "SehirdekiYer");

            migrationBuilder.DropTable(
                name: "Ilce");

            migrationBuilder.DropTable(
                name: "Tur");
        }
    }
}
