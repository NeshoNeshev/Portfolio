using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio.Data.Migrations
{
    public partial class ChangeDataBaseOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_ProfessionalExperiences_ProfessionalExperienceId",
                table: "Organizations");

            migrationBuilder.DropTable(
                name: "ProfessionalExperiences");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.RenameColumn(
                name: "ProfessionalExperienceId",
                table: "Organizations",
                newName: "PrivateInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_Organizations_ProfessionalExperienceId",
                table: "Organizations",
                newName: "IX_Organizations_PrivateInformationId");

            migrationBuilder.AddColumn<string>(
                name: "Period",
                table: "Positions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_PrivateInformations_PrivateInformationId",
                table: "Organizations",
                column: "PrivateInformationId",
                principalTable: "PrivateInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_PrivateInformations_PrivateInformationId",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Period",
                table: "Positions");

            migrationBuilder.RenameColumn(
                name: "PrivateInformationId",
                table: "Organizations",
                newName: "ProfessionalExperienceId");

            migrationBuilder.RenameIndex(
                name: "IX_Organizations_PrivateInformationId",
                table: "Organizations",
                newName: "IX_Organizations_ProfessionalExperienceId");

            migrationBuilder.CreateTable(
                name: "ProfessionalExperiences",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrivateInformationId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalExperiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfessionalExperiences_PrivateInformations_PrivateInformationId",
                        column: x => x.PrivateInformationId,
                        principalTable: "PrivateInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalExperiences_IsDeleted",
                table: "ProfessionalExperiences",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalExperiences_PrivateInformationId",
                table: "ProfessionalExperiences",
                column: "PrivateInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_IsDeleted",
                table: "Settings",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_ProfessionalExperiences_ProfessionalExperienceId",
                table: "Organizations",
                column: "ProfessionalExperienceId",
                principalTable: "ProfessionalExperiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
