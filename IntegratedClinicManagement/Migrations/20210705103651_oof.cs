using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicManagementProject.Migrations
{
    public partial class oof : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Admin_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Admin_Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Doctor_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Doctor_Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Patient_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Patient_Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsultationDetails",
                columns: table => new
                {
                    Consultation_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Patient_Id = table.Column<int>(type: "int", nullable: false),
                    Doctor_Id = table.Column<int>(type: "int", nullable: false),
                    Timeslot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consultation_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Consultation_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consultation_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bill = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultationDetails", x => x.Consultation_Id);
                    table.ForeignKey(
                        name: "FK_ConsultationDetails_Doctors_Doctor_Id",
                        column: x => x.Doctor_Id,
                        principalTable: "Doctors",
                        principalColumn: "Doctor_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsultationDetails_Patients_Patient_Id",
                        column: x => x.Patient_Id,
                        principalTable: "Patients",
                        principalColumn: "Patient_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorSchedules",
                columns: table => new
                {
                    Timeslot_Id = table.Column<int>(type: "int", nullable: false),
                    Doctor_Id = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patient_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedules", x => new { x.Doctor_Id, x.Timeslot_Id });
                    table.ForeignKey(
                        name: "FK_DoctorSchedules_Doctors_Doctor_Id",
                        column: x => x.Doctor_Id,
                        principalTable: "Doctors",
                        principalColumn: "Doctor_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorSchedules_Patients_Patient_Id",
                        column: x => x.Patient_Id,
                        principalTable: "Patients",
                        principalColumn: "Patient_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Admin_Id", "Name", "Password", "PasswordSalt", "Username" },
                values: new object[] { 1, "MrTestAdmin", new byte[] { 0 }, new byte[] { 0 }, "testadmin" });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Doctor_Id", "Age", "Gender", "Name", "Password", "PasswordSalt", "Phone", "Specialization", "Username" },
                values: new object[,]
                {
                    { 1, 30, "Male", "TimDoc", new byte[] { 0 }, new byte[] { 0 }, "323524523", "Childcare", "docabc" },
                    { 2, 30, "Male", "TiDoc", new byte[] { 0, 0 }, new byte[] { 0, 0 }, "323524523", "Oncology", "docoof" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Patient_Id", "Age", "Gender", "Name", "Password", "PasswordSalt", "Phone", "Username" },
                values: new object[] { 1, 30, "Male", "Tim", new byte[] { 0 }, new byte[] { 0 }, "32423434", "abc" });

            migrationBuilder.InsertData(
                table: "DoctorSchedules",
                columns: new[] { "Doctor_Id", "Timeslot_Id", "Patient_Id", "Time" },
                values: new object[,]
                {
                    { 1, 1, null, "930-1030" },
                    { 1, 2, null, "1330-1430" },
                    { 2, 1, null, "930-1030" },
                    { 2, 2, null, "1330-1430" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationDetails_Doctor_Id",
                table: "ConsultationDetails",
                column: "Doctor_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationDetails_Patient_Id",
                table: "ConsultationDetails",
                column: "Patient_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_Patient_Id",
                table: "DoctorSchedules",
                column: "Patient_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "ConsultationDetails");

            migrationBuilder.DropTable(
                name: "DoctorSchedules");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
