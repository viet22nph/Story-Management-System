

namespace OnlineStory.Domain.Entities.Identity;

public class Resource: Abstractions.EntityBase<int>
{
    public  string Name { get; set; }

    private List<Permission> _permissions;
    public IReadOnlyCollection<Permission>? Permissions => _permissions.AsReadOnly();
    private Resource() { 
        _permissions = new List<Permission>();
    }

    public Resource(string name): this()
    {
        Name = name;
    }
}
