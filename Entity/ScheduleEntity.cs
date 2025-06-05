namespace GymManagementProject.Entity
{
    public class ScheduleEntity
    {
        public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
        public virtual InvoiceEntity Invoice { get; set; }

        public DateTime PracticeDay { get; set; }
        public bool IsCheckedIn { get; set; }
    }
}
