
using Microsoft.AspNetCore.Http;
using OnlineStory.Contract.Share;

namespace OnlineStory.Application.Abstractions.Services;

public interface IImageStorageService
{
    Task<Result<string>> SaveImageAsync(IFormFile image, string folder = "tamp");
    Task DeleteImageAsync(string imagePath);
}
