using Microsoft.EntityFrameworkCore;
using SampleApp.Server.Database.Entities;
using SampleApp.Shared;

namespace SampleApp.Server.Database.Repositories;

public class PersonRepository : RepositoryBase
{
    DbSet<Person> Table => Context.Persons;

    public PersonRepository(SampleDbContext context) : base(context) { }

    public async Task<Person[]> GetAllAsync(int page = 0, int pageSize = 0, CancellationToken cancellationToken = default)
    {
        var query = Table;

        return await ExecuteGetAllQuery(query, page, pageSize, cancellationToken);
    }

    static async Task<Person[]> ExecuteGetAllQuery(
        IQueryable<Person> query, 
        int page = 0, 
        int pageSize = 0,
        CancellationToken cancellationToken = default)
    {
        query = query.OrderByDescending(x => x.PersonId);

        if (pageSize > 0)
        {
            query = query.Skip(page * pageSize).Take(pageSize);
        }

        return await query.ToArrayAsync(cancellationToken);
    }

    public async Task<Person> GetAsync(Guid personId, CancellationToken cancellationToken = default)
    {
        var person = await Table
            .SingleOrDefaultAsync(x => x.PersonId == personId, cancellationToken: cancellationToken);

        if (person == null)
        {
            throw new InvalidOperationException($"PersonId:{personId} is not exist.");
        }

        return person;
    }

    public async Task<Guid> AddAsync(Person item, CancellationToken cancellationToken = default)
    {
        await Table.AddAsync(item, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        return item.PersonId;
    }

    public async Task UpdateAsync(Person item, CancellationToken cancellationToken = default)
    {
        var person = await GetAsync(item.PersonId, cancellationToken);

        person.Name = item.Name;
        person.NameKana = item.NameKana;
        person.Age = item.Age;
        person.Birthday = item.Birthday;
        person.Gender = item.Gender;
        person.BloodType = item.BloodType;
        person.Email = item.Email;
        person.PhoneNumber = item.PhoneNumber;
        person.MobileNumber = item.MobileNumber;
        person.PostalCode = item.PostalCode;
        person.CompanyName = item.CompanyName;
        person.ApplicationDate = item.ApplicationDate;
        person.WithdrawalDate = item.WithdrawalDate;
    }

    public async Task DeleteAsync(Guid personId, CancellationToken cancellationToken = default)
    {
        var person = await GetAsync(personId, cancellationToken);
        Table.Remove(person);

        await Context.SaveChangesAsync(cancellationToken);
    }
}