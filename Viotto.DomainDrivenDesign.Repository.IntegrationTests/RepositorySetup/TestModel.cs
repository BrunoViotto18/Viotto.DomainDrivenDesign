using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository.IntegrationTests;

public class TestModel : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public int LuckyNumber { get; set; }
}
