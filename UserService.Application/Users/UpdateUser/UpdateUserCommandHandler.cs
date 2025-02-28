using MediatR;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Context;

namespace UserService.Application.Users.UpdateUser;

public class UpdateUserCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<UpdateUserCommand, ErrorOr<bool>>
{
    public async Task<ErrorOr<bool>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == command.Id, cancellationToken);

        if (user == null)
            return Error.NotFound(description: $"User with id {command.Id} not found");

        user.Name = command.Name;
        
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}