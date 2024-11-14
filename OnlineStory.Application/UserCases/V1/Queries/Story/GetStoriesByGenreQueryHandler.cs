
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineStory.Application.Abstractions;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;
using OnlineStory.Domain.Entities;
using OnlineStory.Domain.Enums;
using System.Linq.Expressions;
using static OnlineStory.Contract.Services.V1.Story.Query;
using static OnlineStory.Contract.Services.V1.Story.Response;

namespace OnlineStory.Application.UserCases.V1.Queries.Story;

public class GetStoriesByGenreQueryHandler : IQueryHandler<GetStoriesByGenreQuery, Pagination<StoryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public GetStoriesByGenreQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<Result<Pagination<StoryResponse>>> Handle(GetStoriesByGenreQuery request, CancellationToken cancellationToken)
    {
        var genre = await _unitOfWork.GenreRepository.FindSingleAsync(x=> x.Slug == request.GenreSlug);
        if (genre is null) {
            return Error.NotFound(description: "Not found genre");
        }
        Expression<Func<Domain.Entities.Story, object>> sortKeyExpression = request?.SortColumn?.ToLower() switch
        {
            "title" => x => x.StoryTitle,
            _ => x => x.ModifiedDate     // Mặc định sắp xếp theo ngày sửa đổi
        };

        var query = _unitOfWork.StoryRepository
        .FindAll(x => x.StoryGenres.Any(x=>x.GenreId == genre.Id)).AsNoTracking();
        if (!string.IsNullOrEmpty(request.Status))
        {
            // Chuyển đổi string thành enum
            if (Enum.TryParse<StoryStatus>(request.Status, true, out var storyStatusEnum))
            {
                // Lọc theo trạng thái của câu chuyện nếu chuyển đổi thành công
                query = query.Where(x => x.StoryStatus == storyStatusEnum);
            }
            else
            {
                // default 
                query = query.Where(x => x.StoryStatus == StoryStatus.Updating);
            }
        }
        query = request.SortOrder == SortOrder.Descending ? query.OrderByDescending(sortKeyExpression) : query.OrderBy(sortKeyExpression);
        //  join các bản liên quan
        query = query.Include(x => x.Country).Include(x => x.StoryGenres).ThenInclude(y => y.Genre);

        var httpRequest = _httpContextAccessor.HttpContext.Request;
        var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";
        var stories = await query.Skip((request.PageIndex -1)* request.PageSize)
            .Take(request.PageSize)
            .Select(x=>  new StoryResponse(x.Id,
            x.StoryTitle,
            x.AnotherStoryTitle, 
            x.Description, 
            x.Author,
            $"{baseUrl}/{x.Thumbnail}",
            x.Slug, 
            x.StoryStatus.ToString(),
            x.StoryGenres.Select(x=> x.Genre.Name).ToList(),
            x.Country.CountryName,
            false,
            x.CreatedDate,
            x.ModifiedDate)
            ).ToListAsync();
        var count = await query.CountAsync(cancellationToken);
        return Pagination<StoryResponse>.Create(stories, request.PageIndex, request.PageSize, count);
    }
}
