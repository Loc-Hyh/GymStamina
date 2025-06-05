using GymManagementProject.Dtos.Common;
using GymManagementProject.Dtos.Trainer;

namespace GymManagementProject.Services.Trainer
{
    public interface ITrainerService
    {
        Task<Guid> Create(CreateTrainerRequest request);
        Task<List<TrainerDto>> GetAll();
        Task<Guid> Update(UpdateTrainerRequest request);
        Task Delete(Guid id);
        Task<PageView<TrainerDto>> SearchTrainer(GetPagingRequest request);
        Task<TrainerScheduleDto> GetTrainerSchedule(Guid Id);
    }
}
