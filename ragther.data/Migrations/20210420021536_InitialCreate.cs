using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ragther.data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FriendshipCondition",
                columns: table => new
                {
                    ConditionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendshipCondition", x => x.ConditionId);
                });

            migrationBuilder.CreateTable(
                name: "NoticeType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoticeType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoCondition",
                columns: table => new
                {
                    ConditionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoCondition", x => x.ConditionId);
                });

            migrationBuilder.CreateTable(
                name: "TokenCondition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenCondition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 22, nullable: false),
                    FirstName = table.Column<string>(maxLength: 25, nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Friendship",
                columns: table => new
                {
                    FriendshipKey = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SenderUserId = table.Column<int>(nullable: false),
                    RecipientUserId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    FriendshipConditionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendship", x => x.FriendshipKey);
                    table.ForeignKey(
                        name: "FK_Friendship_FriendshipCondition_FriendshipConditionId",
                        column: x => x.FriendshipConditionId,
                        principalTable: "FriendshipCondition",
                        principalColumn: "ConditionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Friendship_Users_RecipientUserId",
                        column: x => x.RecipientUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Friendship_Users_SenderUserId",
                        column: x => x.SenderUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MailUpdate",
                columns: table => new
                {
                    MailUpdateId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Token = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    MailWillUpdate = table.Column<string>(nullable: true),
                    TokenConditionId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailUpdate", x => x.MailUpdateId);
                    table.ForeignKey(
                        name: "FK_MailUpdate_TokenCondition_TokenConditionId",
                        column: x => x.TokenConditionId,
                        principalTable: "TokenCondition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MailUpdate_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notice",
                columns: table => new
                {
                    NoticeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NoticeTypeId = table.Column<int>(nullable: false),
                    OwnerUserId = table.Column<int>(nullable: false),
                    RelevantUserId = table.Column<int>(nullable: false),
                    RelevantURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notice", x => x.NoticeId);
                    table.ForeignKey(
                        name: "FK_Notice_NoticeType_NoticeTypeId",
                        column: x => x.NoticeTypeId,
                        principalTable: "NoticeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notice_Users_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notice_Users_RelevantUserId",
                        column: x => x.RelevantUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileDetail",
                columns: table => new
                {
                    ProfileDetailId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ProfileDescription = table.Column<string>(maxLength: 400, nullable: true),
                    HiddenProfileDescription = table.Column<string>(nullable: true),
                    IsHiddenProfile = table.Column<bool>(nullable: false),
                    ProfileScore = table.Column<int>(nullable: false),
                    FriendCount = table.Column<int>(nullable: false),
                    HelpCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileDetail", x => x.ProfileDetailId);
                    table.ForeignKey(
                        name: "FK_ProfileDetail_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CreatorUserId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagId);
                    table.ForeignKey(
                        name: "FK_Tag_Users_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    TodoId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatorUserId = table.Column<int>(nullable: false),
                    imageUrl = table.Column<string>(nullable: true),
                    UntilWhen = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    TodoConditionId = table.Column<int>(nullable: false),
                    LikeCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.TodoId);
                    table.ForeignKey(
                        name: "FK_Todos_Users_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Todos_TodoCondition_TodoConditionId",
                        column: x => x.TodoConditionId,
                        principalTable: "TodoCondition",
                        principalColumn: "ConditionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagsOfInterest",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagsOfInterest", x => new { x.TagId, x.UserId });
                    table.ForeignKey(
                        name: "FK_TagsOfInterest_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagsOfInterest_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParentCommentId = table.Column<int>(nullable: true),
                    TodoId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    IsOffer = table.Column<bool>(nullable: false),
                    Content = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comment_Comment_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "Comment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_Todos_TodoId",
                        column: x => x.TodoId,
                        principalTable: "Todos",
                        principalColumn: "TodoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    TodoId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    LikeDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => new { x.TodoId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Like_Todos_TodoId",
                        column: x => x.TodoId,
                        principalTable: "Todos",
                        principalColumn: "TodoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Like_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Remind",
                columns: table => new
                {
                    TodoId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    RemindDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remind", x => new { x.TodoId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Remind_Todos_TodoId",
                        column: x => x.TodoId,
                        principalTable: "Todos",
                        principalColumn: "TodoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Remind_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TodoTag",
                columns: table => new
                {
                    TodoId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoTag", x => new { x.TodoId, x.TagId });
                    table.ForeignKey(
                        name: "FK_TodoTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TodoTag_Todos_TodoId",
                        column: x => x.TodoId,
                        principalTable: "Todos",
                        principalColumn: "TodoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkWith",
                columns: table => new
                {
                    TodoId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkWith", x => new { x.TodoId, x.UserId });
                    table.ForeignKey(
                        name: "FK_WorkWith_Todos_TodoId",
                        column: x => x.TodoId,
                        principalTable: "Todos",
                        principalColumn: "TodoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkWith_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ParentCommentId",
                table: "Comment",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_TodoId",
                table: "Comment",
                column: "TodoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_FriendshipConditionId",
                table: "Friendship",
                column: "FriendshipConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_RecipientUserId",
                table: "Friendship",
                column: "RecipientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_SenderUserId",
                table: "Friendship",
                column: "SenderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_UserId",
                table: "Like",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MailUpdate_TokenConditionId",
                table: "MailUpdate",
                column: "TokenConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_MailUpdate_UserId",
                table: "MailUpdate",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notice_NoticeTypeId",
                table: "Notice",
                column: "NoticeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notice_OwnerUserId",
                table: "Notice",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notice_RelevantUserId",
                table: "Notice",
                column: "RelevantUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileDetail_UserId",
                table: "ProfileDetail",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Remind_UserId",
                table: "Remind",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_CreatorUserId",
                table: "Tag",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TagsOfInterest_UserId",
                table: "TagsOfInterest",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_CreatorUserId",
                table: "Todos",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_TodoConditionId",
                table: "Todos",
                column: "TodoConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoTag_TagId",
                table: "TodoTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkWith_UserId",
                table: "WorkWith",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Friendship");

            migrationBuilder.DropTable(
                name: "Like");

            migrationBuilder.DropTable(
                name: "MailUpdate");

            migrationBuilder.DropTable(
                name: "Notice");

            migrationBuilder.DropTable(
                name: "ProfileDetail");

            migrationBuilder.DropTable(
                name: "Remind");

            migrationBuilder.DropTable(
                name: "TagsOfInterest");

            migrationBuilder.DropTable(
                name: "TodoTag");

            migrationBuilder.DropTable(
                name: "WorkWith");

            migrationBuilder.DropTable(
                name: "FriendshipCondition");

            migrationBuilder.DropTable(
                name: "TokenCondition");

            migrationBuilder.DropTable(
                name: "NoticeType");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "TodoCondition");
        }
    }
}
