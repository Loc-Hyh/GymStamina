using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GymManagementProject.Dtos.Common;
using GymManagementProject.Dtos.Equipment;
using GymManagementProject.Entity;
using GymManagementProject.Repository;
using Microsoft.EntityFrameworkCore;

namespace GymManagementProject.Services.Equipment
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IRepository<EquipmentEntity> _rp;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;

        public EquipmentService(IRepository<EquipmentEntity> rp, IMapper mapper, Cloudinary cloudinary)
        {
            _mapper = mapper;
            _rp = rp;
            _cloudinary = cloudinary;
        }

        public async Task<Guid> Create(CreateEquipmentRequest request)
        {
            var equipment = await _rp.FirstOrDefault(c => c.EquipmentName == request.EquipmentName);
            if (equipment != null)
            {
                throw new Exception("Email already exists");
            }

            var equipmentExits = _mapper.Map<EquipmentEntity>(request);
            await _rp.UpdateAsync(equipmentExits);
            return equipmentExits.Id;
        }

        public async Task<Guid> Update(UpdateEquipmentRequest request)
        {
            var equipmetnExits = await _rp.GetAsync(request.Id);
            if (equipmetnExits == null)
            {
                throw new Exception("Not found");
            }

            _mapper.Map(request, equipmetnExits);
            await _rp.UpdateAsync(equipmetnExits);
            return equipmetnExits.Id;
        }

        public async Task Delete(Guid id)
        {
            await _rp.DeleteAsync(id);
        }

        public async Task<List<EquipmentDto>> GetAll()
        {
            var equipmentExits = await _rp.AsQueryable().ToListAsync();
            return _mapper.Map<List<EquipmentDto>>(equipmentExits);
        }

        public Task<PageView<EquipmentDto>> SearchEquipment(GetPagingRequest request)
        {
            IQueryable<EquipmentEntity> query= _rp.AsQueryable();
            if (!string.IsNullOrEmpty(request.SearchText))
            {
                query = query.Where(t=>t.EquipmentName.ToLower().Contains(request.SearchText.ToLower()));
            }
            
            var total = query.Count();


            // phan trang 
            if (request.PageIndex.HasValue && request.PageSize.HasValue)
            {
                query= query.Skip((request.PageIndex.Value -1)*request.PageSize.Value).Take(request.PageSize.Value);
            }
            return Task.FromResult(new PageView<EquipmentDto>
            {
                TotalRecord = total,
                Items = _mapper.Map<List<EquipmentDto>>(query.ToList())
            });
        }
        public async Task<string> UploadImageAsync(IFormFile file, Guid equipmentId)
        {
            if (file == null || file.Length == 0)
                throw new Exception("Ảnh không hợp lệ");

            var equipment = await _rp.GetAsync(equipmentId);
            if (equipment == null)
                throw new Exception("Không tìm thấy thiết bị");
            //var account = new Account("dmmukvwvi", "595564171248616", "8U0ZRAJe75pAOwPsRpxIAsMG38g");
            //var cloudinary = new Cloudinary(account);

            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "productImage"
            };

            var result = await _cloudinary.UploadAsync(uploadParams);
            

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Upload lỗi: {result.Error?.Message}");

            // Cập nhật URL vào sản phẩm
            equipment.ImageUrl = result.SecureUri.ToString();
            await _rp.UpdateAsync(equipment);

            return equipment.ImageUrl;
        }

    }
}
