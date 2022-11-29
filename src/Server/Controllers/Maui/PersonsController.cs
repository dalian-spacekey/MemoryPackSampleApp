using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleApp.Server.Database;
using SampleApp.Server.Database.Entities;
using SampleApp.Shared;

namespace SampleApp.Server.Controllers.Maui;

[ApiController]
[Route("maui/[controller]")]
public class PersonsController : ControllerBase
{
    readonly IDbContextFactory<SampleDbContext> dbFactory;

    public PersonsController(IDbContextFactory<SampleDbContext> dbFactory)
    {
        this.dbFactory = dbFactory;
    }

    [HttpGet]
    public async Task<PersonForCSharp[]> GetAll([FromQuery]int page = 0, [FromQuery]int pageSize = 0)
    {
		await using var db = await dbFactory.CreateDbContextAsync();
		var repository = db.GetPersonRepository();
		var messages = await repository.GetAllAsync(page, pageSize);

		return messages.Select(item => new PersonForCSharp
        {
            PersonId = item.PersonId,
            Name = item.Name,
            NameKana = item.NameKana,
            Age = item.Age,
            Birthday = item.Birthday,
            Gender = item.Gender,
            BloodType = item.BloodType,
            Email = item.Email,
            PhoneNumber = item.PhoneNumber,
            MobileNumber = item.MobileNumber,
            PostalCode = item.PostalCode,
            CompanyName = item.CompanyName,
            ApplicationDate = item.ApplicationDate,
            WithdrawalDate = item.WithdrawalDate
        }).ToArray();
    }

    [HttpGet("{id}")]
    public async Task<PersonForCSharp> Get(Guid id)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var repository = db.GetPersonRepository();
        var item = await repository.GetAsync(id);

        return new PersonForCSharp
        {
            PersonId = item.PersonId,
            Name = item.Name,
            NameKana = item.NameKana,
            Age = item.Age,
            Birthday = item.Birthday,
            Gender = item.Gender,
            BloodType = item.BloodType,
            Email = item.Email,
            PhoneNumber = item.PhoneNumber,
            MobileNumber = item.MobileNumber,
            PostalCode = item.PostalCode,
            CompanyName = item.CompanyName,
            ApplicationDate = item.ApplicationDate,
            WithdrawalDate = item.WithdrawalDate
        };
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] PersonForCSharp item)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var repository = db.GetPersonRepository();

        await repository.AddAsync(new Person
        {
            Name = item.Name,
            NameKana = item.NameKana,
            Age = item.Age,
            Birthday = item.Birthday,
            Gender = item.Gender,
            BloodType = item.BloodType,
            Email = item.Email,
            PhoneNumber = item.PhoneNumber,
            MobileNumber = item.MobileNumber,
            PostalCode = item.PostalCode,
            CompanyName = item.CompanyName,
            ApplicationDate = item.ApplicationDate,
            WithdrawalDate = item.WithdrawalDate
        });

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] PersonForCSharp item)
    {
        if (item.PersonId == Guid.Empty)
        {
            return BadRequest();
        }

        await using var db = await dbFactory.CreateDbContextAsync();
        var repository = db.GetPersonRepository();

        await repository.UpdateAsync(new Person
        {
            PersonId = item.PersonId,
            Name = item.Name,
            NameKana = item.NameKana,
            Age = item.Age,
            Birthday = item.Birthday,
            Gender = item.Gender,
            BloodType = item.BloodType,
            Email = item.Email,
            PhoneNumber = item.PhoneNumber,
            MobileNumber = item.MobileNumber,
            PostalCode = item.PostalCode,
            CompanyName = item.CompanyName,
            ApplicationDate = item.ApplicationDate,
            WithdrawalDate = item.WithdrawalDate
        });

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var repository = db.GetPersonRepository();

        await repository.DeleteAsync(id);

        return Ok();
    }
}