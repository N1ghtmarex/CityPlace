using Application.Users.Dtos;
using MediatR;

namespace Application.Users.Queries
{
    public class GetCurrentUserQuery : IRequest<UserViewModel>
    {
    }
}
