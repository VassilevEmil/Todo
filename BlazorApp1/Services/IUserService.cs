using BlazorApp1.Models;

namespace BlazorApp1.Services;

public interface IUserService
{
    public Task<User> GetUserAsync(string username);
}