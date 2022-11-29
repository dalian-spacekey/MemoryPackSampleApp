using Microsoft.EntityFrameworkCore;
using SampleApp.Server.Database.Entities;
using SampleApp.Shared;

namespace SampleApp.Server.Database.Repositories;

public class MessageRepository : RepositoryBase
{
    DbSet<Message> Table => Context.Messages;
    
    public MessageRepository(SampleDbContext context) : base(context) { }

    public async Task<Message[]> GetAllAsync(int page = 0, int pageSize = 0, CancellationToken cancellationToken = default)
    {
        var query = Table;

        return await ExecuteGetAllQuery(query, page, pageSize, cancellationToken);
    }

    static async Task<Message[]> ExecuteGetAllQuery(
        IQueryable<Message> query, 
        int page = 0, 
        int pageSize = 0,
        CancellationToken cancellationToken = default)
    {
        query = query.OrderByDescending(x => x.MessageId);

        if (pageSize > 0)
        {
            query = query.Skip(page * pageSize).Take(pageSize);
        }

        return await query.ToArrayAsync(cancellationToken);
    }

    public async Task<Message> GetAsync(MessageId id, CancellationToken cancellationToken = default)
    {
        var message = await Table
            .SingleOrDefaultAsync(x => x.MessageId == id, cancellationToken: cancellationToken);

        if (message == null)
        {
            throw new InvalidOperationException($"MessageId:{id} is not exist.");
        }

        return message;
    }

    public async Task<MessageId> AddAsync(Message item, CancellationToken cancellationToken = default)
    {
        await Table.AddAsync(item, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        return item.MessageId;
    }

    public async Task UpdateAsync(Message item, CancellationToken cancellationToken = default)
    {
        var message = await GetAsync(item.MessageId, cancellationToken);

        message.Title = item.Title;
        message.Contents = item.Contents;

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MessageId id, CancellationToken cancellationToken = default)
    {
        var message = await GetAsync(id, cancellationToken);
        Table.Remove(message);

        await Context.SaveChangesAsync(cancellationToken);
    }
}