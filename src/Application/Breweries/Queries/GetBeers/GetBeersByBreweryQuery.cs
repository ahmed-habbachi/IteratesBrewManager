using AutoMapper;
using AutoMapper.QueryableExtensions;
using IteratesBrewManager.Application.Common.Interfaces;
using IteratesBrewManager.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IteratesBrewManager.Application.Beers.Queries.GetBeers;

public record GetBeersByBreweryQuery(int BreweryId) : IRequest<IEnumerable<BeerDto>>;

public class GetBeersByBreweryHandler : IRequestHandler<GetBeersByBreweryQuery, IEnumerable<BeerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBeersByBreweryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BeerDto>> Handle(GetBeersByBreweryQuery request, CancellationToken cancellationToken)
    {
        return await _context.Beers.Where(b => b.BrewerId == request.BreweryId)
                .AsNoTracking()
                .ProjectTo<BeerDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
    }
}
