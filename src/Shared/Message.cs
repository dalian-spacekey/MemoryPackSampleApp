using MemoryPack;
using UnitGenerator;

namespace SampleApp.Shared;

[MemoryPackable(GenerateType.VersionTolerant)]
public partial class MessageForCSharp
{
    [MemoryPackOrder(0)]
    public MessageId MessageId { get; set; }
    [MemoryPackOrder(1)]
    public string Title { get; set; } = default!;
    [MemoryPackOrder(2)]
    [Utf16StringFormatter]
    public string Contents { get; set; } = default!;
    [MemoryPackOrder(3)]
    public DateTime PublishDate { get; set; }
}

[MemoryPackable]
[GenerateTypeScript]
public partial class MessageForTypeScript
{
    [MemoryPackOrder(0)]
    public int MessageId { get; set; }
    [MemoryPackOrder(1)]
    public string Title { get; set; } = default!;
#pragma warning disable MEMPACK032 // not allow GenerateTypeScript type
    [MemoryPackOrder(2)]
    public string Contents { get; set; } = default!;
#pragma warning restore MEMPACK032
    [MemoryPackOrder(3)]
    public DateTime PublishDate { get; set; }
}

[MemoryPackable]
public partial class MessageForTypeScript2 //こっちで送り出す
{
    [MemoryPackOrder(0)]
    public int MessageId { get; set; }
    [MemoryPackOrder(1)]
    public string Title { get; set; } = default!;
    [MemoryPackOrder(2)]
    [Utf16StringFormatter]
    public string Contents { get; set; } = default!;
    [MemoryPackOrder(3)]
    public DateTime PublishDate { get; set; }
}

[UnitOf(typeof(int),
    UnitGenerateOptions.JsonConverter |
    UnitGenerateOptions.ParseMethod |
    UnitGenerateOptions.EntityFrameworkValueConverter)]
public partial struct MessageId
{
    public static MessageId Empty => new (0);
}
