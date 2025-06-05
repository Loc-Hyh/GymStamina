namespace GymManagementProject.Entity
{
    public class TrainerEntity
    {
        public Guid Id { get; set; }
        public Guid BranchId { get; set; }
        public virtual BranchEntity Branch { get; set; }

        public string Name { get; set; }
        public string Specialty { get; set; } // Chuyên môn
        public string Phone { get; set; }
        public string Email  { get; set; }

        public virtual ICollection<InvoiceEntity> Invoice { get; set; }
    }
}
