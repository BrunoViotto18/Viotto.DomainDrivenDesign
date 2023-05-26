using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository.UnitTests.Models;


public class Test : IEntity<long>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public int LuckyNumber { get; set; }
}
