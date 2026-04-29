using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

[ApiController]
[Route("api/messages")]
public class MessageController : ControllerBase
{
    private static readonly List<Message> _messages = new();
    private readonly IWebSocketNotifier _notifier;

    public MessageController(IWebSocketNotifier notifier)
    {
        _notifier = notifier;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] CreateMessageDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Content))
            return BadRequest("Message cannot be empty");

        var message = new Message
        {
            Id = Guid.NewGuid(),
            ChannelId = dto.ChannelId,
            Content = dto.Content,
            CreatedAt = DateTime.UtcNow
        };

        _messages.Add(message);

        await _notifier.NotifyChannelAsync(dto.ChannelId, message);

        return Ok(message);
    }
}

public class CreateMessageDto
{
    public string ChannelId { get; set; }
    public string Content { get; set; }
}

public class Message
{
    public Guid Id { get; set; }
    public string ChannelId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}

public interface IWebSocketNotifier
{
    Task NotifyChannelAsync(string channelId, Message message);
}
