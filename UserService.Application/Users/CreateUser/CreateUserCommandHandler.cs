using MediatR;
using ErrorOr;
using Mapster;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Infrastructure.Context;

namespace UserService.Application.Users.CreateUser;

public class CreateUserCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<CreateUserCommand, ErrorOr<User>>
{
    public async Task<ErrorOr<User>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var existingAdmin = await dbContext.Users
            .AnyAsync(u => u.Email == command.Email, cancellationToken);

        if (existingAdmin)
            return Error.Conflict(description: "Admin with this email already exists");
        

        var newAdmin = command.Adapt<User>();
        newAdmin.Role = "Admin";
        newAdmin.IsEmailConfirmed = true;
        
        await dbContext.Users.AddAsync(newAdmin, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return newAdmin;
    }
}