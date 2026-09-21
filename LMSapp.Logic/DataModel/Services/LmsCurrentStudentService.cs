public class LmsCurrentStudentService
{
    public Students? CurrentStudent { get; private set; }
    public int? CurrentStudentId { get; private set; }

    public void SetCurrentStudent(Students student)
    {
        CurrentStudent = student;
        CurrentStudentId = student.StudentsId;
    }

    public void Clear()
    {
        CurrentStudent = null;
        CurrentStudentId = null;
    }
}
