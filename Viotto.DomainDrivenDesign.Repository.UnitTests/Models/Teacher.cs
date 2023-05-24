using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository.UnitTests.Models;


internal class Teacher : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public IEnumerable<Class> Classes { get; set; }
}
