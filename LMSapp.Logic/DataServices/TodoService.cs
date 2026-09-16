using Microsoft.EntityFrameworkCore;

public class TodoService : IDataService<Todos>
{
    private readonly AppDbContext _context;
    public TodoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Todos?> GetByIdAsync(int todoId)
    {
        return await _context.Todos.FirstOrDefaultAsync(t => t.TodosId == todoId);
    }

    public async Task<IEnumerable<Todos>?> GetAllAsync()
    {
        return await _context.Todos.ToListAsync();
    }

    public async Task<IEnumerable<Todos>?> GetByUserAsync(int appUserId)
    {
        return await _context.Todos.Where(t => t.AppUserId == appUserId).OrderBy(t => t.DueDate).ToListAsync();
    }

    public async Task<Todos> CreateAsync(Todos todo)
    {
        _context.Add(todo);
        await _context.SaveChangesAsync();
        return todo;
    }

    public async Task<Todos?> UpdateAsync(Todos todo)
    {
        var todoToUpdate = await _context.Todos.FindAsync(todo.TodosId);
        if (todoToUpdate is null) return null;

        todoToUpdate.Title = todo.Title;
        todoToUpdate.Description = todo.Description;
        todoToUpdate.DueDate = todo.DueDate;
        todoToUpdate.IsCompleted = todo.IsCompleted;

        await _context.SaveChangesAsync();
        return todoToUpdate;
    }

    public async Task<bool> ToggleCompleteAsync(int todoId)
    {
        var todoToUpdate = await _context.Todos.FindAsync(todoId);
        if (todoToUpdate is null) return false;

        todoToUpdate.IsCompleted = !todoToUpdate.IsCompleted;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int todoId)
    {
        var todoToDelete = await _context.Todos.FindAsync(todoId);
        if (todoToDelete is null) return false;

        _context.Remove(todoToDelete);
        await _context.SaveChangesAsync();
        return true;
    }
}