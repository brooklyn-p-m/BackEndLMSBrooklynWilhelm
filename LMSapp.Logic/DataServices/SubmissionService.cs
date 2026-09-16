using Microsoft.EntityFrameworkCore;

public class SubmissionService : IDataService<Submissions>
{
    private readonly AppDbContext _context;
    public SubmissionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Submissions?> GetByIdAsync(int submissionId)
    {
        return await _context.Submissions.FirstOrDefaultAsync(s => s.SubmissionsId == submissionId);
    }

    public async Task<IEnumerable<Submissions>?> GetAllAsync()
    {
        return await _context.Submissions.ToListAsync();
    }

    public async Task<IEnumerable<Submissions>?> GetByStudentAsync(int studentId)
    {
        return await _context.Submissions.Where(s => s.StudentsId == studentId).ToListAsync();
    }

    public async Task<IEnumerable<Submissions>?> GetByAssignmentAsync(int assignmentId)
    {
        return await _context.Submissions.Where(s => s.AssignmentsId == assignmentId).ToListAsync();
    }

    public async Task<Submissions> CreateAsync(Submissions submission)
    {
        _context.Add(submission);
        await _context.SaveChangesAsync();
        return submission;
    }
    public async Task<Submissions> SubmitAssignmentAsync(int assignmentId, int studentId, string content)
    {
        var existing = await _context.Submissions
            .FirstOrDefaultAsync(s => s.AssignmentsId == assignmentId && s.StudentsId == studentId);

        if (existing is not null)
        {
            existing.Submission = content;
            existing.SubmittedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return existing;
        }

        var assignment = await _context.Assignments.FindAsync(assignmentId);
        var student = await _context.Students.FindAsync(studentId);

        var submission = new Submissions
        {
            AssignmentsId = assignmentId,
            StudentsId = studentId,
            Assignment = assignment,
            Student = student,
            Submission = content,
            SubmittedAt = DateTime.Now
        };

        _context.Add(submission);
        await _context.SaveChangesAsync();
        return submission;
    }

    public async Task<bool> GradeSubmissionAsync(int submissionId, int grade, string? feedback)
    {
        var submissionToGrade = await _context.Submissions.FindAsync(submissionId);
        if (submissionToGrade is null) return false;

        submissionToGrade.Grade = grade;
        submissionToGrade.Feedback = feedback;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Submissions?> UpdateAsync(Submissions submission)
    {
        var submissionToUpdate = await _context.Submissions.FindAsync(submission.SubmissionsId);
        if (submissionToUpdate is null) return null;

        submissionToUpdate.Submission = submission.Submission;
        submissionToUpdate.Grade = submission.Grade;
        submissionToUpdate.Feedback = submission.Feedback;

        await _context.SaveChangesAsync();
        return submissionToUpdate;
    }

    public async Task<bool> DeleteAsync(int submissionId)
    {
        var submissionToDelete = await _context.Submissions.FindAsync(submissionId);
        if (submissionToDelete is null) return false;

        _context.Remove(submissionToDelete);
        await _context.SaveChangesAsync();
        return true;
    }
}