using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LMSapp.Logic.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLmsEntityUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Courses_CoursesId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Instructors_InstructorId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CoursesId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Students_StudentsId",
                table: "Submissions");

            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EnrollmentDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "EnrollmentDate",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Syllabus",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "SubmittedAt",
                table: "Submissions",
                newName: "SubmissionDate");

            migrationBuilder.RenameColumn(
                name: "Submission",
                table: "Submissions",
                newName: "GradeFeedback");

            migrationBuilder.RenameColumn(
                name: "StudentsId",
                table: "Submissions",
                newName: "EnrollmentsId");

            migrationBuilder.RenameColumn(
                name: "Feedback",
                table: "Submissions",
                newName: "FileUrl");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_StudentsId",
                table: "Submissions",
                newName: "IX_Submissions_EnrollmentsId");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_AssignmentsId_StudentsId",
                table: "Submissions",
                newName: "IX_Submissions_AssignmentsId_EnrollmentsId");

            migrationBuilder.RenameColumn(
                name: "EnrollmentDate",
                table: "Enrollments",
                newName: "EnrollDate");

            migrationBuilder.RenameColumn(
                name: "CoursesId",
                table: "Enrollments",
                newName: "SectionsId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_StudentsId_CoursesId",
                table: "Enrollments",
                newName: "IX_Enrollments_StudentsId_SectionsId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_CoursesId",
                table: "Enrollments",
                newName: "IX_Enrollments_SectionsId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Courses",
                newName: "CourseTitle");

            migrationBuilder.RenameColumn(
                name: "InstructorId",
                table: "Courses",
                newName: "Credits");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Courses",
                newName: "SyllabusExp");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Assignments",
                newName: "AssignName");

            migrationBuilder.RenameColumn(
                name: "CoursesId",
                table: "Assignments",
                newName: "SectionsId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_CoursesId",
                table: "Assignments",
                newName: "IX_Assignments_SectionsId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Grade",
                table: "Submissions",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GradingDate",
                table: "Submissions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Submissions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "Students",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentNumber",
                table: "Students",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Instructors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Office",
                table: "Instructors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinishGrade",
                table: "Enrollments",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxPoints",
                table: "Assignments",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Assignments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AvailableDate",
                table: "Assignments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LockDate",
                table: "Assignments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Assignments",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    SemestersId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Term = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.SemestersId);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    SectionsId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SectionNumber = table.Column<int>(type: "integer", nullable: false),
                    SemestersId = table.Column<int>(type: "integer", nullable: false),
                    CoursesId = table.Column<int>(type: "integer", nullable: false),
                    InstructorId = table.Column<int>(type: "integer", nullable: false),
                    ClassTime = table.Column<TimeSpan>(type: "interval", nullable: true),
                    Room = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.SectionsId);
                    table.ForeignKey(
                        name: "FK_Sections_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "CoursesId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sections_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "InstructorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sections_Semesters_SemestersId",
                        column: x => x.SemestersId,
                        principalTable: "Semesters",
                        principalColumn: "SemestersId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sections_CoursesId",
                table: "Sections",
                column: "CoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_InstructorId",
                table: "Sections",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_SemestersId",
                table: "Sections",
                column: "SemestersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Sections_SectionsId",
                table: "Assignments",
                column: "SectionsId",
                principalTable: "Sections",
                principalColumn: "SectionsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Sections_SectionsId",
                table: "Enrollments",
                column: "SectionsId",
                principalTable: "Sections",
                principalColumn: "SectionsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Enrollments_EnrollmentsId",
                table: "Submissions",
                column: "EnrollmentsId",
                principalTable: "Enrollments",
                principalColumn: "EnrollmentsId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Sections_SectionsId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Sections_SectionsId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Enrollments_EnrollmentsId",
                table: "Submissions");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GradingDate",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "Major",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentNumber",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "Office",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "FinishGrade",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "AvailableDate",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "LockDate",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "SubmissionDate",
                table: "Submissions",
                newName: "SubmittedAt");

            migrationBuilder.RenameColumn(
                name: "GradeFeedback",
                table: "Submissions",
                newName: "Submission");

            migrationBuilder.RenameColumn(
                name: "FileUrl",
                table: "Submissions",
                newName: "Feedback");

            migrationBuilder.RenameColumn(
                name: "EnrollmentsId",
                table: "Submissions",
                newName: "StudentsId");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_EnrollmentsId",
                table: "Submissions",
                newName: "IX_Submissions_StudentsId");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_AssignmentsId_EnrollmentsId",
                table: "Submissions",
                newName: "IX_Submissions_AssignmentsId_StudentsId");

            migrationBuilder.RenameColumn(
                name: "SectionsId",
                table: "Enrollments",
                newName: "CoursesId");

            migrationBuilder.RenameColumn(
                name: "EnrollDate",
                table: "Enrollments",
                newName: "EnrollmentDate");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_StudentsId_SectionsId",
                table: "Enrollments",
                newName: "IX_Enrollments_StudentsId_CoursesId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_SectionsId",
                table: "Enrollments",
                newName: "IX_Enrollments_CoursesId");

            migrationBuilder.RenameColumn(
                name: "SyllabusExp",
                table: "Courses",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Credits",
                table: "Courses",
                newName: "InstructorId");

            migrationBuilder.RenameColumn(
                name: "CourseTitle",
                table: "Courses",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "SectionsId",
                table: "Assignments",
                newName: "CoursesId");

            migrationBuilder.RenameColumn(
                name: "AssignName",
                table: "Assignments",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_SectionsId",
                table: "Assignments",
                newName: "IX_Assignments_CoursesId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "PasswordHash",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "Submissions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollmentDate",
                table: "Students",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollmentDate",
                table: "Instructors",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Courses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Syllabus",
                table: "Courses",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaxPoints",
                table: "Assignments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Assignments",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Assignments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Assignments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    TodosId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppUserId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.TodosId);
                    table.ForeignKey(
                        name: "FK_Todos_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Users",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_AppUserId",
                table: "Todos",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Courses_CoursesId",
                table: "Assignments",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "CoursesId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Instructors_InstructorId",
                table: "Courses",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "InstructorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CoursesId",
                table: "Enrollments",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "CoursesId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Students_StudentsId",
                table: "Submissions",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "StudentsId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
