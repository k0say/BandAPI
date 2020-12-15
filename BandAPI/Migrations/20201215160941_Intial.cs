using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BandAPI.Migrations
{
    public partial class Intial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bands",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Founded = table.Column<DateTime>(nullable: false),
                    MainGenre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 400, nullable: true),
                    BandId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albums_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bands",
                columns: new[] { "Id", "Founded", "MainGenre", "Name" },
                values: new object[,]
                {
                    { new Guid("0f3e64f2-0bd4-9431-6024-8abd724f5377"), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Farley, Aspen V.", "Hunter Rivera" },
                    { new Guid("f5e1191d-b972-b37d-64b2-6e93d27da92e"), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Huff, Darius M.", "Burton Chambers" },
                    { new Guid("22c52ad2-ae11-036f-5d51-83369aa777bb"), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Whitney, Jena O.", "Talon Bailey" },
                    { new Guid("0e66ea98-0bc8-214f-f2b4-4a36a4d5329f"), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Daniels, Melvin Y.", "Gage Gross" },
                    { new Guid("9f5d793b-1eb9-baca-281c-50af5e5ce583"), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Huber, Addison M.", "Lars Patel" },
                    { new Guid("ad706a51-acb1-006e-26a3-6345f47a66df"), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Russo, Cruz Q.", "Ethan Bryan" },
                    { new Guid("b5b8c87f-ec11-2f49-c9ed-6a916f7da923"), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elliott, Harrison R.", "Cole Pickett" },
                    { new Guid("7697d932-e00d-f9e5-ecc0-013e68259504"), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cleveland, Rooney F.", "Abdul Mclean" },
                    { new Guid("a695c383-f13c-4013-3d54-724bf36283f4"), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bridges, Mark D.", "Fletcher England" }
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "BandId", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("ade75b2b-78f9-4039-b07f-ee7dfb98d52d"), new Guid("a695c383-f13c-4013-3d54-724bf36283f4"), "Awesome debut album AHAHHSHD", "Burton Chambers" },
                    { new Guid("d03563f8-9009-409e-98dc-2b68394353bc"), new Guid("a695c383-f13c-4013-3d54-724bf36283f4"), "Awesome debut album AHAHHSHD", "Burton Chambers" },
                    { new Guid("5ce28d03-3254-4fdb-bbd7-414825b99fe4"), new Guid("a695c383-f13c-4013-3d54-724bf36283f4"), "Awesome debut album AHAHHSHD", "Burton Chambers" },
                    { new Guid("71d4dcec-b4fe-4600-92ed-cc8fa6145645"), new Guid("a695c383-f13c-4013-3d54-724bf36283f4"), "Awesome debut album AHAHHSHD", "Burton Chambers" },
                    { new Guid("ad706a51-acb1-006e-26a3-6345f47a66df"), new Guid("a695c383-f13c-4013-3d54-724bf36283f4"), "Awesome debut album AHAHHSHD", "Burton Chambers" },
                    { new Guid("05b623b5-5b97-49d3-9808-5ee0af52c4aa"), new Guid("a695c383-f13c-4013-3d54-724bf36283f4"), "Awesome debut album AHAHHSHD", "Burton Chambers" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_BandId",
                table: "Albums",
                column: "BandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Bands");
        }
    }
}
