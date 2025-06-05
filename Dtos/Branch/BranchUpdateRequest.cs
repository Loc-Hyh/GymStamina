using System.ComponentModel.DataAnnotations;

namespace GymManagementProject.Dtos.Branch
{
    public class BranchUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }  // ID chi nhánh cần cập nhật
        
        [StringLength(100)]
        public string? BranchName { get; set; }
        
        [StringLength(255)]
        public string? Address { get; set; }
        
        [Phone]
        public string? Phone { get; set; }
        
        [EmailAddress]
        public string? Email { get; set; }

        public Guid? StaffId { get; set; }  // Có thể null nếu không thay đổi
    }
}
