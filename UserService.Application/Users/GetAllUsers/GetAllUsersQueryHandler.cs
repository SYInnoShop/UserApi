using Mapster;
using MediatR;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using UserService.Application.DTOs;
using UserService.Infrastructure.Context;

namespace UserService.Application.Users.GetAllUsers;

public class GetAllUsersQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetAllUsersQuery, ErrorOr<List<UserDto>>>
{
    public async Task<ErrorOr<List<UserDto>>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await dbContext.Users.ToListAsync(cancellationToken);
        
        return users.Adapt<List<UserDto>>();
    }
}