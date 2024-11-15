using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PeluqueriaApi.Migrations
{
    /// <inheritdoc />
    public partial class conection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Existencias = table.Column<int>(type: "int", nullable: false),
                    Url_imagen = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    apelNom = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RolUsers",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolUsers", x => new { x.RolId, x.UserId });
                    table.ForeignKey(
                        name: "FK_RolUsers_Rol_RolId",
                        column: x => x.RolId,
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FH_turno = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Metodo_pago = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ServicioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Turnos_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turnos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "id", "Categoria", "Descripcion", "Existencias", "Marca", "Nombre", "Precio", "Url_imagen" },
                values: new object[,]
                {
                    { 1, "Herramientas", "Tijeras de alta precisión para cortes profesionales.", 15, "ProCut", "Tijeras Profesionales", 2599.00m, "https://http2.mlstatic.com/D_NQ_NP_985742-MLA45241256474_032021-O.webp" },
                    { 2, "Estilizado", "Cera moldeadora para un acabado natural.", 20, "HairStyle", "Cera para Cabello", 1099.00m, "https://legioners.com.ar/wp-content/uploads/2022/01/fiber-wax-100-3.jpg" },
                    { 3, "Estilizado", "Gel fijador de alta resistencia para peinados duraderos.", 30, "StrongHold", "Gel Fijador", 899.00m, "https://simplicityar.vtexassets.com/arquivos/ids/169941-800-auto?v=638288421490970000&width=800&height=auto&aspect=true" }
                });

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Mod" },
                    { 2, "Admin" },
                    { 3, "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolUsers_UserId",
                table: "RolUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_ServicioId",
                table: "Turnos",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_UserId",
                table: "Turnos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_userName",
                table: "Users",
                column: "userName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "RolUsers");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
