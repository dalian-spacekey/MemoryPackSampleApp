using CsvHelper.Configuration.Attributes;
using MemoryPack;

namespace SampleApp.Server.Database.Entities;

public class Person
{
    public Guid PersonId { get; set; } = default!;

    public string Name { get; set; } = default!;
    public string NameKana { get; set; } = default!;
    public int Age { get; set; }
    [Format("yyyy/MM/dd")]
    public DateTime Birthday { get; set; }
    public string Gender { get; set; } = default!;
    public string BloodType { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string MobileNumber { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    [Utf16StringFormatter]
    public string Address { get; set; } = default!;
    public string CompanyName { get; set; } = default!;

    [Format("yyyy/MM/dd")]
    public DateTime ApplicationDate { get; set; } = default!;
    [Format("yyyy/MM/dd")]
    public DateTime? WithdrawalDate { get; set; } = default!;
}