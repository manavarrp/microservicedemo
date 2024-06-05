namespace Ordering.Domain.Common
{
    public abstract class BaseEntity
    {
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdateDate { get; set; }
    }
}
