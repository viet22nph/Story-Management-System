
using Microsoft.EntityFrameworkCore;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using OnlineStory.Domain.Abstractions.RepositoryBase;
using System.Linq.Expressions;
using static OnlineStory.Contract.Services.V1.Country.Query;
using static OnlineStory.Contract.Services.V1.Country.Response;

namespace OnlineStory.Application.UserCases.V1.Queries.Country;

public class GetContriesQueryHandler : IQueryHandler<GetCountiesQuery, List<CountryResponse>>
{
    private readonly IGenericRepository<OnlineStory.Domain.Entities.Country, int> _countryRepository;
    public GetContriesQueryHandler(IGenericRepository<OnlineStory.Domain.Entities.Country, int> countryRepository)
    {
        _countryRepository = countryRepository;
    }
    public async Task<Result<List<CountryResponse>>> Handle(GetCountiesQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Domain.Entities.Country, object>> sortKeyExpression = request?.SortColumn?.ToLower() switch
        {
            "id" => country => country.Id,
            _ => country => country.CountryName // Default column -> country name
        };
        var countries =  _countryRepository.FindAll();
        countries = request?.SortBy == SortOrder.Ascending ? countries.OrderBy(sortKeyExpression) :
           countries.OrderByDescending(sortKeyExpression);
        var countryResponse = await countries.Select( x=> new CountryResponse
        (
            x.Id,
           x.CountryName,
           x.CountryCode
        )).ToListAsync();
        return countryResponse;
    }
}
    