using AutoMapper;
using MediatR;
using MercySocial.Application.Users.Commands.CreateUser;
using MercySocial.Presentation.Common.Controllers;
using MercySocial.Presentation.Users.Requests;
using MercySocial.Presentation.Users.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MercySocial.Presentation.Users.Controllers;

public class UserController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public UserController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(
        CreateUserRequest createUserRequest,
        CancellationToken cancellationToken)
    {
        var createUserCommand = _mapper.Map<CreateUserCommand>(createUserRequest);
        var createUserResult = await _mediator.Send(createUserCommand, cancellationToken);
        
        return createUserResult.Match(
            result => Ok(_mapper.Map<RegisterResponse>(result)),
            error => Problem(error));
    }
}
