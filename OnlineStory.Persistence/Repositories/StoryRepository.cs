using Microsoft.EntityFrameworkCore;
using OnlineStory.Application.Abstractions.Repository;
using OnlineStory.Contract.Dtos.CountryDtos;
using OnlineStory.Contract.Dtos.GenreDtos;
using OnlineStory.Contract.Dtos.RatingDtos;
using OnlineStory.Contract.Dtos.StoryDtos;
using OnlineStory.Domain.Entities;
using OnlineStory.Persistence.ApplicationDbContext;
using static Azure.Core.HttpHeader;

namespace OnlineStory.Persistence.Repositories;

public class StoryRepository : GenericRepository<Story, Guid>, IStoryRepository
{
    public StoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<StoryDetailDto> GetStoryByIdAsync(Guid id)
    {
        var story =  await _context.Stories
            .Where(x => x.Id == id)
            .Select(x=> new StoryDetailDto
            {
                Id = x.Id,
                StoryTitle = x.StoryTitle,
                AnotherStoryTitle = x.AnotherStoryTitle,
                Description = x.Description,
                Author = x.Author,
                Thumbnail = x.Thumbnail,
                Country = new CountryDto
                {
                    Id = x.Country.Id,
                    Code = x.Country.CountryCode,
                    Name = x.Country.CountryName,
                },
                StoryStatus = x.StoryStatus.ToString(),
                Audience = x.Audience.ToString(),
                Genres = x.StoryGenres.Select(genre => new GenreDto
                {
                    Id = genre.Genre.Id,
                    Description = genre.Genre.Description,
                    Name = genre.Genre.Name,
                }).ToList(),
                Slug = x.Slug,
                View = x.StoryViews.Sum(v => v.Count),
                NumberOfFollowers = x.FollowStories.Count(),
                CreatedDate = x.CreatedDate,
                ModifiedDate = x.ModifiedDate
            })
            .FirstOrDefaultAsync();
        return story;
    }

    public async Task<StoryDetailDto> GetStoryBySlugAsync(string slug)
    {
        var story = await _context.Stories
             .Where(x => x.Slug == slug)
             .Select(x => new StoryDetailDto
             {
                 Id = x.Id,
                 StoryTitle = x.StoryTitle,
                 AnotherStoryTitle = x.AnotherStoryTitle,
                 Description = x.Description,
                 Author = x.Author,
                 Thumbnail = x.Thumbnail,
                 Country = new CountryDto
                 {
                     Id = x.Country.Id,
                     Code = x.Country.CountryCode,
                     Name = x.Country.CountryName,
                 },
                 StoryStatus = x.StoryStatus.ToString(),
                 Audience = x.Audience.ToString(),
                 Genres = x.StoryGenres.Select(genre => new GenreDto
                 {
                     Id = genre.Genre.Id,
                     Description = genre.Genre.Description,
                     Name = genre.Genre.Name,
                 }).ToList(),
                 Slug =x.Slug,
                 View = x.StoryViews.Sum(v => v.Count),
                 NumberOfFollowers = x.FollowStories.Count(),
                 CreatedDate = x.CreatedDate,
                 ModifiedDate = x.ModifiedDate
             })
             .FirstOrDefaultAsync();
        return story;
    }
}
