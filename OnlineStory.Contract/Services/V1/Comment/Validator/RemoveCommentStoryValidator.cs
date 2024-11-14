

using FluentValidation;
using static OnlineStory.Contract.Services.V1.Comment.Command;

namespace OnlineStory.Contract.Services.V1.Comment.Validator;

public class RemoveCommentStoryValidator: AbstractValidator<RemoveCommentStoryCommand>
{
    public RemoveCommentStoryValidator()
    {
        RuleFor(x=> x.StoryId).NotEmpty().NotNull();
    }
}
