using GymManagementProject.Const;

namespace GymManagementProject.Entity
{
    public class ServiceEntity
    {
        public Guid Id { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public Enums.ServiceType ServiceType { get; set; } // tu_tap, thue_pt
        public Enums.DateTrain DateTrain { get; set; } // Số ngày tập
        public virtual ICollection<ScheduleEntity> Schedules { get; set; }
        public virtual ICollection<InvoiceEntity> Invoices { get; set; }
    }
}
