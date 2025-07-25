namespace Shared.Bases;

public abstract class BaseEntity
{
    public DateTime? CreatedDate { get; set; }
    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
    public string? UpdatedBy { get; set; }

    public DateTime? DeleteDate { get; set; }
    public string? DeleteBy { get; set; }
}
