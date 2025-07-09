using Microsoft.AspNetCore.Http;

namespace TaskBoard.Application.Interfaces;

public interface IFileStorage
{
    Task<string> SaveFileAsync(IFormFile file, string subFolder);
    Task DeleteFileAsync(string relativePath);
}