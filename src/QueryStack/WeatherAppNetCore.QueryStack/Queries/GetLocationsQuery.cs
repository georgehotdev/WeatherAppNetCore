using MediatR;
using Microsoft.Extensions.Options;
using WeatherAppNetCore.Infrastructure.Configuration;

namespace WeatherAppNetCore.QueryStack.Queries;

public class GetLocationsQuery : IRequest<IEnumerable<string>>
{
}

public class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, IEnumerable<string>>
{
    private readonly WeatherApiConfig _weatherApiConfig;
    
    public GetLocationsQueryHandler(IOptions<WeatherApiConfig> weatherApiConfig)
    {
        _weatherApiConfig = weatherApiConfig.Value;
    }
    
    public Task<IEnumerable<string>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_weatherApiConfig.Locations.Select(x => x.City));
    }
}