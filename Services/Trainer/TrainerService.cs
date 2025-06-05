using AutoMapper;
using GymManagementProject.Data;
using GymManagementProject.Dtos.Common;
using GymManagementProject.Dtos.Schedule;
using GymManagementProject.Dtos.Trainer;
using GymManagementProject.Entity;
using GymManagementProject.Repository;
using Microsoft.EntityFrameworkCore;

namespace GymManagementProject.Services.Trainer
{
    public class TrainerService : ITrainerService
    {
        private readonly IRepository<TrainerEntity> _rpTrainer;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public TrainerService(IRepository<TrainerEntity> rpTrainer, IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _rpTrainer = rpTrainer;
            _context = context;
        }

        public async Task<Guid> Create(CreateTrainerRequest request)
        {
            var trainerExits = await _rpTrainer.FirstOrDefault(t => t.Email == request.Email && t.Phone == request.Phone);
            if (trainerExits != null)
            {
                throw new Exception("Du lieu da ton tai");
            }
            var trainer = _mapper.Map<TrainerEntity>(request);
            await _rpTrainer.CreateAsync(trainer);
            return trainer.Id;
        }

        public async Task<List<TrainerDto>> GetAll()
        {
            var trainer =await _rpTrainer.AsQueryable().ToListAsync();
            return _mapper.Map<List<TrainerDto>>(trainer);
        }

        public async Task<Guid> Update(UpdateTrainerRequest request)
        {
            var trainer = await _rpTrainer.GetAsync(request.Id);
            _mapper.Map(request, trainer);
            await _rpTrainer.UpdateAsync(trainer);
            return trainer.Id;
        }

        public async Task Delete(Guid id)
        {
            await _rpTrainer.DeleteAsync(id);
        }

        public Task<PageView<TrainerDto>> SearchTrainer(GetPagingRequest request)
        {
            IQueryable<TrainerEntity> query = _rpTrainer.AsQueryable();
            if (!string.IsNullOrEmpty(request.SearchText))
            {
                query = query.Where(t=>t.Name.ToLower().Contains(request.SearchText.ToLower()) 
                                       || t.Email.ToLower().Contains(request.SearchText.ToLower()) || t.Phone.ToLower().Contains(request.SearchText.ToLower()));
            }
            
            var total = query.Count();


            // phan trang 
            if (request.PageIndex.HasValue && request.PageSize.HasValue)
            {
                query= query.Skip((request.PageIndex.Value -1)*request.PageSize.Value).Take(request.PageSize.Value);
            }
            return Task.FromResult(new PageView<TrainerDto>
            {
                TotalRecord = total,
                Items = _mapper.Map<List<TrainerDto>>(query.ToList())
            });
        }

        public async Task<TrainerScheduleDto> GetTrainerSchedule(Guid Id)
        {
            var trainer = await _rpTrainer.FirstOrDefault(t => t.Id == Id);
            if (trainer == null)
            {
                throw new Exception("Không tồn tại dữ liệu");
            }
            var result = await _context.Trainers.Where(t => t.Id == trainer.Id).Select(s =>
                new TrainerScheduleDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    HasSchedule = s.Invoice.Any(i => i.Schedules.Any(s => s.PracticeDay>DateTime.Now)),
                    FutureSchedules = s.Invoice.SelectMany(s => s.Schedules).Where(s => s.PracticeDay>DateTime.Now)
                        .Select(s => new GetScheduleDto
                        {
                            PracticeDay = s.PracticeDay,
                        }).ToList()
                }).FirstOrDefaultAsync();
            return result!;
        }
    }
}
