using AutoMapper;
using MediatR;
using MercySocial.Application.Common.Authentication;
using MercySocial.Application.Users.Commands.CreateRegisterUser;
using MercySocial.Application.Users.Commands.CreateUserLogin;
using MercySocial.Presentation.Users.Requests;
using MercySocial.Presentation.Users.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MercySocial.Presentation.Common.Controllers;

public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(
        IAuthenticationService authenticationService,
        IMapper mapper,
        ISender mediator)
    {
        _authenticationService = authenticationService;
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync(
        [FromBody] CreateUserLoginRequest createUserLoginRequest,
        CancellationToken cancellationToken)
    {
        var mappedLoginUser = _mapper.Map<CreateUserLoginCommand>(createUserLoginRequest);
        var authenticationResult = await _mediator.Send(mappedLoginUser, cancellationToken);
        if (authenticationResult.IsError)
            return Problem(authenticationResult.Errors);

        var token = _authenticationService.GenerateJwtToken(authenticationResult.Value.UserName);

        return Ok(token);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAsync(
        [FromBody] CreateUserRegisterRequest createUserRegisterRequest,
        CancellationToken cancellationToken)
    {
        var registerCommand = _mapper.Map<CreateUserRegisterCommand>(createUserRegisterRequest);
        var registerResult = await _mediator.Send(registerCommand, cancellationToken);
        
        return registerResult.Match(
            result => Ok(_mapper.Map<UserResponse>(result)),
            error => Problem(error));
    }
}