using MediatR;
using ErrorOr;

namespace UserService.Application.Users.UpdateUser;

public record UpdateUserCommand(int Id, string Name) : IRequest<ErrorOr<bool>>;