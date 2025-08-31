namespace Questioning_Data_Repositories.DataTableEF.Common
{
    public class BaseEntity
    {
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdateDate { get; set; } = DateTime.UtcNow;
    }
}
