
using FluentValidation;
using static OnlineStory.Contract.Services.V1.Chapter.Command;

namespace OnlineStory.Contract.Services.V1.Chapter.Validators;

public class UpdateChapterValidator: AbstractValidator<UpdateChapterCommand>
{
    public UpdateChapterValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.ChapterNumber).NotEmpty().WithMessage("Chapter number is requied").GreaterThanOrEqualTo(0).WithMessage("Chapter number must be greater than 0 or equal to 0");
        RuleFor(x => x.ChapterTitle).NotEmpty().WithMessage("Chapter title is requied");
        RuleFor(x => x.StoryId).NotEmpty().WithMessage("Story id is requied");
    }
}
