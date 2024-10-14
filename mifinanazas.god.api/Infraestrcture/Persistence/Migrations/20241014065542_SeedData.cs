using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mifinanazas.God.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "MoveOptions",
            columns: new[] { "description", "killId" },
            values: new object[,]
            {
                { "Rock", 3 },
                { "Paper", 1 },
                { "Scissors", 2 }
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
