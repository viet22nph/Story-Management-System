
using FluentValidation;
using static OnlineStory.Contract.Services.V1.Comment.Command;

namespace OnlineStory.Contract.Services.V1.Comment.Validator;

public class CreateCommentChapterValidator: AbstractValidator<CreateCommentChapterCommand>
{
    public CreateCommentChapterValidator()
    {

        RuleFor(x => x.Content).MinimumLength(1).NotEmpty();
        RuleFor(x => x.ChapterId).NotNull().NotEmpty();
        RuleFor(x => x.UserId).NotNull().NotEmpty();
    }
}
