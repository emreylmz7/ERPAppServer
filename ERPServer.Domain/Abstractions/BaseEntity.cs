namespace ERPServer.Domain.Abstractions;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? UpdatedDate { get; set; }
    public Guid CreatedBy  { get; set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }
}
