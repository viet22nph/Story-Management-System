namespace OnlineStory.Contract.Services.V1.Country;

public class Response
{
    public record CountryResponse(int Id, string CountryCode, string CountryName);
}
