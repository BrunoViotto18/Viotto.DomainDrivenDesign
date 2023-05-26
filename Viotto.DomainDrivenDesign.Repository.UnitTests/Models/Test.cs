using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository.UnitTests.Models;


public class Test : IEntity<long>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public int LuckyNumber { get; set; }


    public override bool Equals(object obj)
    {
        if (obj is not Test test)
            return false;

        return test.Id == Id && test.Name == Name && test.DateOfBirth == DateOfBirth && test.LuckyNumber == LuckyNumber;
    }
}
