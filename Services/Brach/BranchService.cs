using AutoMapper;
using GymManagementProject.Dtos.Branch;
using GymManagementProject.Entity;
using GymManagementProject.Repository;
using Microsoft.EntityFrameworkCore;

namespace GymManagementProject.Services.Brach
{
    public class BranchService : IBranchService
    {
        private readonly IRepository<BranchEntity> _rp;
        private readonly IMapper _mapper;
        public BranchService(IRepository<BranchEntity> rp, IMapper mapper)
        {
            _mapper = mapper;
            _rp = rp;
        }
        public async Task<Guid> Create(BranchCreateRequest request)
        {
            var branchExits= await _rp.AsQueryable().AnyAsync(c => c.BranchName == request.BranchName);
            if (branchExits)
            {
                throw new Exception("Danh mục đã tồn tại");
            }
            var branch = _mapper.Map<BranchEntity>(request);
            await _rp.CreateAsync(branch);
            return branch.Id;
        }

        public async Task<List<BranchDto>> GetAll()
        {
            var result =await _rp.AsQueryable().ToListAsync();
            return _mapper.Map<List<BranchDto>>(result);
        }

        public async Task<Guid> Update(BranchUpdateRequest request)
        {
            var branchExits = await _rp.GetAsync(request.Id);
            if (branchExits == null)
            {
                throw new Exception("Chi nhánh không tồn tại");
            }
            _mapper.Map(request, branchExits);
            await _rp.UpdateAsync(branchExits);
            return branchExits.Id;
        }
        public async Task Delete(Guid id)
        {
            await _rp.DeleteAsync(id);
        }
    }
}
