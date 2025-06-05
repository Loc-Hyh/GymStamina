using GymManagementProject.Const;

namespace GymManagementProject.Dtos.Staff
{
    public class StaffCreateRequest
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Enums.StaffRole Role { get; set; } // Use enum if needed
        public string Email { get; set; }
        public string Phone { get; set; }

        public Guid? BranchId { get; set; }
    }
}
