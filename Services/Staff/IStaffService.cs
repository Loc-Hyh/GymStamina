using GymManagementProject.Dtos.Common;
using GymManagementProject.Dtos.Staff;
using GymManagementProject.Entity;

namespace GymManagementProject.Services.Staff
{
    public interface IStaffService
    {
        Task<Guid> Create(StaffCreateRequest request);
        Task<Guid> Update(StaffUpdateRequest request);
        Task<List<StaffDto>> getAll();
        Task<List<StaffDto>> getByBranchId(Guid branchId);
        Task Delete(Guid id);
        Task<string> Login(LoginRequest request);
        Task<PageView<StaffDto>> SearchStaff(GetPagingRequest request);
    }
}
