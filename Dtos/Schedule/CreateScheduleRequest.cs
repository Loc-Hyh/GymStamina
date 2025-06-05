namespace GymManagementProject.Dtos.Schedule;

public class CreateScheduleRequest
{
    public Guid InvoiceId { get; set; }
    public DateTime PracticeDay { get; set; }
}