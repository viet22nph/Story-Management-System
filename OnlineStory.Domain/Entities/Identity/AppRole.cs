
using Microsoft.AspNetCore.Identity;
using OnlineStory.Domain.Abstractions.Entities;

namespace OnlineStory.Domain.Entities.Identity;

public class AppRole : IdentityRole<Guid>, IAuditable
{
    public string? Description { get; set; }
    public virtual ICollection<IdentityRoleClaim<Guid>> Claims { get; set; }
    public ICollection<AppUserRole> UserRoles { get; set; }
    //public virtual ICollection<IdentityUserRole<Guid>> UserRoles { get; set; }
    public virtual ICollection<Permission>? Permissions { get; set; }
    public Guid CreateBy { get; set; }
    public Guid? UpdateBy { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeleteAt { get; set; }
}
