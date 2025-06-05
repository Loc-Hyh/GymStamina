using System.ComponentModel.DataAnnotations;

namespace GymManagementProject.Dtos.Branch
{
    public class BranchCreateRequest
    {
        [Required]
        [StringLength(100)]
        public string BranchName { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Tuỳ chọn: ID nhân viên quản lý chi nhánh
        public Guid? StaffId { get; set; }
    }
}
