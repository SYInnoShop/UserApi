using MediatR;
using ErrorOr;
using UserService.Application.DTOs;

namespace UserService.Application.Users.GetAllUsers;

public record GetAllUsersQuery : IRequest<ErrorOr<List<UserDto>>>;