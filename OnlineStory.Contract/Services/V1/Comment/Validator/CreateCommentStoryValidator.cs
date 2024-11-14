using FluentValidation;
using static OnlineStory.Contract.Services.V1.Comment.Command;

namespace OnlineStory.Contract.Services.V1.Comment.Validator
{
    public class CreateCommentStoryValidator: AbstractValidator<CreateCommentStoryCommand>
    {
        public CreateCommentStoryValidator()
        {
            RuleFor(x => x.Content).MinimumLength(1).NotEmpty();
            RuleFor(x=> x.StoryId).NotNull().NotEmpty();
            RuleFor(x=> x.UserId).NotNull().NotEmpty();
        }

    }
}
