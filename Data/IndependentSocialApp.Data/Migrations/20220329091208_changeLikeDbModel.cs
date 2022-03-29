using Microsoft.EntityFrameworkCore.Migrations;

namespace IndependentSocialApp.Data.Migrations
{
    public partial class changeLikeDbModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsLike",
                table: "Likes",
                newName: "IsPostLike");

            migrationBuilder.AddColumn<bool>(
                name: "IsCommentLike",
                table: "Likes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCommentLike",
                table: "Likes");

            migrationBuilder.RenameColumn(
                name: "IsPostLike",
                table: "Likes",
                newName: "IsLike");
        }
    }
}
