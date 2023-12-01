using Testcontainers.MsSql;

namespace Viotto.DomainDrivenDesign.Repository.IntegrationTests;


[CollectionDefinition(nameof(TestCollection))]
public class TestCollection : ICollectionFixture<TestSetup<MsSqlContainer>>
{
}
