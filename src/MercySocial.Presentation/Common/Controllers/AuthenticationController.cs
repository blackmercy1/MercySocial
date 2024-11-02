using AutoMapper;
using MediatR;
using MercySocial.Application.Common.Authentication;
using MercySocial.Application.Users.Commands.CreateLogin;
using MercySocial.Presentation.Users.Requests;
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
        [FromBody] CreateLoginRequest createLoginRequest, 
        CancellationToken cancellationToken)
    {
        var mappedLoginUser = _mapper.Map<CreateLoginCommand>(createLoginRequest);
        var authenticationResult = await _mediator.Send(mappedLoginUser, cancellationToken);
        if (authenticationResult.IsError)
            return Problem(authenticationResult.Errors);
        
        var token = _authenticationService.GenerateJwtToken(authenticationResult.Value.UserName);
        
        return Ok(token);
    }

    // [HttpPost("register")]
    // public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserDto registerUserDto)
    // {
    //     var mappedRegisterUserDto = _mapper.Map<RegisterUser>(registerUserDto);
    //     var registerResult = await _userService.RegisterUserAsync(mappedRegisterUserDto);
    // }
}