namespace GymManagementProject.Entity
{
    public class BranchEntity
    {
        public Guid Id { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Guid? StaffId { get; set; } // Manager
        public virtual StaffEntity Staff { get; set; }

        public virtual ICollection<CustomerEntity> Customers { get; set; }
        public virtual ICollection<TrainerEntity> Trainers { get; set; }
        public virtual ICollection<EquipmentEntity> Equipment { get; set; }
        public virtual ICollection<StaffEntity> Staffs { get; set; }
    }
}
