namespace GymManagementProject.Dtos.Trainer;

public class TrainerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string BranchName { get; set; }
    public string Specialty { get; set; } //Lĩnh vực
    public string Phone { get; set; }
    public string Email { get; set; }

}