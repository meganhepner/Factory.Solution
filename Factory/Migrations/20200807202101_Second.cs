using Microsoft.EntityFrameworkCore.Migrations;

namespace Factory.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngineerMachine_Engineers_EngineerId",
                table: "EngineerMachine");

            migrationBuilder.DropForeignKey(
                name: "FK_EngineerMachine_Machines_MachineId",
                table: "EngineerMachine");

            migrationBuilder.AlterColumn<int>(
                name: "MachineId",
                table: "EngineerMachine",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EngineerId",
                table: "EngineerMachine",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EngineerMachine_Engineers_EngineerId",
                table: "EngineerMachine",
                column: "EngineerId",
                principalTable: "Engineers",
                principalColumn: "EngineerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EngineerMachine_Machines_MachineId",
                table: "EngineerMachine",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "MachineId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngineerMachine_Engineers_EngineerId",
                table: "EngineerMachine");

            migrationBuilder.DropForeignKey(
                name: "FK_EngineerMachine_Machines_MachineId",
                table: "EngineerMachine");

            migrationBuilder.AlterColumn<int>(
                name: "MachineId",
                table: "EngineerMachine",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EngineerId",
                table: "EngineerMachine",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_EngineerMachine_Engineers_EngineerId",
                table: "EngineerMachine",
                column: "EngineerId",
                principalTable: "Engineers",
                principalColumn: "EngineerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EngineerMachine_Machines_MachineId",
                table: "EngineerMachine",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "MachineId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
