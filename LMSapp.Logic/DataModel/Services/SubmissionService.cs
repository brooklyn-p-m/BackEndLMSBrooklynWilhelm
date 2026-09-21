using Microsoft.EntityFrameworkCore;

public class SubmissionService : IDataService<Submissions>
{
    private readonly AppDbContext _context;
    public SubmissionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Submissions?> GetByIdAsync(int submissionsId)
    {
        return await _context.Submissions.FirstOrDefaultAsync(s => s.SubmissionsId == submissionsId);
    }

    public async Task<IEnumerable<Submissions>?> GetAllAsync()
    {
        return await _context.Submissions.Where(s => s.IsDeleted == false).ToListAsync();
    }

    public async Task<IEnumerable<Submissions>?> GetByAssignmentAsync(int assignmentsId)
    {
        return await _context.Submissions.Where(s => s.AssignmentsId == assignmentsId && s.IsDeleted == false).ToListAsync();
    }

    public async Task<IEnumerable<Submissions>?> GetByEnrollmentAsync(int enrollmentsId)
    {
        return await _context.Submissions.Where(s => s.EnrollmentsId == enrollmentsId && s.IsDeleted == false).ToListAsync();
    }

    public async Task<Submissions> CreateAsync(Submissions submission)
    {
        submission.SubmissionDate = DateTime.UtcNow;
        _context.Add(submission);
        await _context.SaveChangesAsync();
        return submission;
    }

    public async Task<Submissions?> UpdateAsync(Submissions submission)
    {
        var submissionToUpdate = await _context.Submissions.FindAsync(submission.SubmissionsId);
        if (submissionToUpdate is null) return null;

        submissionToUpdate.FileUrl = submission.FileUrl;
        submissionToUpdate.SubmissionDate = submission.SubmissionDate;

        await _context.SaveChangesAsync();
        return submissionToUpdate;
    }

    public async Task<bool> GradeAsync(int submissionsId, decimal grade, string? feedback)
    {
        var submissionToGrade = await _context.Submissions.FindAsync(submissionsId);
        if (submissionToGrade is null) return false;

        submissionToGrade.Grade = grade;
        submissionToGrade.GradeFeedback = feedback;
        submissionToGrade.GradingDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int submissionsId)
    {
        var submissionToDelete = await _context.Submissions.Where(s => s.IsDeleted == false && s.SubmissionsId == submissionsId).SingleOrDefaultAsync();
        if (submissionToDelete is null) return false;

        submissionToDelete.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
}
