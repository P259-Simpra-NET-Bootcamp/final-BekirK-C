using Base.Entities.Abstract;

namespace Base.Entities.Concrete;

public class OperationClaim : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}
