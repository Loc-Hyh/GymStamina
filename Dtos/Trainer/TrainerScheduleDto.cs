using GymManagementProject.Dtos.Schedule;

namespace GymManagementProject.Dtos.Trainer;

public class TrainerScheduleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool HasSchedule { get; set; }
    public List<GetScheduleDto> FutureSchedules { get; set; } = new();
}