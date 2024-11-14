
using FluentValidation;
using static OnlineStory.Contract.Services.V1.Story.Command;

namespace OnlineStory.Contract.Services.V1.Story.Validators;

public class UpdateStoryValidator: AbstractValidator<UpdateStoryCommand>
{
    public UpdateStoryValidator()
    {
        RuleFor(x => x.StoryTitle).NotEmpty().NotNull();
        RuleFor(x => x.Audience).NotEmpty().NotNull();
        RuleFor(x => x.Status).NotEmpty().NotNull();
    }
}
