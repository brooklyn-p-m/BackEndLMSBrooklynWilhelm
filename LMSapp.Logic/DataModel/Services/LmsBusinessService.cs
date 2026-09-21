using Microsoft.EntityFrameworkCore;

public class LmsBusinessService
{
    private readonly AppDbContext _context;

    public LmsBusinessService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CourseSummaryDto>> GetStudentCourseSummariesAsync(int studentId)
    {
        var sectionIds = await _context.Enrollments
            .Where(e => e.StudentsId == studentId && !e.IsDeleted)
            .Select(e => e.SectionsId)
            .Distinct()
            .ToListAsync();

        var sections = await _context.Sections
            .Where(s => sectionIds.Contains(s.SectionsId) && !s.IsDeleted)
            .Include(s => s.Course)
            .Include(s => s.Instructor)
                .ThenInclude(i => i.User)
            .ToListAsync();

        var results = new List<CourseSummaryDto>();

        foreach (var section in sections)
        {
            var course = section.Course;
            var assignments = await _context.Assignments
                .Where(a => a.SectionsId == section.SectionsId && !a.IsDeleted)
                .ToListAsync();

            var enrollmentIds = await _context.Enrollments
                .Where(e => e.StudentsId == studentId && e.SectionsId == section.SectionsId && !e.IsDeleted)
                .Select(e => e.EnrollmentsId)
                .ToListAsync();

            var gradedAssignments = 0;
            var totalPoints = 0m;
            var earnedPoints = 0m;

            foreach (var assignment in assignments)
            {
                var submission = await _context.Submissions
                    .Where(s => s.AssignmentsId == assignment.AssignmentsId && enrollmentIds.Contains(s.EnrollmentsId) && !s.IsDeleted)
                    .OrderByDescending(s => s.SubmissionDate)
                    .FirstOrDefaultAsync();

                if (submission?.Grade is not null)
                {
                    gradedAssignments++;
                    totalPoints += assignment.MaxPoints;
                    earnedPoints += submission.Grade.Value;
                }
            }

            var progress = totalPoints > 0 ? (int)Math.Round((earnedPoints / totalPoints) * 100m) : 0;

            results.Add(new CourseSummaryDto
            {
                Id = course.CoursesId,
                Code = course.CourseCode,
                Title = course.CourseTitle,
                Instructor = section.Instructor != null && section.Instructor.User != null
                    ? $"{section.Instructor.User.FirstName} {section.Instructor.User.LastName}".Trim()
                    : "TBD",
                Credits = course.Credits,
                Progress = Math.Clamp(progress, 0, 100),
                Status = "Current",
                SyllabusExp = course.SyllabusExp
            });
        }

        return results.OrderBy(c => c.Title).ToList();
    }

    public async Task<List<AssignmentSummaryDto>> GetStudentAssignmentSummariesAsync(int studentId)
    {
        var sectionIds = await _context.Enrollments
            .Where(e => e.StudentsId == studentId && !e.IsDeleted)
            .Select(e => e.SectionsId)
            .Distinct()
            .ToListAsync();

        var assignments = await _context.Assignments
            .Where(a => sectionIds.Contains(a.SectionsId) && !a.IsDeleted)
            .Include(a => a.Section)
                .ThenInclude(s => s.Course)
            .OrderBy(a => a.DueDate)
            .ToListAsync();

        var enrollmentIds = await _context.Enrollments
            .Where(e => e.StudentsId == studentId && !e.IsDeleted)
            .Select(e => e.EnrollmentsId)
            .ToListAsync();

        var results = new List<AssignmentSummaryDto>();

        foreach (var assignment in assignments)
        {
            var submission = await _context.Submissions
                .Where(s => s.AssignmentsId == assignment.AssignmentsId && enrollmentIds.Contains(s.EnrollmentsId) && !s.IsDeleted)
                .OrderByDescending(s => s.SubmissionDate)
                .FirstOrDefaultAsync();

            var status = submission switch
            {
                null => "Pending",
                _ when submission.Grade.HasValue => "Graded",
                _ => "Submitted"
            };

            results.Add(new AssignmentSummaryDto
            {
                Id = assignment.AssignmentsId,
                CourseCode = assignment.Section?.Course?.CourseCode ?? "N/A",
                Title = assignment.AssignName,
                DueDate = assignment.DueDate,
                MaxPoints = assignment.MaxPoints,
                Status = status,
                Grade = submission?.Grade,
                FileUrl = submission?.FileUrl
            });
        }

        return results;
    }

