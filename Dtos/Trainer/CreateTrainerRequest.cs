namespace GymManagementProject.Dtos.Trainer;

public class CreateTrainerRequest
{
    public Guid BranchId { get; set; }
    public string Name { get; set; }
    public string Specialty { get; set; } // ví dụ: "Yoga,Cardio"
    public string Phone { get; set; }
    public string Email { get; set; }
}