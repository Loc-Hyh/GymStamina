namespace GymManagementProject.Dtos.Equipment;

public class UpdateEquipmentRequest
{
    public Guid Id { get; set; } 
    public Guid BranchId { get; set; }

    public string EquipmentName { get; set; }
    public int Using { get; set; }
    public int Maintenance { get; set; }
    public int Broken { get; set; }
}