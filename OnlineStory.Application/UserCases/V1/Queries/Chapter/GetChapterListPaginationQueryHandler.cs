

using Microsoft.EntityFrameworkCore;
using OnlineStory.Application.Abstractions;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;
using OnlineStory.Domain.Entities;
using System.Collections.Generic;
using static OnlineStory.Contract.Services.V1.Chapter.Query;
using static OnlineStory.Contract.Services.V1.Chapter.Response;

namespace OnlineStory.Application.UserCases.V1.Queries.Chapter
{
    public class GetChapterListPaginationQueryHandler : IQueryHandler<GetChapterListPaginationQuery, Pagination<ChapterResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetChapterListPaginationQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<Pagination<ChapterResponse>>> Handle(GetChapterListPaginationQuery request, CancellationToken cancellationToken)
        {
            var story = await _unitOfWork.StoryRepository.FindSingleAsync(x=> x.Slug == request.StorySlug);
            if (story is null)
            {
                return Error.NotFound(description: "Not found story");
            }
            var query = _unitOfWork.ChapterRepository.FindAll(x => x.StoryId == story.Id);
            var chapters = await query
                .OrderBy(x=> x.CreatedDate)
                .Skip((request.PageIndex-1)* request.PageSize)
                .Take(request.PageSize)
                .Select(x=> new ChapterResponse(x.Id, x.ChapterNumber, x.ChapterTitle,x.Slug,x.CreatedDate))
                .ToListAsync(cancellationToken);
            int count = query.Count();
            return Pagination<ChapterResponse>.Create(chapters, request.PageIndex, request.PageSize, count);
        }
    }
}
