using AutoMapper;
using AutoMapper.QueryableExtensions;
using IteratesBrewManager.Application.Common.Interfaces;
using IteratesBrewManager.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IteratesBrewManager.Application.Breweries.Queries.GetBreweries;

public record GetBreweriesQuery : IRequest<IEnumerable<BreweryDto>>;

public class GetBreweriesQueryHandler : IRequestHandler<GetBreweriesQuery, IEnumerable<BreweryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBreweriesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BreweryDto>> Handle(GetBreweriesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Breweries
                .AsNoTracking()
                .ProjectTo<BreweryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
    }
}
