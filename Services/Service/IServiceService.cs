using GymManagementProject.Dtos.Service;
using GymManagementProject.Entity;

namespace GymManagementProject.Services.Service
{
    public interface IServiceService
    {
        Task<Guid> Create(CreateServiceRequest request);
        Task<List<ServiceDto>> GetAll();
        Task<Guid> Update(UpdateServiceRequest request);
        Task Delete(Guid id);
    }
}