    public async Task<List<GradeSummaryDto>> GetStudentGradeSummariesAsync(int studentId)
    {
        var courses = await GetStudentCourseSummariesAsync(studentId);
        var results = new List<GradeSummaryDto>();

        foreach (var course in courses)
        {
            var section = await _context.Sections
                .Include(s => s.Course)
                .FirstOrDefaultAsync(s => s.CoursesId == course.Id && !s.IsDeleted);

            if (section is null)
            {
                continue;
            }

            var enrollmentIds = await _context.Enrollments
                .Where(e => e.StudentsId == studentId && e.SectionsId == section.SectionsId && !e.IsDeleted)
                .Select(e => e.EnrollmentsId)
                .ToListAsync();

            var assignmentList = await _context.Assignments
                .Where(a => a.SectionsId == section.SectionsId && !a.IsDeleted && a.DueDate <= DateTime.UtcNow)
                .ToListAsync();

            decimal totalPoints = 0m;
            decimal earnedPoints = 0m;
            int completedTasks = 0;
            foreach (var assignment in assignmentList)
            {
                var submission = await _context.Submissions
                    .Where(s => s.AssignmentsId == assignment.AssignmentsId && enrollmentIds.Contains(s.EnrollmentsId) && !s.IsDeleted)
                    .OrderByDescending(s => s.SubmissionDate)
                    .FirstOrDefaultAsync();

                if (submission is not null)
                {
                    completedTasks++;
                }

                if (submission?.Grade is not null)
                {
                    totalPoints += assignment.MaxPoints;
                    earnedPoints += submission.Grade.Value;
                }
            }

            var percentage = totalPoints > 0 ? (double)(earnedPoints / totalPoints * 100m) : 0;

            results.Add(new GradeSummaryDto
            {
                Id = course.Id,
                Code = course.Code,
                Title = course.Title,
                Credits = course.Credits,
                Percentage = percentage,
                LetterGrade = GetLetterGrade(percentage),
                CompletedTasks = completedTasks,
                TotalTasks = assignmentList.Count
            });
        }

        return results;
    }

    public async Task<List<CalendarEntryDto>> GetStudentCalendarEntriesAsync(int studentId)
    {
        var assignments = await GetStudentAssignmentSummariesAsync(studentId);

        return assignments
            .Where(a => a.DueDate != default)
            .Select(a => new CalendarEntryDto
            {
                Id = a.Id,
                Date = a.DueDate,
                Title = a.Title,
                CourseCode = a.CourseCode,
                Type = "deadline"
            })
            .OrderBy(x => x.Date)
            .ToList();
    }

    public async Task<int> GetDefaultStudentIdAsync()
    {
        var preferredStudent = await _context.Students
            .Include(s => s.User)
            .OrderBy(s => s.User != null && s.User.Email == "test@test.com" ? 0 : 1)
            .ThenBy(s => s.StudentsId)
            .FirstOrDefaultAsync();

        return preferredStudent?.StudentsId ?? 0;
    }

    private static string GetLetterGrade(double percentage)
    {
        if (percentage >= 90) return "A";
        if (percentage >= 85) return "A-";
        if (percentage >= 80) return "B";
        if (percentage >= 75) return "B-";
        if (percentage >= 70) return "C";
        if (percentage >= 60) return "C-";
        if (percentage >= 50) return "D";
        return "F";
    }
}

public class CourseSummaryDto
{
    public int Id { get; set; }
    public string Code { get; set; } = "";
    public string Title { get; set; } = "";
    public string Instructor { get; set; } = "TBD";
    public int Credits { get; set; }
    public int Progress { get; set; }
    public string Status { get; set; } = "Current";
    public DateTime? SyllabusExp { get; set; }
}

public class AssignmentSummaryDto
{
    public int Id { get; set; }
    public string CourseCode { get; set; } = "";
    public string Title { get; set; } = "";
    public DateTime DueDate { get; set; }
    public decimal MaxPoints { get; set; }
    public string Status { get; set; } = "Pending";
    public decimal? Grade { get; set; }
    public string? FileUrl { get; set; }
}

public class GradeSummaryDto
{
    public int Id { get; set; }
    public string Code { get; set; } = "";
    public string Title { get; set; } = "";
    public int Credits { get; set; }
    public double Percentage { get; set; }
    public string LetterGrade { get; set; } = "F";
    public int CompletedTasks { get; set; }
    public int TotalTasks { get; set; }
}

public class CalendarEntryDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Title { get; set; } = "";
    public string CourseCode { get; set; } = "";
    public string Type { get; set; } = "deadline";
}
