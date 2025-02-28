using Mapster;
using MediatR;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using UserService.Application.DTOs;
using UserService.Infrastructure.Context;

namespace UserService.Application.Users.GetUserById;

public class GetUserByIdQueryHandler(ApplicationDbContext dbContext) 
    : IRequestHandler<GetUserByIdQuery, ErrorOr<UserDto>>
{
    public async Task<ErrorOr<UserDto>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == query.Id, cancellationToken);
        
        if (user == null)
            return Error.NotFound(description: $"User with id {query.Id} not found");

        return user.Adapt<UserDto>();
    }
}