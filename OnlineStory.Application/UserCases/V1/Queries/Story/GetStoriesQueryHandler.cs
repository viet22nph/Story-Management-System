

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineStory.Application.Abstractions;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using OnlineStory.Domain.Abstractions.RepositoryBase;
using static OnlineStory.Contract.Services.V1.Story.Query;
using static OnlineStory.Contract.Services.V1.Story.Response;

namespace OnlineStory.Application.UserCases.V1.Queries.Story;

public class GetStoriesQueryHandler : IQueryHandler<GetStoriesQuery, Pagination<StoryResponse>>
{


    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public GetStoriesQueryHandler(IUnitOfWork unitOfWork,      
         IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<Result<Pagination<StoryResponse>>> Handle(GetStoriesQuery request, CancellationToken cancellationToken)
    {
        string sortKeyExpression = request?.SortColumn?.ToLower() switch
        {
            "id" => nameof(Domain.Entities.Story.Id),
            "title" => nameof(Domain.Entities.Story.StoryTitle),
            _ => nameof(Domain.Entities.Story.ModifiedDate)
        };
        string sortOrder = request!.SortOrder == SortOrder.Ascending ? "ASC" : "DESC";
          string sqlQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
        ? $"SELECT * FROM {nameof(Domain.Entities.Story)} ORDER BY {sortKeyExpression} {sortOrder} OFFSET {(request.PageIndex - 1) * request.PageSize} ROWS FETCH NEXT {request.PageSize} ROWS ONLY"
        : $"SELECT * FROM {nameof(Domain.Entities.Story)} WHERE StoryTitle COLLATE Vietnamese_CI_AI LIKE {{0}} OR Description COLLATE Vietnamese_CI_AI LIKE {{0}} ORDER BY {sortKeyExpression} {sortOrder} OFFSET {(request.PageIndex - 1) * request.PageSize} ROWS FETCH NEXT {request.PageSize} ROWS ONLY";
        var stories = string.IsNullOrWhiteSpace(request.SearchTerm)
        ? await _unitOfWork.StoryRepository.FromSqlRaw(sqlQuery)
        .Include(s => s.Country)
        .Include(s => s.StoryGenres)
            .ThenInclude(sg => sg.Genre)
        .ToListAsync()
        : await _unitOfWork.StoryRepository.FromSqlRaw(sqlQuery, $"%{request.SearchTerm}%").Include(s => s.Country)
        .Include(s => s.StoryGenres)
            .ThenInclude(sg => sg.Genre)
        .ToListAsync();
        var httpRequest = _httpContextAccessor.HttpContext.Request;
        var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";
        var storiesResponse = stories.Select(x => new StoryResponse(
                   Id: x.Id,
                   Title: x.StoryTitle,
                   SubTitle: x.AnotherStoryTitle??"",
                   Description: x.Description,
                   Author: x.Author,
                   Thumbnail: $"{baseUrl}/{x.Thumbnail}",
                   Slug: x.Slug,
                   Status: x.StoryStatus.ToString(),
                   Genres: x.StoryGenres.Select(sg=> sg.Genre.Name).ToList(),
                   Country: x.Country.CountryName,
                   Nsfw: false,
                   CreateAt: x.CreatedDate,
                   UpdateAt: x.ModifiedDate
                   
           )
       ).ToList();
        
        return Pagination<StoryResponse>.Create(storiesResponse, request.PageIndex, request.PageSize, 1);
    }
}
