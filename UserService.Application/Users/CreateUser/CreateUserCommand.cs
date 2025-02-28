using MediatR;
using ErrorOr;
using UserService.Domain.Entities;

namespace UserService.Application.Users.CreateUser;

public record CreateUserCommand(string Name, string Email, string Password) : IRequest<ErrorOr<User>>;