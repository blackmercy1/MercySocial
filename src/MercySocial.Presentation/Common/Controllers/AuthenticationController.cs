using AutoMapper;
using MediatR;
using MercySocial.Application.Users.Commands.CreateRegisterUser;
using MercySocial.Application.Users.Queries.UserLogin.Queries;
using MercySocial.Domain.UserAggregate;
using MercySocial.Presentation.Common.Authentication;
using MercySocial.Presentation.Users.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MercySocial.Presentation.Common.Controllers;

[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(
        IMapper mapper,
        ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync(
        [FromBody] UserLoginRequest userLoginRequest,
        CancellationToken cancellationToken)
    {
        var mappedLoginUser = _mapper.Map<UserLoginQuery>(userLoginRequest);
        var authenticationResult = await _mediator.Send(mappedLoginUser, cancellationToken);
        if (authenticationResult.IsError)
            return Problem(authenticationResult.Errors);

        return authenticationResult.Match(
            result => Ok(_mapper.Map<AuthenticationResponse>(result)),
            error => Problem(error));
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAsync(
        [FromBody] CreateUserRegisterRequest createUserRegisterRequest,
        CancellationToken cancellationToken)
    {
        var registerCommand = _mapper.Map<CreateUserRegisterCommand>(createUserRegisterRequest);
        var registerResult = await _mediator.Send(registerCommand, cancellationToken);
        
        return registerResult.Match(
            result => Ok(_mapper.Map<User>(result)),
            error => Problem(error));
    }
}