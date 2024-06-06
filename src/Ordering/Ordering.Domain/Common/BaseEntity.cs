namespace Ordering.Domain.Common
{
    public abstract class BaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdateDate { get; set; }
    }
}
