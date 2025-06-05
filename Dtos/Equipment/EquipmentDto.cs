namespace GymManagementProject.Dtos.Equipment;

public class EquipmentDto
{
    public Guid Id { get; set; }
    
    public Guid BranchId { get; set; }

    public string BranchName { get; set; }

    public string EquipmentName { get; set; }
    public string? ImageUrl { get; set; }
    public int Using { get; set; }
    public int Maintenance { get; set; }
    public int Broken { get; set; }
}