using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiPromptOptimizer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_chats",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    prompt_category = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_chats", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_chat_messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_chat_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sequence_number = table.Column<int>(type: "integer", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_chat_messages", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_chat_messages_user_chats_user_chat_id",
                        column: x => x.user_chat_id,
                        principalTable: "user_chats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_chat_messages_user_chat_id",
                table: "user_chat_messages",
                column: "user_chat_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_chat_messages_user_chat_id_sequence_number",
                table: "user_chat_messages",
                columns: new[] { "user_chat_id", "sequence_number" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_chat_messages");

            migrationBuilder.DropTable(
                name: "user_chats");
        }
    }
}
