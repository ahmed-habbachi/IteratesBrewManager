using AutoMapper;
using AutoMapper.QueryableExtensions;
using IteratesBrewManager.Application.Common.Interfaces;
using IteratesBrewManager.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IteratesBrewManager.Application.Beers.Queries.GetBeers;

public record GetBeersByBreweryQuery : IRequest<IEnumerable<BreweryDto>>;

public class GetBeersByBreweryHandler : IRequestHandler<GetBeersByBreweryQuery, IEnumerable<BreweryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBeersByBreweryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BreweryDto>> Handle(GetBeersByBreweryQuery request, CancellationToken cancellationToken)
    {
        return await _context.Breweries.Include(b=>b.Beers)
                .AsNoTracking()
                .ProjectTo<BreweryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
    }
}
