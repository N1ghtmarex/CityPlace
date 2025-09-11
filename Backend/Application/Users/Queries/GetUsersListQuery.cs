using Application.Abstractions.Models;
using Application.BaseModels;
using Core.EntityFramework.Features.SearchPagination.Models;
using MediatR;

namespace Application.Users.Queries
{
    public class GetUsersListQuery : SearchablePagedQuery, IRequest<PagedResult<UserListViewModel>>
    {
    }
}
