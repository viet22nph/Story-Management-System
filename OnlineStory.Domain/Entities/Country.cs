

using OnlineStory.Domain.Abstractions;

namespace OnlineStory.Domain.Entities
{
    public class Country : EntityBase<int>
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }

        private readonly List<Story> _stories;
        public IReadOnlyCollection<Story> Stories=> _stories.AsReadOnly();

        private Country()
        {
            _stories = new List<Story>();
        }
        public Country(string countryCode, string countryName)
        {
            CountryCode = !string.IsNullOrWhiteSpace(countryCode)? countryCode : throw new ArgumentNullException(nameof(CountryCode));
            CountryName = !string.IsNullOrWhiteSpace(countryName) ? countryName : throw new ArgumentNullException(nameof(CountryName));
        }
    }
}
