using System.Reflection;

namespace OnlineStory.Infrastructure.MessageQueue;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
