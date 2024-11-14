

using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace OnlineStory.Domain.Entities.Identity;

public class AppUserRole : IdentityUserRole<Guid>
{

    public virtual AppUser User { get; set; }
    public virtual AppRole Role { get; set; }
}
