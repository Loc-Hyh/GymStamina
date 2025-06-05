using GymManagementProject.Const;

namespace GymManagementProject.Entity
{
    public class CustomerEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Enums.MembershipType MembershipType { get; set; }

        public virtual ICollection<ScheduleEntity> Schedules { get; set; }
        public virtual ICollection<InvoiceEntity> Invoices { get; set; }
    }
}
