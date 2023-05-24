using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository.UnitTests.Models;


internal class Class : IEntity<long>
{
    public long Id { get; set; }
    public string Name { get; set; }

    public IEnumerable<Student> Students { get; set; }
    public IEnumerable<Teacher> Teachers { get; set; }
}
