using Bogus;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Respawn;
using System.Data.Common;
using Testcontainers.MsSql;

namespace Viotto.DomainDrivenDesign.Repository.IntegrationTests;

public class SoftDeleteSetup<T> : IAsyncLifetime
    where T : DockerContainer, IDatabaseContainer
{
    public const int _seed = 27;

    public T DbContainer { get; private set; }
    public DbContext DbContext { get; private set; }
    public DbConnection DbConnection { get; private set; }
    public Func<Task> RespawnAsync { get; private set; }
    public Faker<SoftDeleteModel> FakeData { get; private set; }

    public async Task InitializeAsync()
    {
        DbContainer = (new MsSqlBuilder().Build() as T)!;
        await DbContainer.StartAsync();

        DbContext = new SoftDeleteContext(DbContainer.GetConnectionString());
        await DbContext.Database.EnsureCreatedAsync();

        FakeData = new Faker<SoftDeleteModel>()
            .RuleFor(x => x.Id, x => x.Random.Guid())
            .RuleFor(x => x.Name, x => x.Person.FullName)
            .UseSeed(_seed);

        DbConnection = DbContext.Database.GetDbConnection();
        await DbConnection.OpenAsync();

        var respawner = await Respawner.CreateAsync(DbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.SqlServer,
            WithReseed = true
        });

        RespawnAsync = () => respawner.ResetAsync(DbConnection);
    }

    public async Task DisposeAsync()
    {
        await DbConnection.CloseAsync();
        await DbContainer.StopAsync();
    }
}
