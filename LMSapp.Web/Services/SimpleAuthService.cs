using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace LMSapp.Web.Services;

public class SimpleAuthService
{
    private const string StorageKey = "lms_current_user";
    private readonly IJSRuntime _jsRuntime;

    public SimpleAuthService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task SetCurrentUserAsync(AppUser user)
    {
        var payload = new CurrentUserDto
        {
            AppUserId = user.AppUserId,
            Login = user.Login,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone
        };

        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", StorageKey, JsonSerializer.Serialize(payload));
    }

    public async Task<AppUser?> GetCurrentUserAsync()
    {
        try
        {
            var json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", StorageKey);
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            var payload = JsonSerializer.Deserialize<CurrentUserDto>(json);
            if (payload is null)
            {
                return null;
            }

            return new AppUser
            {
                AppUserId = payload.AppUserId,
                Login = payload.Login ?? string.Empty,
                FirstName = payload.FirstName ?? string.Empty,
                LastName = payload.LastName ?? string.Empty,
                Email = payload.Email ?? string.Empty,
                Phone = payload.Phone,
                IsDeleted = false,
                Password = string.Empty
            };
        }
        catch
        {
            return null;
        }
    }

    public async Task ClearCurrentUserAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", StorageKey);
    }

    public async Task<bool> IsLoggedInAsync()
    {
        return await GetCurrentUserAsync() is not null;
    }

    public async Task<bool> EnsureLoggedInAsync(NavigationManager navigationManager)
    {
        if (await IsLoggedInAsync())
        {
            return true;
        }

        navigationManager.NavigateTo("/login");
        return false;
    }

    private sealed class CurrentUserDto
    {
        public int AppUserId { get; set; }
        public string? Login { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
