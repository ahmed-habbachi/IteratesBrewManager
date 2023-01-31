using AutoMapper;
using AutoMapper.QueryableExtensions;
using IteratesBrewManager.Application.Common.Interfaces;
using IteratesBrewManager.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IteratesBrewManager.Application.Beers.Queries.GetBeers;

public record GetBeersQuery : IRequest<IEnumerable<BeerDto>>;

public class GetBeersQueryHandler : IRequestHandler<GetBeersQuery, IEnumerable<BeerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBeersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BeerDto>> Handle(GetBeersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Beers
                .AsNoTracking()
                .ProjectTo<BeerDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
    }
}
