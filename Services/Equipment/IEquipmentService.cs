using GymManagementProject.Dtos.Common;
using GymManagementProject.Dtos.Equipment;
using GymManagementProject.Entity;

namespace GymManagementProject.Services.Equipment
{
    public interface IEquipmentService
    {
        Task<Guid> Create(CreateEquipmentRequest request);
        Task<Guid> Update(UpdateEquipmentRequest request);
        Task Delete(Guid id);
        Task<List<EquipmentDto>> GetAll();
        Task<PageView<EquipmentDto>> SearchEquipment(GetPagingRequest request);
        Task<string> UploadImageAsync(IFormFile file, Guid equipmentId);

    }
}
