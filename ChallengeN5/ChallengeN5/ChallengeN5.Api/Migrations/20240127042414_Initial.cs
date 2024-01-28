using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChallengeN5.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    EmpleadoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmpleadoApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmepleadoDocidentidad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmpleadoPuesto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmpleadoDepartamento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmpleadoFechaInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Audit_fec_creacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Audit_fec_modificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Audit_user_creacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Audit_user_modificacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.EmpleadoID);
                });

            migrationBuilder.CreateTable(
                name: "TipoPermisos",
                columns: table => new
                {
                    TipoPermisoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoPermisoDescripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoPermisoObservacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Audit_fec_creacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Audit_fec_modificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Audit_user_creacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Audit_user_modificacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPermisos", x => x.TipoPermisoID);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    PermisoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoID = table.Column<int>(type: "int", nullable: false),
                    TipoPermisoID = table.Column<int>(type: "int", nullable: false),
                    PermisoDescripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PermisoActivo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PermisoFechaInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PermisoFechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Audit_fec_creacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Audit_fec_modificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Audit_user_creacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Audit_user_modificacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.PermisoID);
                    table.ForeignKey(
                        name: "FK_Permisos_Empleados_EmpleadoID",
                        column: x => x.EmpleadoID,
                        principalTable: "Empleados",
                        principalColumn: "EmpleadoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permisos_TipoPermisos_TipoPermisoID",
                        column: x => x.TipoPermisoID,
                        principalTable: "TipoPermisos",
                        principalColumn: "TipoPermisoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_EmepleadoDocidentidad",
                table: "Empleados",
                column: "EmepleadoDocidentidad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_EmpleadoID",
                table: "Permisos",
                column: "EmpleadoID");

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_TipoPermisoID",
                table: "Permisos",
                column: "TipoPermisoID");

            migrationBuilder.CreateIndex(
                name: "IX_TipoPermisos_TipoPermisoDescripcion",
                table: "TipoPermisos",
                column: "TipoPermisoDescripcion",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "TipoPermisos");
        }
    }
}
