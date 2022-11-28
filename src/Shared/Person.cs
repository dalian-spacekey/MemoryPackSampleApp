using MemoryPack;

namespace SampleApp.Shared;

[MemoryPackable(GenerateType.VersionTolerant)]
public partial class PersonForCSharp
{
    [MemoryPackOrder(0)]
    public Guid PersonId { get; set; } = default!;
    [MemoryPackOrder(1)]
    public string Name { get; set; } = default!;
    [MemoryPackOrder(2)]
    public string NameKana { get; set; } = default!;
    [MemoryPackOrder(3)]
    public int Age { get; set; }
    [MemoryPackOrder(4)]
    public DateTime Birthday { get; set; }
    [MemoryPackOrder(5)]
    public string Gender { get; set; } = default!;
    [MemoryPackOrder(6)]
    public string BloodType { get; set; } = default!;
    [MemoryPackOrder(7)]
    public string Email { get; set; } = default!;
    [MemoryPackOrder(8)]
    public string PhoneNumber { get; set; } = default!;
    [MemoryPackOrder(9)]
    public string MobileNumber { get; set; } = default!;
    [MemoryPackOrder(10)]
    public string PostalCode { get; set; } = default!;
    [MemoryPackOrder(11)]
    public string Address { get; set; } = default!;
    [MemoryPackOrder(12)]
    public string CompanyName { get; set; } = default!;
    [MemoryPackOrder(13)]
    public DateTime ApplicationDate { get; set; } = default!;
    [MemoryPackOrder(14)]
    public DateTime? WithdrawalDate { get; set; } = default!;
}

[MemoryPackable]
[GenerateTypeScript]
public partial class PersonForTypeScript
{
    [MemoryPackOrder(0)]
    public Guid PersonId { get; set; } = default!;
    [MemoryPackOrder(1)]
    public string Name { get; set; } = default!;
    [MemoryPackOrder(2)]
    public string NameKana { get; set; } = default!;
    [MemoryPackOrder(3)]
    public int Age { get; set; }
    [MemoryPackOrder(4)]
    public DateTime Birthday { get; set; }
    [MemoryPackOrder(5)]
    public string Gender { get; set; } = default!;
    [MemoryPackOrder(6)]
    public string BloodType { get; set; } = default!;
    [MemoryPackOrder(7)]
    public string Email { get; set; } = default!;
    [MemoryPackOrder(8)]
    public string PhoneNumber { get; set; } = default!;
    [MemoryPackOrder(9)]
    public string MobileNumber { get; set; } = default!;
    [MemoryPackOrder(10)]
    public string PostalCode { get; set; } = default!;
    [MemoryPackOrder(11)]
    public string Address { get; set; } = default!;
    [MemoryPackOrder(12)]
    public string CompanyName { get; set; } = default!;
    [MemoryPackOrder(13)]
    public DateTime ApplicationDate { get; set; } = default!;
    [MemoryPackOrder(14)]
    public DateTime? WithdrawalDate { get; set; } = default!;
}