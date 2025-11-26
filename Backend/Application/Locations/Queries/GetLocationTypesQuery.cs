using Application.BaseModels;
using Application.Locations.Dtos;
using Core.EntityFramework.Features.SearchPagination.Models;
using MediatR;

namespace Application.Locations.Queries
{
    public class GetLocationTypesQuery : IRequest<List<LocationTypesViewModel>>
    {
    }
}
