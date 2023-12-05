using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository.IntegrationTests;

public class SoftDeleteModel : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset? Deleted { get; set; }
}
