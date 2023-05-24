using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository.UnitTests.Models;


internal class Student : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public long ClassId { get; set; }

    public Class Class { get; set; }
}
