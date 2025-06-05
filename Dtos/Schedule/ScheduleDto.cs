namespace GymManagementProject.Dtos.Schedule;

public class ScheduleDto
{
    public Guid Id { get; set; }
    
    public Guid CustomerId { get; set; }
    
    public string CustomerName { get; set; }
    
    public Guid TrainerId { get; set; }
    
    public string TrainerName { get; set; }
    
    public Guid ServiceId { get; set; }
    
    public string ServiceName { get; set; }
    public DateTime PracticeDay { get; set; }

    public bool IsCheckedIn { get; set; }
}