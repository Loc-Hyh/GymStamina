namespace GymManagementProject.Dtos.Branch;

public class BranchDto
{
    public Guid Id { get; set; }
    public string BranchName { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string? ManagementName { get; set; }
    
}