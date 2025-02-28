using MediatR;
using ErrorOr;

namespace UserService.Application.Users.DeleteUser;

public record DeleteUserCommand(int Id) : IRequest<ErrorOr<bool>>;