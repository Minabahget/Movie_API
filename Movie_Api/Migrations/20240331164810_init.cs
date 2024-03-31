using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Movie_Api.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Duration = table.Column<TimeOnly>(type: "time", nullable: false),
                    Rate = table.Column<float>(type: "real", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieCategory",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCategory", x => new { x.MovieId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_MovieCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieCategory_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Movies filled with action-packed scenes.", "Action" },
                    { 2, "Movies intended to make the audience laugh.", "Comedy" },
                    { 3, "Movies that focus on emotional themes.", "Drama" },
                    { 4, "Movies filled with adventurous journeys and exploration.", "Adventure" },
                    { 5, "Movies based on historical events or settings.", "Historical" },
                    { 6, "Movies created using animation techniques.", "Animation" },
                    { 7, "Movies that explore speculative or futuristic concepts.", "Science Fiction" }
                });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "CreatedDate", "Description", "Duration", "ImagePath", "Rate", "Title" },
                values: new object[,]
                {
                    { 1, new DateOnly(2008, 7, 18), "A vigilante known as Batman sets out to combat the injustices of Gotham City.", new TimeOnly(2, 32, 0), "uploads\\movie1.jpg", 9f, "The last stand" },
                    { 2, new DateOnly(2010, 7, 16), "A thief who enters the dreams of others to steal secrets from their subconscious.", new TimeOnly(2, 28, 0), "uploads\\movie2.jpg", 8.8f, "Spider man 2" },
                    { 3, new DateOnly(1994, 10, 14), "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", new TimeOnly(2, 22, 0), "uploads\\movie3.jpg", 9.3f, "Spider man 3" },
                    { 4, new DateOnly(2008, 12, 25), "A group of German officers plot to assassinate Adolf Hitler during World War II.", new TimeOnly(2, 1, 0), "uploads\\movie4.jpg", 8.1f, "valkyrie" },
                    { 5, new DateOnly(2000, 5, 5), "A former Roman General sets out to exact vengeance against the corrupt emperor who murdered his family and sent him into slavery.", new TimeOnly(2, 35, 0), "uploads\\movie5.jpg", 8.5f, "Gladiator" },
                    { 6, new DateOnly(2002, 3, 15), "Set during the Ice Age, a sabertooth tiger, a sloth, and a wooly mammoth find a lost human infant, and they try to return him to his tribe.", new TimeOnly(1, 21, 0), "uploads\\movie6.jpg", 7.5f, "Ice age" },
                    { 7, new DateOnly(2007, 7, 3), "An ancient struggle between two Cybertronian races, the heroic Autobots and the evil Decepticons, comes to Earth, with a clue to the ultimate power held by a teenager.", new TimeOnly(2, 24, 0), "uploads\\movie7.jpg", 7.1f, "Transformers" },
                    { 8, new DateOnly(2000, 7, 14), "Two mutants come to a private academy for their kind whose resident superhero team must oppose a terrorist organization with similar powers.", new TimeOnly(1, 44, 0), "uploads\\movie8.jpg", 7.4f, "X-men" }
                });

            migrationBuilder.InsertData(
                table: "MovieCategory",
                columns: new[] { "CategoryId", "MovieId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 1, 4 },
                    { 5, 4 },
                    { 1, 5 },
                    { 5, 5 },
                    { 6, 6 },
                    { 7, 6 },
                    { 7, 7 },
                    { 1, 8 },
                    { 7, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_Name",
                table: "Category",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieCategory_CategoryId",
                table: "MovieCategory",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieCategory");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Movie");
        }
    }
}
