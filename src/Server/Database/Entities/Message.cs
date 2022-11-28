using SampleApp.Shared;

namespace SampleApp.Server.Database.Entities;

public class Message
{
    public MessageId MessageId { get; set; }
    public string Title { get; set; } = default!;
    public string Contents { get; set; } = default!;
    public DateTimeOffset PublishDate { get; set; }
}