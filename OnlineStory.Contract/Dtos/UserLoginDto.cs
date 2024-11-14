namespace OnlineStory.Contract.Dtos
{
    public class UserLoginDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string? Avatar { get; set; }
        public string DisplayName { get; set; }
    }
}
