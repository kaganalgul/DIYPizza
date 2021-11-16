using Microsoft.EntityFrameworkCore.Migrations;

namespace DIYPizza.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Malzemeler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Malzemeler", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Malzemeler",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "Salam" },
                    { 17, "Tavuk" },
                    { 16, "Soğan" },
                    { 15, "Kaşar Peyniri" },
                    { 14, "Rokfor Peyniri" },
                    { 13, "Ton Balığı" },
                    { 12, "Ananas" },
                    { 11, "Kaburga Füme" },
                    { 18, "Parmesan Peyniri" },
                    { 10, "Pastırma" },
                    { 8, "Biber" },
                    { 7, "Sosis" },
                    { 6, "Zeytin" },
                    { 5, "Mısır" },
                    { 4, "Pizza Sosu" },
                    { 3, "Mozzarella Peyniri" },
                    { 2, "Sucuk" },
                    { 9, "Mantar" },
                    { 19, "Domates" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Malzemeler");
        }
    }
}
