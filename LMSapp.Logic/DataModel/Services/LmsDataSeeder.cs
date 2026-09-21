using Microsoft.EntityFrameworkCore;

public class LmsDataSeeder
{
    private readonly AppDbContext _context;

    public LmsDataSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await _context.Database.MigrateAsync();

        var hasLmsSeedData = await _context.Courses.AnyAsync()
            || await _context.Sections.AnyAsync()
            || await _context.Assignments.AnyAsync()
            || await _context.Enrollments.AnyAsync();

        if (hasLmsSeedData)
        {
            return;
        }

        var now = DateTime.UtcNow.Date;

        var instructorUser = new AppUser
        {
            AppUserId = 0,
            Login = "profsmith",
            FirstName = "Alicia",
            LastName = "Smith",
            Email = "profsmith@brooklyn.edu",
            Phone = "555-1111",
            Password = "P@ssw0rd123",
            IsDeleted = false
        };

        var studentUser1 = new AppUser
        {
            AppUserId = 0,
            Login = "mariajones",
            FirstName = "Maria",
            LastName = "Jones",
            Email = "maria.jones@brooklyn.edu",
            Phone = "555-2222",
            Password = "P@ssw0rd123",
            IsDeleted = false
        };

        var studentUser2 = new AppUser
        {
            AppUserId = 0,
            Login = "davidlee",
            FirstName = "David",
            LastName = "Lee",
            Email = "david.lee@brooklyn.edu",
            Phone = "555-3333",
            Password = "P@ssw0rd123",
            IsDeleted = false
        };

        var studentUser3 = new AppUser
        {
            AppUserId = 0,
            Login = "sophiawong",
            FirstName = "Sophia",
            LastName = "Wong",
            Email = "sophia.wong@brooklyn.edu",
            Phone = "555-4444",
            Password = "P@ssw0rd123",
            IsDeleted = false
        };

        var testUser = new AppUser
        {
            AppUserId = 0,
            Login = "testtest",
            FirstName = "Test",
            LastName = "Test",
            Email = "test@test.com",
            Phone = "555-9000",
            Password = "test123",
            IsDeleted = false
        };

        _context.AppUsers.AddRange(instructorUser, studentUser1, studentUser2, studentUser3, testUser);
        await _context.SaveChangesAsync();

        var instructor = new Instructor
        {
            InstructorId = 0,
            AppUserId = instructorUser.AppUserId,
            User = instructorUser,
            Department = "Computer Science",
            Office = "B-210",
            IsDeleted = false
        };

        _context.Instructors.Add(instructor);
        await _context.SaveChangesAsync();

        var student1 = new Students
        {
            StudentsId = 0,
            AppUserId = studentUser1.AppUserId,
            User = studentUser1,
            StudentNumber = 10001,
            Major = "Computer Science",
            IsDeleted = false
        };

        var student2 = new Students
        {
            StudentsId = 0,
            AppUserId = studentUser2.AppUserId,
            User = studentUser2,
            StudentNumber = 10002,
            Major = "Mathematics",
            IsDeleted = false
        };

        var student3 = new Students
        {
            StudentsId = 0,
            AppUserId = studentUser3.AppUserId,
            User = studentUser3,
            StudentNumber = 10003,
            Major = "Business Administration",
            IsDeleted = false
        };

        var testStudent = new Students
        {
            StudentsId = 0,
            AppUserId = testUser.AppUserId,
            User = testUser,
            StudentNumber = 90001,
            Major = "Software Engineering",
            IsDeleted = false
        };

        _context.Students.AddRange(student1, student2, student3, testStudent);
        await _context.SaveChangesAsync();

        var semester = new Semesters
        {
            SemestersId = 0,
            Term = "Fall",
            Year = 2026,
            sections = new List<Sections>()
        };

        _context.Semesters.Add(semester);
        await _context.SaveChangesAsync();

