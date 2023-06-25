using Base.Entities.Abstract;

namespace Base.Entities.Concrete;

public class UserOperationClaim : IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserMail { get; set; }
    public int OperationClaimId { get; set; }
}
