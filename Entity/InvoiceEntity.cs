using GymManagementProject.Const;

namespace GymManagementProject.Entity
{
    public class InvoiceEntity
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }
        public virtual CustomerEntity Customer { get; set; }
        
        public Guid? TrainerId { get; set; }
        public virtual TrainerEntity? Trainer { get; set; }
        public Guid ServicesId { get; set; }
        public virtual ServiceEntity Service { get; set; }
        public int DurationTime { get; set; }
        public decimal TotalPrice { get; set; }
        public Enums.PaymentMethod Method { get; set; }
        public DateTime PaymentDate { get; set; }
        
        public Guid CreateById { get; set; }
        public virtual List<ScheduleEntity> Schedules { get; set; }
        public virtual StaffEntity CreateBy { get; set; }
    }
}