        var courseBlueprints = new[]
        {
            new { Code = "CS-220", Title = "Web Development Principles", Credits = 4, SectionNumber = 220, Room = "B-214", Time = new TimeSpan(9, 30, 0), Assignments = new[] { (Name: "UX wireframe sprint", OffsetDays: 2, Points: 90m), (Name: "Responsive portfolio build", OffsetDays: 7, Points: 120m), (Name: "Sprint demo review", OffsetDays: 13, Points: 140m) } },
            new { Code = "BIO-240", Title = "Human Anatomy & Lab", Credits = 4, SectionNumber = 240, Room = "C-118", Time = new TimeSpan(11, 0, 0), Assignments = new[] { (Name: "Anatomy atlas check-in", OffsetDays: 3, Points: 80m), (Name: "Lab practical reflection", OffsetDays: 8, Points: 110m), (Name: "Case study presentation", OffsetDays: 14, Points: 150m) } },
            new { Code = "MATH-310", Title = "Applied Statistics", Credits = 3, SectionNumber = 310, Room = "D-402", Time = new TimeSpan(13, 30, 0), Assignments = new[] { (Name: "Data collection memo", OffsetDays: 4, Points: 100m), (Name: "Regression analysis brief", OffsetDays: 10, Points: 130m), (Name: "Predictive model critique", OffsetDays: 16, Points: 160m) } },
            new { Code = "ENG-230", Title = "Technical Communication", Credits = 3, SectionNumber = 230, Room = "A-109", Time = new TimeSpan(15, 0, 0), Assignments = new[] { (Name: "Audience analysis memo", OffsetDays: 5, Points: 85m), (Name: "Design review proposal", OffsetDays: 9, Points: 120m), (Name: "Final technical brief", OffsetDays: 15, Points: 170m) } }
        };

