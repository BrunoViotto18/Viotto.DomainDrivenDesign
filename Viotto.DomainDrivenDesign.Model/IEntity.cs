namespace Viotto.DomainDrivenDesign.Model;


public interface IEntity<TId>
{
    public TId Id { get; }
}
