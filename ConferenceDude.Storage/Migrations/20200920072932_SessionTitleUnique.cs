namespace ConferenceDude.Storage.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class SessionTitleUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Sessions_Title",
                table: "Sessions",
                column: "Title",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sessions_Title",
                table: "Sessions");
        }
    }
}