        foreach (var courseBlueprint in courseBlueprints)
        {
            var course = new Courses
            {
                CoursesId = 0,
                CourseCode = courseBlueprint.Code,
                CourseTitle = courseBlueprint.Title,
                Credits = courseBlueprint.Credits,
                SyllabusExp = now.AddMonths(4),
                IsDeleted = false,
                sections = new List<Sections>()
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            var section = new Sections
            {
                SectionsId = 0,
                SectionNumber = courseBlueprint.SectionNumber,
                SemestersId = semester.SemestersId,
                Semester = semester,
                CoursesId = course.CoursesId,
                Course = course,
                InstructorId = instructor.InstructorId,
                Instructor = instructor,
                ClassTime = courseBlueprint.Time,
                Room = courseBlueprint.Room,
                IsDeleted = false,
                enrollments = new List<Enrollments>(),
                assignments = new List<Assignments>()
            };

            _context.Sections.Add(section);
            await _context.SaveChangesAsync();

            var assignmentBlueprints = courseBlueprint.Assignments;
            var assignments = assignmentBlueprints
                .Select((assignment, index) => new Assignments
                {
                    AssignmentsId = 0,
                    SectionsId = section.SectionsId,
                    Section = section,
                    AssignName = assignment.Name,
                    DueDate = now.AddDays(assignment.OffsetDays),
                    AvailableDate = now.AddDays(assignment.OffsetDays - 7),
                    LockDate = now.AddDays(assignment.OffsetDays + 2),
                    MaxPoints = assignment.Points,
                    Url = $"https://example.edu/{courseBlueprint.Code.ToLowerInvariant().Replace("-", "")}/assignment-{index + 1}",
                    IsDeleted = false,
                    submissions = new List<Submissions>()
                })
                .ToList();

            _context.Assignments.AddRange(assignments);
            await _context.SaveChangesAsync();

            var enrollment = new Enrollments
            {
                EnrollmentsId = 0,
                StudentsId = testStudent.StudentsId,
                Student = testStudent,
                SectionsId = section.SectionsId,
                Section = section,
                EnrollDate = now.AddDays(-14),
                Status = EnrollmentStatus.Active,
                FinishGrade = null,
                IsDeleted = false,
                submissions = new List<Submissions>()
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            if (courseBlueprint.Code == "CS-220")
            {
                var sampleSubmission = new Submissions
                {
                    SubmissionsId = 0,
                    AssignmentsId = assignments[0].AssignmentsId,
                    Assignment = assignments[0],
                    EnrollmentsId = enrollment.EnrollmentsId,
                    Enrollment = enrollment,
                    Grade = 93m,
                    SubmissionDate = now.AddDays(1),
                    FileUrl = "https://storage.example.com/test/ux-wireframe-sprint.pdf",
                    GradeFeedback = "Strong creative direction and clear structure.",
                    IsDeleted = false
                };

                _context.Submissions.Add(sampleSubmission);
                await _context.SaveChangesAsync();
            }
        }
    }

    public async Task ClearSeedDataAsync()
    {
        var seededLogins = new[] { "profsmith", "mariajones", "davidlee", "sophiawong", "testtest" };
        var seededEmails = new[]
        {
            "profsmith@brooklyn.edu",
            "maria.jones@brooklyn.edu",
            "david.lee@brooklyn.edu",
            "sophia.wong@brooklyn.edu",
            "test@test.com"
        };

        var seededUsers = await _context.AppUsers
            .Where(u => seededLogins.Contains(u.Login) || seededEmails.Contains(u.Email))
            .ToListAsync();

        if (!seededUsers.Any())
        {
            return;
        }

        var seededUserIds = seededUsers.Select(u => u.AppUserId).ToList();

        var seededStudents = await _context.Students
            .Where(s => seededUserIds.Contains(s.AppUserId))
            .ToListAsync();

        var seededStudentIds = seededStudents.Select(s => s.StudentsId).ToList();

        var seededInstructors = await _context.Instructors
            .Where(i => seededUserIds.Contains(i.AppUserId))
            .ToListAsync();

        var seededInstructorIds = seededInstructors.Select(i => i.InstructorId).ToList();

        var seededSectionIds = await _context.Sections
            .Where(s => seededInstructorIds.Contains(s.InstructorId))
            .Select(s => s.SectionsId)
            .ToListAsync();

        var seededAssignmentIds = await _context.Assignments
            .Where(a => seededSectionIds.Contains(a.SectionsId))
            .Select(a => a.AssignmentsId)
            .ToListAsync();

        var seededEnrollmentIds = await _context.Enrollments
            .Where(e => seededStudentIds.Contains(e.StudentsId) || seededSectionIds.Contains(e.SectionsId))
            .Select(e => e.EnrollmentsId)
            .ToListAsync();

        if (seededEnrollmentIds.Count > 0)
        {
            var seededSubmissionRows = await _context.Submissions
                .Where(s => seededEnrollmentIds.Contains(s.EnrollmentsId) || seededAssignmentIds.Contains(s.AssignmentsId))
                .ToListAsync();

            if (seededSubmissionRows.Any())
            {
                _context.Submissions.RemoveRange(seededSubmissionRows);
            }
        }

        if (seededEnrollmentIds.Count > 0)
        {
            var enrollmentRows = await _context.Enrollments
                .Where(e => seededEnrollmentIds.Contains(e.EnrollmentsId))
                .ToListAsync();

            if (enrollmentRows.Any())
            {
                _context.Enrollments.RemoveRange(enrollmentRows);
            }
        }

        if (seededAssignmentIds.Count > 0)
        {
            var assignmentRows = await _context.Assignments
                .Where(a => seededAssignmentIds.Contains(a.AssignmentsId))
                .ToListAsync();

            if (assignmentRows.Any())
            {
                _context.Assignments.RemoveRange(assignmentRows);
            }
        }

        if (seededSectionIds.Count > 0)
        {
            var sectionRows = await _context.Sections
                .Where(s => seededSectionIds.Contains(s.SectionsId))
                .ToListAsync();

            if (sectionRows.Any())
            {
                _context.Sections.RemoveRange(sectionRows);
            }
        }

        var seededCourseCodes = new[] { "CS-220", "BIO-240", "MATH-310", "ENG-230" };
        var seededCourses = await _context.Courses
            .Where(c => seededCourseCodes.Contains(c.CourseCode))
            .ToListAsync();

        if (seededCourses.Any())
        {
            _context.Courses.RemoveRange(seededCourses);
        }

        var semesterIds = await _context.Semesters
            .Where(s => s.Year == 2026 && s.Term == "Fall")
            .Select(s => s.SemestersId)
            .ToListAsync();

        if (semesterIds.Count > 0)
        {
            var sems = await _context.Semesters
                .Where(s => semesterIds.Contains(s.SemestersId))
                .ToListAsync();

            if (sems.Any())
            {
                _context.Semesters.RemoveRange(sems);
            }
        }

        if (seededStudents.Any())
        {
            _context.Students.RemoveRange(seededStudents);
        }

        if (seededInstructors.Any())
        {
            _context.Instructors.RemoveRange(seededInstructors);
        }

        if (seededUsers.Any())
        {
            _context.AppUsers.RemoveRange(seededUsers);
        }

        await _context.SaveChangesAsync();
    }
}
