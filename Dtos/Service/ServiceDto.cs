using GymManagementProject.Const;

namespace GymManagementProject.Dtos.Service;

public class ServiceDto
{
    public Guid Id { get; set; }
    public string ServiceName { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public Enums.ServiceType ServiceType { get; set; }
    public Enums.DateTrain DateTrain { get; set; }
}