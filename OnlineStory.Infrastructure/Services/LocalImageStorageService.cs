
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OnlineStory.Application.Abstractions.Services;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;

namespace OnlineStory.Infrastructure.Services;

public class LocalImageStorageService : IImageStorageService
{

    public async Task<Result<string>> SaveImageAsync(IFormFile image, string folder = "tamp")
    {
        string[] allowedExtensions = { ".jpg", ".jpeg", ".png"};
        if(!IsFileExtensionAllowed(image, allowedExtensions))
        {
            return Error.Validation($"File {image.FileName} extension not allowed.");
        }
        using var imageStream = image.OpenReadStream();
        var folderPath = Path.Combine("wwwroot/images", folder);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        // Combine the full file path
        string ticks = DateTime.UtcNow.Ticks.ToString();
        var fileExtension = Path.GetExtension(image.FileName).ToLower();
        var fileName = $"{Path.GetFileNameWithoutExtension(image.FileName).ToLower().Replace(" ", "-")}{ticks}{fileExtension}";
        var filePath = Path.Combine(folderPath,fileName);

        // Save the file
        try
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await imageStream.CopyToAsync(fileStream);
            }
            return $"images/{folder}/{fileName}";
        }
        catch (Exception ex) {
            return Error.Validation("Failed to save image", ex.Message);
        }
    }

    public async Task DeleteImageAsync(string imagePath)
    {
        // Implement the logic to delete the image from your storage
        if (string.IsNullOrEmpty(imagePath))
            throw new ArgumentNullException(nameof(imagePath));

        // Assuming the images are stored in a directory
        var fullPath = Path.Combine("wwwroot", imagePath);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            await Task.CompletedTask; // Simulate async operation
        }
        else
        {
            throw new FileNotFoundException("Image not found", fullPath);
        }
    }
    private bool IsFileExtensionAllowed(IFormFile file, string[] allowedExtensions)
    {
        var extension = Path.GetExtension(file.FileName);
        return allowedExtensions.Contains(extension);
    }
}
