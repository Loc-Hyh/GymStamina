using GymManagementProject.Dtos.Branch;
using GymManagementProject.Entity;

namespace GymManagementProject.Services.Brach
{
    public interface IBranchService
    {
        Task<Guid> Create(BranchCreateRequest request);
        Task<List<BranchDto>> GetAll();
        Task<Guid> Update(BranchUpdateRequest request);
        Task Delete(Guid id);
    }
}
