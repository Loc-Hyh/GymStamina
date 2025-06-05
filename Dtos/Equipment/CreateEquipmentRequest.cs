namespace GymManagementProject.Dtos.Equipment;

public class CreateEquipmentRequest
{
    public Guid BranchId { get; set; }
    public string EquipmentName { get; set; }
    public int Using { get; set; }
    public int Maintenance { get; set; }
    public int Broken { get; set; }
    
}