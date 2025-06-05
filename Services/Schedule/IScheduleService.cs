using GymManagementProject.Dtos.Common;
using GymManagementProject.Dtos.Schedule;
using GymManagementProject.Entity;

namespace GymManagementProject.Services.Schedule
{
    public interface IScheduleService
    {
        Task<Guid> Create(CreateScheduleRequest request);
        Task<ScheduleResponse> CreateList(CreateManualScheduleRequest request);
        Task<Guid> Update(UpdateScheduleRequest request);
        Task<List<ScheduleDto>> GetAll();
        Task<List<ScheduleDto>> GetByCustomerId(Guid CustomerId);
        //Task<List<ScheduleDto>> GetByTrainerId(Guid TrainerId);
        Task Delete(Guid id);
        Task<ScheduleDto> Get(Guid id);
        Task<ScheduleEntity> CheckIn(Guid scheduleId);
        Task<byte[]> ExportFileScheduleXML(Guid scheduleId);
        
    }
}
