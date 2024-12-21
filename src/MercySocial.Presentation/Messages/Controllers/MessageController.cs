using AutoMapper;
using MediatR;
using MercySocial.Application.Messages.Commands;
using MercySocial.Presentation.Common.Controllers;
using MercySocial.Presentation.Messages.Requests;
using MercySocial.Presentation.Messages.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MercySocial.Presentation.Messages.Controllers;

public class MessagesController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public MessagesController(
        ISender mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessageAsync(
        SendMessageRequest request)
    {
        var sendMessageCommand = _mapper.Map<SendMessageCommand>(request);
        var sendMessageResult = await _mediator.Send(sendMessageCommand);
        return sendMessageResult.Match(
            result => Ok(_mapper.Map<MessageResponse>(result)),
            problem => Problem(problem));
    }

    // [HttpGet("conversation")]
    // public async Task<IActionResult> GetConversationAsync(
    //     Guid user1,
    //     Guid user2)
    // {
    //     var messages = await _messageService.GetConversationAsync(user1, user2);
    //     return Ok(messages);
    // }
    //
    // [HttpPatch("{id:guid}/read")]
    // public async Task<IActionResult> MarkAsReadAsync(
    //     Guid id)
    // {
    //     await _messageService.MarkAsReadAsync(id);
    //     return Ok(new {Status = "read"});
    // }
}