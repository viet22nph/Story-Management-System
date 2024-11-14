
using OnlineStory.Contract.Dtos.StoryDtos;
using OnlineStory.Domain.Abstractions.RepositoryBase;
using OnlineStory.Domain.Entities;

namespace OnlineStory.Application.Abstractions.Repository;

public interface IStoryRepository: IGenericRepository<Story, Guid>
{  /// <summary>
   /// Retrieves the details of a specific story, including its associated information.
   /// </summary>
   /// <param name="storyId">The unique identifier of the story.</param>
   /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="StoryDetailDto"/> object with the story details.</returns>
    Task<StoryDetailDto> GetStoryByIdAsync(Guid id);
    /// <summary>
    /// Retrieves the details of a specific story, including its associated information.
    /// </summary>
    /// <param name="slug">The unique, URL-friendly path that identifies each story.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="StoryDetailDto"/> object with the story details.</returns>
    Task<StoryDetailDto> GetStoryBySlugAsync(string slug);
}
