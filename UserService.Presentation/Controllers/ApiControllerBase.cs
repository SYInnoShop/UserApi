using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiControllerBase : ControllerBase
{
    protected readonly IMediator Mediator;

    protected ApiControllerBase(IMediator mediator)
    {
        Mediator = mediator;
    }
}