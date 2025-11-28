using Application.Mappers;
using Application.Users.Dtos;
using Application.Users.Queries;
using Core.EntityFramework.Features.SearchPagination;
using Core.EntityFramework.Features.SearchPagination.Models;
using Core.Exceptions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Handlers
{
    internal class UserQueriesHandler(ApplicationDbContext dbContext) :
        IRequestHandler<GetUserQuery, UserViewModel>, IRequestHandler<GetUsersListQuery, PagedResult<UserListViewModel>>
    {
        public async Task<UserViewModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users
                .AsNoTracking()
                .ProjectToViewModel()
                .SingleOrDefaultAsync(x => x.Id == request.UserId, cancellationToken)
                ?? throw new ObjectNotFoundException($"Пользователь с идентификатором {request.UserId} не найден!"); ;

            return user;
        }

        public async Task<PagedResult<UserListViewModel>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var userQuery = dbContext.Users
                .AsNoTracking()
                .OrderBy(x => x.Username)
                .ApplySearch(request, x => x.Username);

            var result = await userQuery
                .ApplyPagination(request)
                .ProjectToListViewModel()
                .ToListAsync(cancellationToken);

            return result.AsPagedResult(request, await userQuery.CountAsync(cancellationToken));
        }
    }
}
