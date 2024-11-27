
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;
using static OnlineStory.Contract.Services.V1.Authentication.Query;
using static OnlineStory.Contract.Services.V1.Authentication.Response;

namespace OnlineStory.Application.UserCases.V1.Queries.Authentication;

public class GetMeQueryHandler : IQueryHandler<GetMeQuery, UserResponse>
{
    private readonly UserManager<AppUser> _userManager;
    public GetMeQueryHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }


    public async Task<Result<UserResponse>> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        // check user exist 
        var user = await _userManager.FindByIdAsync(request.UserId.ToString()); // convert Guid => string
        if(user is null)
        {
            return Error.Unauthorized(description: "User is not authenticated");
        }
        var role = await _userManager.GetRolesAsync(user);
        var userResponse = new UserResponse(
            Id: user.Id,
            UserName: user.UserName,
            Email: user.Email,
            FirstName: user.FirstName,
            LastName: user.LastName,
            DisplayName: user.DisplayName,
            ProfilePictureUrl: user.Avatar,
            Roles: role.ToList(),
            Gender: user.Gender.ToString(),
            Created: user.CreatedDate,
            Updated: user.ModifiedDate

        );
        return userResponse;
    }
}
