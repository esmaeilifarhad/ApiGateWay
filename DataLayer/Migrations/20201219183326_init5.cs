using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserId",
                table: "UserRole");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserRole",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "UserRole",
                newName: "roleId");

            migrationBuilder.RenameColumn(
                name: "UserRoleId",
                table: "UserRole",
                newName: "userRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                newName: "IX_UserRole_userId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                newName: "IX_UserRole_roleId");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "User",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "User",
                newName: "token");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "User",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "User",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "User",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "User",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Student",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Student",
                newName: "studentId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Role",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Role",
                newName: "roleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_roleId",
                table: "UserRole",
                column: "roleId",
                principalTable: "Role",
                principalColumn: "roleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_userId",
                table: "UserRole",
                column: "userId",
                principalTable: "User",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_roleId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_userId",
                table: "UserRole");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "UserRole",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "roleId",
                table: "UserRole",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "userRoleId",
                table: "UserRole",
                newName: "UserRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_userId",
                table: "UserRole",
                newName: "IX_UserRole_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_roleId",
                table: "UserRole",
                newName: "IX_UserRole_RoleId");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "User",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "token",
                table: "User",
                newName: "Token");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "User",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "User",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "User",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "User",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Student",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "studentId",
                table: "Student",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Role",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "roleId",
                table: "Role",
                newName: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
