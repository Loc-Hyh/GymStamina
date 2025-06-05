namespace GymManagementProject.Dtos.Schedule;

public class ScheduleResponse
{
    public string Message { get; set; }
    public int Remaining { get; set; } // Số buổi còn thiếu sau khi tạo
    public List<ScheduleDto> Schedules { get; set; }
}