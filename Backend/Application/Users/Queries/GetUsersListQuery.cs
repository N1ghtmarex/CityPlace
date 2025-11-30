using Application.BaseModels;
using Application.Users.Dtos;
using Core.EntityFramework.Features.SearchPagination.Models;
using MediatR;

namespace Application.Users.Queries
{
    public class GetUsersListQuery : SearchablePagedQuery, IRequest<PagedResult<UserListViewModel>>
    {
    }
}
