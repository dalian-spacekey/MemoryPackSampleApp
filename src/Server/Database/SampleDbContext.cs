using System.Globalization;
using System.Reflection;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using SampleApp.Server.Database.Entities;
using SampleApp.Server.Database.Repositories;
using SampleApp.Server.Services;
using SampleApp.Shared;
using Person = SampleApp.Server.Database.Entities.Person;

namespace SampleApp.Server.Database;

public class SampleDbContext : DbContext
{
    public SampleDbContext() { }
    public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options) { }

    public DbSet<Person> Persons { get; set; } = default!;
    public DbSet<Message> Messages { get; set; } = default!;

    public PersonRepository GetPersonRepository() => new(this);
    public MessageRepository GetMessageRepository() => new(this);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite($"Data source=sample.db");
        }
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<MessageId>().HaveConversion<MessageId.MessageIdValueConverter>();
    }

    public async Task ImportAsync()
    {
        if (!Persons.Any())
        {
            using var reader = new StreamReader("Data\\dummy_persons.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<Person>();

            if (records == null) throw new InvalidOperationException("Person not found.");

            Persons.AddRange(records);
        }

        if (!Messages.Any())
        {
            var service = new RosanjinService();

            Messages.AddRange(Enumerable.Range(1, 100).Select(x =>
            {
                var publishDate = DateTimeOffset.Now.AddDays(-x);
                return new Message
                {
                    MessageId = new MessageId(x),
                    Title = $"{publishDate:yyyy/MM/dd}の日記",
                    Contents = string.Join($"{Environment.NewLine}{Environment.NewLine}", service.Generate(new Random().Next(1, 4))),
                    PublishDate = publishDate
                };
            }));
        }


        await SaveChangesAsync();
    }
}
