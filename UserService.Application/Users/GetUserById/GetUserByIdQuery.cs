using MediatR;
using ErrorOr;
using UserService.Application.DTOs;

namespace UserService.Application.Users.GetUserById;

public record GetUserByIdQuery(int Id) : IRequest<ErrorOr<UserDto>>;