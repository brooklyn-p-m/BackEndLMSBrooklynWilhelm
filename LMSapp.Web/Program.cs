using LMSapp.Web.Components;
using LMSapp.Web.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "DataProtectionKeys")))
    .SetApplicationName("LMSapp.Web");

builder.Services.AddScoped<SimpleAuthService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<AppUserService>();
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<InstructorService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<SemesterService>();
builder.Services.AddScoped<SectionService>();
builder.Services.AddScoped<EnrollmentService>();
builder.Services.AddScoped<AssignmentService>();
builder.Services.AddScoped<SubmissionService>();
builder.Services.AddScoped<LmsBusinessService>();
builder.Services.AddScoped<LmsCurrentStudentService>();
builder.Services.AddScoped<LmsDataSeeder>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    for (var attempt = 1; attempt <= 20; attempt++)
    {
        try
        {
            dbContext.Database.Migrate();
            break;
        }
        catch (NpgsqlException) when (attempt < 20)
        {
            await Task.Delay(5000);
        }
    }

    var seeder = scope.ServiceProvider.GetRequiredService<LmsDataSeeder>();
    await seeder.SeedAsync();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
