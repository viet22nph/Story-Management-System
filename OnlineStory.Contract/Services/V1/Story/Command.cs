

using MediatR;
using Microsoft.AspNetCore.Http;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;

namespace OnlineStory.Contract.Services.V1.Story;

public class Command
{
    public record CreateStoryCommand(
        string StoryTitle,
        string? AnotherStoryTitle,
        string? Description,
        string? Author,
        IFormFile Thumbnail,
        int CountryId,
        string Status,
        string Audience,
        List<int>? GenresId,
        string? Slug
    ) : ICommand<Success>;
    public record UpdateStoryCommand(
    Guid StoryId,
    string StoryTitle,
    string? AnotherStoryTitle,
    string? Description,
    string? Author,
    IFormFile? Thumbnail, // Nullable in case of no update to the image
    int CountryId,
    string Status,
    string Audience,
    List<int>? GenresId,
    string? Slug) : ICommand<Success>;

    public record IncrementViewCountStoryCommand(Guid StoryId) : ICommand<Success>;
}
