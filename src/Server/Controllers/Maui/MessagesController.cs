using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleApp.Server.Database;
using SampleApp.Server.Database.Entities;
using SampleApp.Shared;

namespace SampleApp.Server.Controllers.Maui;

[ApiController]
[Route("maui/[controller]")]
public class MessagesController : ControllerBase
{
    readonly IDbContextFactory<SampleDbContext> dbFactory;

    public MessagesController(IDbContextFactory<SampleDbContext> dbFactory)
    {
        this.dbFactory = dbFactory;
    }

    [HttpGet]
    public async Task<MessageForCSharp[]> GetAll([FromQuery]int page = 0, [FromQuery]int pageSize = 0)
    {
		await using var db = await dbFactory.CreateDbContextAsync();
		var repository = db.GetMessageRepository();
		var messages = await repository.GetAllAsync(page, pageSize);

		return messages.Select(message => new MessageForCSharp
        {
            MessageId = message.MessageId,
            Title = message.Title,
            Contents = message.Contents,
            PublishDate = message.PublishDate.UtcDateTime
        }).ToArray();
    }

    [HttpGet("{id}")]
    public async Task<MessageForCSharp> Get(int id)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var repository = db.GetMessageRepository();
        var message = await repository.GetAsync(new MessageId(id));

        return new MessageForCSharp
        {
            MessageId = message.MessageId,
            Title = message.Title,
            Contents = message.Contents,
            PublishDate = message.PublishDate.UtcDateTime
        };
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] MessageForCSharp data)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var repository = db.GetMessageRepository();

        await repository.AddAsync(new Message
        {
            Title = data.Title,
            Contents = data.Contents
        });

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] MessageForCSharp data)
    {
        if (data.MessageId == MessageId.Empty)
        {
            return BadRequest();
        }

        await using var db = await dbFactory.CreateDbContextAsync();
        var repository = db.GetMessageRepository();

        await repository.UpdateAsync(new Message
        {
            MessageId = data.MessageId,
            Title = data.Title,
            Contents = data.Contents
        });

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var repository = db.GetMessageRepository();

        await repository.DeleteAsync(new MessageId(id));

        return Ok();
    }
}