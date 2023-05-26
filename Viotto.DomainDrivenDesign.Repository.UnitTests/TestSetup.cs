using Bogus;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;
using System.Data.Common;

namespace Viotto.DomainDrivenDesign.Repository.UnitTests;

using Contexts;
using Models;
using Repositories;
using Respawn;


public class TestSetup : IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder().Build();
    private DbConnection _dbConnection;
    private Respawner _respawner;

    public TestContext TestContext { get; set; }

    public TestRepository TestRepository { get; set; }

    public Faker<Test> TestGenerator { get; set; }


    public TestSetup()
    {
        var seed = 42;

        TestContext = new TestContext();

        TestRepository = new TestRepository(TestContext);

        TestGenerator = new Faker<Test>()
            .RuleFor(x => x.Name, x => x.Person.FullName)
            .RuleFor(x => x.DateOfBirth, x => x.Date.BetweenDateOnly(new DateOnly(1960, 1, 1), new DateOnly(2020, 12, 31)))
            .RuleFor(x => x.LuckyNumber, x => x.Random.Int(0, 100))
            .UseSeed(seed);
    }


    public async Task RespawnDatabase()
    {
        await _respawner.ResetAsync(_dbConnection);
    }


    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        _dbConnection = new SqlConnection(_dbContainer.GetConnectionString());

        await InitializeContext();
        await InitializeRespawner();
    }

    public async Task InitializeContext()
    {
        TestContext = new TestContext(x => x.UseSqlServer(_dbContainer.GetConnectionString()));
        _dbConnection = TestContext.Database.GetDbConnection();
        await TestContext.Database.EnsureCreatedAsync();
    }

    public async Task InitializeRespawner()
    {
        await _dbConnection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.SqlServer,
            SchemasToInclude = new[] { "dbo" },
            WithReseed = true
        });
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
}
