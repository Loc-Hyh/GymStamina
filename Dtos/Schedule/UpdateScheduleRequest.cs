namespace GymManagementProject.Dtos.Schedule;

public class UpdateScheduleRequest
{
    public Guid Id { get; set; }
    public DateTime PracticeDay { get; set; }

}