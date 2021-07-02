using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio.Data.Migrations
{
    public partial class AddOneonOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Certificates_CertificateId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Universities_UniversityId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Organizations_OrganizationId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CertificateId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "Positions",
                newName: "SectorId");

            migrationBuilder.RenameIndex(
                name: "IX_Positions_OrganizationId",
                table: "Positions",
                newName: "IX_Positions_SectorId");

            migrationBuilder.RenameColumn(
                name: "UniversityId",
                table: "Courses",
                newName: "SpecialtyId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_UniversityId",
                table: "Courses",
                newName: "IX_Courses_SpecialtyId");

            migrationBuilder.AlterColumn<string>(
                name: "CertificateId",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseId",
                table: "Certificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Courses_Id",
                table: "Certificates",
                column: "Id",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Specialties_SpecialtyId",
                table: "Courses",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Sectors_SectorId",
                table: "Positions",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Courses_Id",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Specialties_SpecialtyId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Sectors_SectorId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Certificates");

            migrationBuilder.RenameColumn(
                name: "SectorId",
                table: "Positions",
                newName: "OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Positions_SectorId",
                table: "Positions",
                newName: "IX_Positions_OrganizationId");

            migrationBuilder.RenameColumn(
                name: "SpecialtyId",
                table: "Courses",
                newName: "UniversityId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_SpecialtyId",
                table: "Courses",
                newName: "IX_Courses_UniversityId");

            migrationBuilder.AlterColumn<string>(
                name: "CertificateId",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CertificateId",
                table: "Courses",
                column: "CertificateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Certificates_CertificateId",
                table: "Courses",
                column: "CertificateId",
                principalTable: "Certificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Universities_UniversityId",
                table: "Courses",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Organizations_OrganizationId",
                table: "Positions",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
