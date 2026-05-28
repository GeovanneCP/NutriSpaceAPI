using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriSpaceAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_NS_ATRONAUTA",
                columns: table => new
                {
                    id_astronauta = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    cargo = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "NVARCHAR2(60)", maxLength: 60, nullable: false),
                    senha = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_NS_ATRONAUTA", x => x.id_astronauta);
                });

            migrationBuilder.CreateTable(
                name: "TB_NS_PLANTA",
                columns: table => new
                {
                    id_planta = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome_planta = table.Column<string>(type: "NVARCHAR2(80)", maxLength: 80, nullable: false),
                    temp_min_ideal = table.Column<decimal>(type: "DECIMAL(5,2)", precision: 5, scale: 2, nullable: false),
                    temp_max_ideal = table.Column<decimal>(type: "DECIMAL(5,2)", precision: 5, scale: 2, nullable: false),
                    umi_min_ideal = table.Column<decimal>(type: "DECIMAL(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_NS_PLANTA", x => x.id_planta);
                });

            migrationBuilder.CreateTable(
                name: "TB_NS_ESTUFA",
                columns: table => new
                {
                    id_estufa = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    id_astronauta = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_planta = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    nome_estufa = table.Column<string>(type: "NVARCHAR2(60)", maxLength: 60, nullable: false),
                    status_bomba = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    AstronautaIdAstronauta = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    PlantaIdPlanta = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_NS_ESTUFA", x => x.id_estufa);
                    table.ForeignKey(
                        name: "FK_TB_NS_ESTUFA_TB_NS_ATRONAUTA_AstronautaIdAstronauta",
                        column: x => x.AstronautaIdAstronauta,
                        principalTable: "TB_NS_ATRONAUTA",
                        principalColumn: "id_astronauta");
                    table.ForeignKey(
                        name: "FK_TB_NS_ESTUFA_TB_NS_PLANTA_PlantaIdPlanta",
                        column: x => x.PlantaIdPlanta,
                        principalTable: "TB_NS_PLANTA",
                        principalColumn: "id_planta");
                });

            migrationBuilder.CreateTable(
                name: "TB_NS_LEITURA_SENSOR",
                columns: table => new
                {
                    id_leitor = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    id_estufa = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    temperatura_lida = table.Column<decimal>(type: "DECIMAL(5,2)", precision: 5, scale: 2, nullable: false),
                    umidade_lida = table.Column<decimal>(type: "DECIMAL(5,2)", precision: 5, scale: 2, nullable: false),
                    dt_hr_leitura = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_NS_LEITURA_SENSOR", x => x.id_leitor);
                    table.ForeignKey(
                        name: "FK_TB_NS_LEITURA_SENSOR_TB_NS_ESTUFA_id_estufa",
                        column: x => x.id_estufa,
                        principalTable: "TB_NS_ESTUFA",
                        principalColumn: "id_estufa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_NS_ESTUFA_AstronautaIdAstronauta",
                table: "TB_NS_ESTUFA",
                column: "AstronautaIdAstronauta");

            migrationBuilder.CreateIndex(
                name: "IX_TB_NS_ESTUFA_PlantaIdPlanta",
                table: "TB_NS_ESTUFA",
                column: "PlantaIdPlanta");

            migrationBuilder.CreateIndex(
                name: "IX_TB_NS_LEITURA_SENSOR_id_estufa",
                table: "TB_NS_LEITURA_SENSOR",
                column: "id_estufa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_NS_LEITURA_SENSOR");

            migrationBuilder.DropTable(
                name: "TB_NS_ESTUFA");

            migrationBuilder.DropTable(
                name: "TB_NS_ATRONAUTA");

            migrationBuilder.DropTable(
                name: "TB_NS_PLANTA");
        }
    }
}
