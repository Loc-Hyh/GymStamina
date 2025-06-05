using AutoMapper;
using GymManagementProject.Dtos.Service;
using GymManagementProject.Entity;
using GymManagementProject.Repository;
using Microsoft.EntityFrameworkCore;

namespace GymManagementProject.Services.Service
{
    public class ServiceService : IServiceService
    {
        private readonly IRepository<ServiceEntity> _rp;
        private readonly IMapper _mapper;
        public ServiceService(IRepository<ServiceEntity> rp, IMapper mapper)
        {
            _mapper = mapper;
            _rp = rp;
        }

        public async Task<Guid> Create(CreateServiceRequest request)
        {
            var service = await _rp.FirstOrDefault(s => s.ServiceName == request.ServiceName);
            if (service != null)
            {
                throw new Exception("Service already exists");  
            }
            var serviceEntity = _mapper.Map<ServiceEntity>(request);
            await _rp.CreateAsync(serviceEntity);
            return serviceEntity.Id;
        }

        public async Task<List<ServiceDto>> GetAll()
        {
            var service = await _rp.AsQueryable().ToListAsync();
            return _mapper.Map<List<ServiceDto>>(service);
        }

        public async Task<Guid> Update(UpdateServiceRequest request)
        {
            var service =await _rp.FirstOrDefault(s => s.Id == request.Id);
            if (service == null)
            {
                throw new Exception("Khong co du lieu");
            }
            _mapper.Map(request, service);
            await _rp.UpdateAsync(service);
            return service.Id;
        }

        public async Task Delete(Guid id)
        {
            await _rp.DeleteAsync(id);
        }
    }
}
