namespace GymManagementProject.Dtos.Trainer;

public class UpdateTrainerRequest
{
    public Guid Id { get; set; } 
    public Guid BranchId { get; set; }
    public string? Name { get; set; }
    public string? Specialty { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
}