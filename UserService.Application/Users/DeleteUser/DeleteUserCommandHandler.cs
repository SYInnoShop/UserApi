using MediatR;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Context;

namespace UserService.Application.Users.DeleteUser;

public class DeleteUserCommandHandler(ApplicationDbContext dbContext) 
    : IRequestHandler<DeleteUserCommand, ErrorOr<bool>>
{
    public async Task<ErrorOr<bool>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == command.Id, cancellationToken);

        if (user == null)
        {
            return Error.NotFound(description: $"User with id {command.Id} not found");
        }

        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}