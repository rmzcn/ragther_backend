using Microsoft.EntityFrameworkCore.Migrations;

namespace ragther.data.Migrations
{
    public partial class DbContextUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Comment_ParentCommentId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Todos_TodoId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Users_UserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_FriendshipCondition_FriendshipConditionId",
                table: "Friendship");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_Users_RecipientUserId",
                table: "Friendship");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_Users_SenderUserId",
                table: "Friendship");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_Todos_TodoId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_Users_UserId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_MailUpdate_TokenCondition_TokenConditionId",
                table: "MailUpdate");

            migrationBuilder.DropForeignKey(
                name: "FK_MailUpdate_Users_UserId",
                table: "MailUpdate");

            migrationBuilder.DropForeignKey(
                name: "FK_Notice_NoticeType_NoticeTypeId",
                table: "Notice");

            migrationBuilder.DropForeignKey(
                name: "FK_Notice_Users_OwnerUserId",
                table: "Notice");

            migrationBuilder.DropForeignKey(
                name: "FK_Notice_Users_RelevantUserId",
                table: "Notice");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileDetail_Users_UserId",
                table: "ProfileDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Remind_Todos_TodoId",
                table: "Remind");

            migrationBuilder.DropForeignKey(
                name: "FK_Remind_Users_UserId",
                table: "Remind");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Users_CreatorUserId",
                table: "Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_TagsOfInterest_Tag_TagId",
                table: "TagsOfInterest");

            migrationBuilder.DropForeignKey(
                name: "FK_TagsOfInterest_Users_UserId",
                table: "TagsOfInterest");

            migrationBuilder.DropForeignKey(
                name: "FK_Todos_TodoCondition_TodoConditionId",
                table: "Todos");

            migrationBuilder.DropForeignKey(
                name: "FK_TodoTag_Tag_TagId",
                table: "TodoTag");

            migrationBuilder.DropForeignKey(
                name: "FK_TodoTag_Todos_TodoId",
                table: "TodoTag");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkWith_Todos_TodoId",
                table: "WorkWith");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkWith_Users_UserId",
                table: "WorkWith");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkWith",
                table: "WorkWith");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TokenCondition",
                table: "TokenCondition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoTag",
                table: "TodoTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoCondition",
                table: "TodoCondition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagsOfInterest",
                table: "TagsOfInterest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Remind",
                table: "Remind");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileDetail",
                table: "ProfileDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoticeType",
                table: "NoticeType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notice",
                table: "Notice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MailUpdate",
                table: "MailUpdate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Like",
                table: "Like");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendshipCondition",
                table: "FriendshipCondition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friendship",
                table: "Friendship");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "WorkWith",
                newName: "WorkWiths");

            migrationBuilder.RenameTable(
                name: "TokenCondition",
                newName: "TokenConditions");

            migrationBuilder.RenameTable(
                name: "TodoTag",
                newName: "TodoTags");

            migrationBuilder.RenameTable(
                name: "TodoCondition",
                newName: "TodoConditions");

            migrationBuilder.RenameTable(
                name: "TagsOfInterest",
                newName: "TagsOfInterests");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "Remind",
                newName: "Reminds");

            migrationBuilder.RenameTable(
                name: "ProfileDetail",
                newName: "ProfileDetails");

            migrationBuilder.RenameTable(
                name: "NoticeType",
                newName: "NoticeTypes");

            migrationBuilder.RenameTable(
                name: "Notice",
                newName: "Notices");

            migrationBuilder.RenameTable(
                name: "MailUpdate",
                newName: "MailUpdates");

            migrationBuilder.RenameTable(
                name: "Like",
                newName: "Likes");

            migrationBuilder.RenameTable(
                name: "FriendshipCondition",
                newName: "FriendshipConditions");

            migrationBuilder.RenameTable(
                name: "Friendship",
                newName: "Friendships");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_WorkWith_UserId",
                table: "WorkWiths",
                newName: "IX_WorkWiths_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TodoTag_TagId",
                table: "TodoTags",
                newName: "IX_TodoTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_TagsOfInterest_UserId",
                table: "TagsOfInterests",
                newName: "IX_TagsOfInterests_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tag_CreatorUserId",
                table: "Tags",
                newName: "IX_Tags_CreatorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Remind_UserId",
                table: "Reminds",
                newName: "IX_Reminds_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileDetail_UserId",
                table: "ProfileDetails",
                newName: "IX_ProfileDetails_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notice_RelevantUserId",
                table: "Notices",
                newName: "IX_Notices_RelevantUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notice_OwnerUserId",
                table: "Notices",
                newName: "IX_Notices_OwnerUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notice_NoticeTypeId",
                table: "Notices",
                newName: "IX_Notices_NoticeTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_MailUpdate_UserId",
                table: "MailUpdates",
                newName: "IX_MailUpdates_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MailUpdate_TokenConditionId",
                table: "MailUpdates",
                newName: "IX_MailUpdates_TokenConditionId");

            migrationBuilder.RenameIndex(
                name: "IX_Like_UserId",
                table: "Likes",
                newName: "IX_Likes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendship_SenderUserId",
                table: "Friendships",
                newName: "IX_Friendships_SenderUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendship_RecipientUserId",
                table: "Friendships",
                newName: "IX_Friendships_RecipientUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendship_FriendshipConditionId",
                table: "Friendships",
                newName: "IX_Friendships_FriendshipConditionId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UserId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_TodoId",
                table: "Comments",
                newName: "IX_Comments_TodoId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ParentCommentId",
                table: "Comments",
                newName: "IX_Comments_ParentCommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkWiths",
                table: "WorkWiths",
                columns: new[] { "TodoId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TokenConditions",
                table: "TokenConditions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoTags",
                table: "TodoTags",
                columns: new[] { "TodoId", "TagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoConditions",
                table: "TodoConditions",
                column: "ConditionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagsOfInterests",
                table: "TagsOfInterests",
                columns: new[] { "TagId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reminds",
                table: "Reminds",
                columns: new[] { "TodoId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileDetails",
                table: "ProfileDetails",
                column: "ProfileDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoticeTypes",
                table: "NoticeTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notices",
                table: "Notices",
                column: "NoticeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MailUpdates",
                table: "MailUpdates",
                column: "MailUpdateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                columns: new[] { "TodoId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendshipConditions",
                table: "FriendshipConditions",
                column: "ConditionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships",
                column: "FriendshipKey");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                table: "Comments",
                column: "ParentCommentId",
                principalTable: "Comments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Todos_TodoId",
                table: "Comments",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "TodoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_FriendshipConditions_FriendshipConditionId",
                table: "Friendships",
                column: "FriendshipConditionId",
                principalTable: "FriendshipConditions",
                principalColumn: "ConditionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Users_RecipientUserId",
                table: "Friendships",
                column: "RecipientUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Users_SenderUserId",
                table: "Friendships",
                column: "SenderUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Todos_TodoId",
                table: "Likes",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "TodoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MailUpdates_TokenConditions_TokenConditionId",
                table: "MailUpdates",
                column: "TokenConditionId",
                principalTable: "TokenConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MailUpdates_Users_UserId",
                table: "MailUpdates",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notices_NoticeTypes_NoticeTypeId",
                table: "Notices",
                column: "NoticeTypeId",
                principalTable: "NoticeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notices_Users_OwnerUserId",
                table: "Notices",
                column: "OwnerUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notices_Users_RelevantUserId",
                table: "Notices",
                column: "RelevantUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileDetails_Users_UserId",
                table: "ProfileDetails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reminds_Todos_TodoId",
                table: "Reminds",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "TodoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reminds_Users_UserId",
                table: "Reminds",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Users_CreatorUserId",
                table: "Tags",
                column: "CreatorUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagsOfInterests_Tags_TagId",
                table: "TagsOfInterests",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagsOfInterests_Users_UserId",
                table: "TagsOfInterests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_TodoConditions_TodoConditionId",
                table: "Todos",
                column: "TodoConditionId",
                principalTable: "TodoConditions",
                principalColumn: "ConditionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoTags_Tags_TagId",
                table: "TodoTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoTags_Todos_TodoId",
                table: "TodoTags",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "TodoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkWiths_Todos_TodoId",
                table: "WorkWiths",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "TodoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkWiths_Users_UserId",
                table: "WorkWiths",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Todos_TodoId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_FriendshipConditions_FriendshipConditionId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Users_RecipientUserId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Users_SenderUserId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Todos_TodoId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_UserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_MailUpdates_TokenConditions_TokenConditionId",
                table: "MailUpdates");

            migrationBuilder.DropForeignKey(
                name: "FK_MailUpdates_Users_UserId",
                table: "MailUpdates");

            migrationBuilder.DropForeignKey(
                name: "FK_Notices_NoticeTypes_NoticeTypeId",
                table: "Notices");

            migrationBuilder.DropForeignKey(
                name: "FK_Notices_Users_OwnerUserId",
                table: "Notices");

            migrationBuilder.DropForeignKey(
                name: "FK_Notices_Users_RelevantUserId",
                table: "Notices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileDetails_Users_UserId",
                table: "ProfileDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Reminds_Todos_TodoId",
                table: "Reminds");

            migrationBuilder.DropForeignKey(
                name: "FK_Reminds_Users_UserId",
                table: "Reminds");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Users_CreatorUserId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_TagsOfInterests_Tags_TagId",
                table: "TagsOfInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_TagsOfInterests_Users_UserId",
                table: "TagsOfInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_Todos_TodoConditions_TodoConditionId",
                table: "Todos");

            migrationBuilder.DropForeignKey(
                name: "FK_TodoTags_Tags_TagId",
                table: "TodoTags");

            migrationBuilder.DropForeignKey(
                name: "FK_TodoTags_Todos_TodoId",
                table: "TodoTags");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkWiths_Todos_TodoId",
                table: "WorkWiths");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkWiths_Users_UserId",
                table: "WorkWiths");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkWiths",
                table: "WorkWiths");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TokenConditions",
                table: "TokenConditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoTags",
                table: "TodoTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoConditions",
                table: "TodoConditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagsOfInterests",
                table: "TagsOfInterests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reminds",
                table: "Reminds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileDetails",
                table: "ProfileDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoticeTypes",
                table: "NoticeTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notices",
                table: "Notices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MailUpdates",
                table: "MailUpdates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendshipConditions",
                table: "FriendshipConditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "WorkWiths",
                newName: "WorkWith");

            migrationBuilder.RenameTable(
                name: "TokenConditions",
                newName: "TokenCondition");

            migrationBuilder.RenameTable(
                name: "TodoTags",
                newName: "TodoTag");

            migrationBuilder.RenameTable(
                name: "TodoConditions",
                newName: "TodoCondition");

            migrationBuilder.RenameTable(
                name: "TagsOfInterests",
                newName: "TagsOfInterest");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "Reminds",
                newName: "Remind");

            migrationBuilder.RenameTable(
                name: "ProfileDetails",
                newName: "ProfileDetail");

            migrationBuilder.RenameTable(
                name: "NoticeTypes",
                newName: "NoticeType");

            migrationBuilder.RenameTable(
                name: "Notices",
                newName: "Notice");

            migrationBuilder.RenameTable(
                name: "MailUpdates",
                newName: "MailUpdate");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "Like");

            migrationBuilder.RenameTable(
                name: "Friendships",
                newName: "Friendship");

            migrationBuilder.RenameTable(
                name: "FriendshipConditions",
                newName: "FriendshipCondition");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_WorkWiths_UserId",
                table: "WorkWith",
                newName: "IX_WorkWith_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TodoTags_TagId",
                table: "TodoTag",
                newName: "IX_TodoTag_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_TagsOfInterests_UserId",
                table: "TagsOfInterest",
                newName: "IX_TagsOfInterest_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_CreatorUserId",
                table: "Tag",
                newName: "IX_Tag_CreatorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reminds_UserId",
                table: "Remind",
                newName: "IX_Remind_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileDetails_UserId",
                table: "ProfileDetail",
                newName: "IX_ProfileDetail_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notices_RelevantUserId",
                table: "Notice",
                newName: "IX_Notice_RelevantUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notices_OwnerUserId",
                table: "Notice",
                newName: "IX_Notice_OwnerUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notices_NoticeTypeId",
                table: "Notice",
                newName: "IX_Notice_NoticeTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_MailUpdates_UserId",
                table: "MailUpdate",
                newName: "IX_MailUpdate_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MailUpdates_TokenConditionId",
                table: "MailUpdate",
                newName: "IX_MailUpdate_TokenConditionId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_UserId",
                table: "Like",
                newName: "IX_Like_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendships_SenderUserId",
                table: "Friendship",
                newName: "IX_Friendship_SenderUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendships_RecipientUserId",
                table: "Friendship",
                newName: "IX_Friendship_RecipientUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendships_FriendshipConditionId",
                table: "Friendship",
                newName: "IX_Friendship_FriendshipConditionId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comment",
                newName: "IX_Comment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_TodoId",
                table: "Comment",
                newName: "IX_Comment_TodoId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ParentCommentId",
                table: "Comment",
                newName: "IX_Comment_ParentCommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkWith",
                table: "WorkWith",
                columns: new[] { "TodoId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TokenCondition",
                table: "TokenCondition",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoTag",
                table: "TodoTag",
                columns: new[] { "TodoId", "TagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoCondition",
                table: "TodoCondition",
                column: "ConditionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagsOfInterest",
                table: "TagsOfInterest",
                columns: new[] { "TagId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Remind",
                table: "Remind",
                columns: new[] { "TodoId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileDetail",
                table: "ProfileDetail",
                column: "ProfileDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoticeType",
                table: "NoticeType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notice",
                table: "Notice",
                column: "NoticeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MailUpdate",
                table: "MailUpdate",
                column: "MailUpdateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Like",
                table: "Like",
                columns: new[] { "TodoId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friendship",
                table: "Friendship",
                column: "FriendshipKey");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendshipCondition",
                table: "FriendshipCondition",
                column: "ConditionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Comment_ParentCommentId",
                table: "Comment",
                column: "ParentCommentId",
                principalTable: "Comment",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Todos_TodoId",
                table: "Comment",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "TodoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Users_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_FriendshipCondition_FriendshipConditionId",
                table: "Friendship",
                column: "FriendshipConditionId",
                principalTable: "FriendshipCondition",
                principalColumn: "ConditionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_Users_RecipientUserId",
                table: "Friendship",
                column: "RecipientUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_Users_SenderUserId",
                table: "Friendship",
                column: "SenderUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Todos_TodoId",
                table: "Like",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "TodoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Users_UserId",
                table: "Like",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MailUpdate_TokenCondition_TokenConditionId",
                table: "MailUpdate",
                column: "TokenConditionId",
                principalTable: "TokenCondition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MailUpdate_Users_UserId",
                table: "MailUpdate",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notice_NoticeType_NoticeTypeId",
                table: "Notice",
                column: "NoticeTypeId",
                principalTable: "NoticeType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notice_Users_OwnerUserId",
                table: "Notice",
                column: "OwnerUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notice_Users_RelevantUserId",
                table: "Notice",
                column: "RelevantUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileDetail_Users_UserId",
                table: "ProfileDetail",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Remind_Todos_TodoId",
                table: "Remind",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "TodoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Remind_Users_UserId",
                table: "Remind",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Users_CreatorUserId",
                table: "Tag",
                column: "CreatorUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagsOfInterest_Tag_TagId",
                table: "TagsOfInterest",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagsOfInterest_Users_UserId",
                table: "TagsOfInterest",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_TodoCondition_TodoConditionId",
                table: "Todos",
                column: "TodoConditionId",
                principalTable: "TodoCondition",
                principalColumn: "ConditionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoTag_Tag_TagId",
                table: "TodoTag",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoTag_Todos_TodoId",
                table: "TodoTag",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "TodoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkWith_Todos_TodoId",
                table: "WorkWith",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "TodoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkWith_Users_UserId",
                table: "WorkWith",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
