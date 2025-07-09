using Microsoft.AspNetCore.Http;
using TaskBoard.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace TaskBoard.Infrastructure;

public class FileStorage: IFileStorage
{
    private readonly string _rootPath;

    public FileStorage(IWebHostEnvironment env)
    {
        _rootPath = env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    }

    public async Task<string> SaveFileAsync(IFormFile file, string subFolder)
    {
        var folderPath = Path.Combine(_rootPath, subFolder);
        Directory.CreateDirectory(folderPath);

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(folderPath, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return Path.Combine(subFolder, fileName).Replace("\\", "/");
    }

    public Task DeleteFileAsync(string relativePath)
    {
        var fullPath = Path.Combine(_rootPath, relativePath);
        if (File.Exists(fullPath)) File.Delete(fullPath);
        return Task.CompletedTask;
    }
}