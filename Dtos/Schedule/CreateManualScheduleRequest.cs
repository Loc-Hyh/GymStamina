namespace GymManagementProject.Dtos.Schedule;

public class CreateManualScheduleRequest
{
    public Guid InvoiceId { get; set; }
    //public Guid TrainerId { get; set; }
    public List<DateTime> PracticeDay { get; set; }
}