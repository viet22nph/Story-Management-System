

using FluentValidation;
using static OnlineStory.Contract.Services.V1.Comment.Command;

namespace OnlineStory.Contract.Services.V1.Comment.Validator;

public class RemoveCommentChapterValidator: AbstractValidator<RemoveCommentChapterCommand>
{
    public RemoveCommentChapterValidator()
    {
        RuleFor(x => x.ChapterId).NotEmpty().NotNull().GreaterThanOrEqualTo(1);
    }
}
