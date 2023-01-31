using AutoMapper;
using AutoMapper.QueryableExtensions;
using IteratesBrewManager.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace IteratesBrewManager.Application.Common.Mappings;

public static class MappingExtensions
{
    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration) where TDestination : class
        => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();
}
