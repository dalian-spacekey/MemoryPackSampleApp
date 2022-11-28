namespace SampleApp.Server.Database.Repositories;

public class RepositoryBase
{
    protected SampleDbContext Context;

    public RepositoryBase(SampleDbContext context)
    {
        Context = context;
    }
}