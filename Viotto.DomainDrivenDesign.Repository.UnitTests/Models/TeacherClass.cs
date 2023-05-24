using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository.UnitTests.Models;


internal class TeacherClass : IEntity<long>
{
    public long Id { get; set; }
    public Guid TeacherId { get; set; }
    public long ClassId { get; set; }

    public Teacher Teacher { get; set; }
    public Class Class { get; set; }
}
