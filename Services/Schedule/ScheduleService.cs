using AutoMapper;
using ClosedXML.Excel;
using GymManagementProject.Data;
using GymManagementProject.Dtos.Common;
using GymManagementProject.Dtos.Schedule;
using GymManagementProject.Entity;
using GymManagementProject.Repository;
using Microsoft.EntityFrameworkCore;

namespace GymManagementProject.Services.Schedule
{
    public class ScheduleService : IScheduleService
    {
        private readonly IRepository<ScheduleEntity> _rpScheduleRepo;
        private readonly IRepository<TrainerEntity> _rpTrainerRepo;
        private readonly IRepository<InvoiceEntity> _rpInvoiceRepo;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ScheduleService(IRepository<ScheduleEntity> rpSchedule, IRepository<TrainerEntity> rpTrainer,IRepository<InvoiceEntity> rpInvoiceRepo,IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _rpScheduleRepo = rpSchedule;
            _rpTrainerRepo = rpTrainer;
            _rpInvoiceRepo = rpInvoiceRepo;
            _context = context;
        }

        public async Task<Guid> Create(CreateScheduleRequest request)
        {
            var invoice = await _rpInvoiceRepo.GetAsync(request.InvoiceId);
            if (invoice == null) throw new Exception("Hóa đơn không tồn tại");
            var practiceDay = request.PracticeDay.Date;
            if (practiceDay < DateTime.Today)
            {
                throw new Exception("Không thể tạo ngày trong quá khứ");
            }
            var isDuplicate = await _rpScheduleRepo.AsQueryable()
                .AnyAsync(s => s.InvoiceId == request.InvoiceId && s.PracticeDay.Date == practiceDay);
            if (isDuplicate)
            {
                throw new Exception("Ngày này đã được được tạo");
            }
            var isTrainerBusy = await _rpScheduleRepo.AsQueryable()
                .Include(s => s.Invoice)
                .AnyAsync(s => s.Invoice.TrainerId == invoice.TrainerId && s.PracticeDay.Date == practiceDay);
            if (isTrainerBusy)
            {
                throw new Exception("Huấn luyện viên đã có lịch");
            }
            var schedule = _mapper.Map<ScheduleEntity>(request);
            schedule.PracticeDay = practiceDay;
            await _rpScheduleRepo.CreateAsync(schedule);
            return schedule.Id;
        }

        public async Task<ScheduleResponse> CreateList(CreateManualScheduleRequest request)
        {
            var invoice = await _rpInvoiceRepo.GetAsync(request.InvoiceId);
            if (invoice == null)
            {
                throw new Exception("Hóa đơn không tồn tại");
            }
            

            var inputDays = request.PracticeDay
                .Select(d => d.Date)
                .ToList();

            // Kiểm tra ngày trùng trong request
            var duplicates = inputDays
                .GroupBy(d => d)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (duplicates.Any())
            {
                throw new Exception("Ngày này đã được được tạo");
            }
    
            // Chặn ngày trong quá khứ
            var invalidDays = inputDays.Where(d => d < DateTime.Today).ToList();
            if (invalidDays.Any())
            {
                throw new Exception("Không thể tạo ngày trong quá khứ");
            }

            int scheduledCount = await _rpScheduleRepo.AsQueryable()
                .CountAsync(s => s.InvoiceId == request.InvoiceId);
            int remaining = invoice.DurationTime - scheduledCount;

            if (inputDays.Count > remaining)
                throw new Exception($"Bạn chỉ được tạo thêm {remaining} buổi học");

            // Kiểm tra trùng lịch trainer
            var existingSchedules = await _rpScheduleRepo.AsQueryable()
                .Include(s => s.Invoice)
                .Where(s => s.Invoice != null &&
                    s.Invoice.TrainerId == invoice.TrainerId &&
                    inputDays.Contains(s.PracticeDay.Date))
                .ToListAsync();

            if (existingSchedules.Any())
            {
                var conflictDays = existingSchedules
                    .Select(s => s.PracticeDay.Date)
                    .Distinct()
                    .ToList();

                throw new Exception("Huấn luyện viên đã có lịch");
            }

            // Tạo schedule
            var schedules = inputDays.Select(day => new ScheduleEntity
            {
                Id = Guid.NewGuid(),
                InvoiceId = request.InvoiceId,
                PracticeDay = day,
                IsCheckedIn = false
            }).ToList();

            await _rpScheduleRepo.CreateListAsync(schedules);

            return new ScheduleResponse
            {
                Message = $"Tạo thành công {schedules.Count} buổi học",
                Remaining = remaining - schedules.Count,
                Schedules = schedules.Select(s => new ScheduleDto
                {
                    Id = s.Id,
                    CustomerId = s.Invoice.CustomerId,
                    CustomerName = s.Invoice.Customer.Name,
                    TrainerId = s.Invoice.Trainer.Id,
                    TrainerName = s.Invoice.Trainer.Name,
                    ServiceId = s.Invoice.Service.Id,
                    ServiceName = s.Invoice.Service.ServiceName,
                    PracticeDay = s.PracticeDay,
                    IsCheckedIn = s.IsCheckedIn,
                }).ToList()
            };
        }



        public async Task<Guid> Update(UpdateScheduleRequest request)
        {
            var schedule = await _rpScheduleRepo.FirstOrDefault(s => s.Id == request.Id);
            if (schedule == null)
            {
                throw new Exception("Không có dữ liệu");
            }
            _mapper.Map(request, schedule);
            await _rpScheduleRepo.UpdateAsync(schedule);
            return schedule.Id;
        }

        public async Task<List<ScheduleDto>> GetAll()
        {
            var schedules = await _rpScheduleRepo.AsQueryable().ToListAsync();
            return _mapper.Map<List<ScheduleDto>>(schedules);
        }

        public async Task<List<ScheduleDto>> GetByCustomerId(Guid CustomerId)
        {
            var getCustomer = await _context.Schedules.Where(s => s.Invoice.Customer.Id == CustomerId).ToListAsync();
            if (getCustomer == null)
            {
                throw new Exception("Không có dữ liệu");
            }
            return _mapper.Map<List<ScheduleDto>>(getCustomer);
        }

        /*public async Task<List<ScheduleDto>> GetByTrainerId(Guid TrainerId)
        {
            var getTrainer = await _context.Schedules.Where(s => s.TrainerId == TrainerId).ToListAsync();
            if (getTrainer == null)
            {
                throw new Exception("Khong co du lieu");
            }
            return _mapper.Map<List<ScheduleDto>>(getTrainer);
        }*/

        public async Task Delete(Guid id)
        {
            await _rpScheduleRepo.DeleteAsync(id);
        }

        public async Task<ScheduleDto> Get(Guid id)
        {
            var schedule = await _rpScheduleRepo.FirstOrDefault(s => s.Id == id);
            if (schedule == null)
            {
                throw new Exception("Không có dữ liệu");
            }
            return _mapper.Map<ScheduleDto>(schedule);
        }

        public async Task<ScheduleEntity> CheckIn(Guid scheduleId)
        {
            var schedule = await _rpScheduleRepo.FirstOrDefault(s => s.Id == scheduleId);

            if (schedule == null)
               throw new Exception("Không tìm thấy buổi tập.");

            if (schedule.IsCheckedIn)
                throw new Exception("Buổi này đã được điểm danh.");

            if (schedule.PracticeDay.Date != DateTime.Today)
                throw new Exception("Chỉ được điểm danh đúng ngày tập.");

            var invoice = await _rpInvoiceRepo.FirstOrDefault(i => i.Id == schedule.InvoiceId);

            if (invoice == null)
                throw new Exception("Không tìm thấy hóa đơn.");

            int checkedInCount = invoice.Schedules.Count(s => s.IsCheckedIn);

            if (checkedInCount >= invoice.DurationTime)
                throw new Exception("Khách hàng đã điểm danh đủ số buổi.");

            schedule.IsCheckedIn = true;
            await _rpScheduleRepo.UpdateAsync(schedule);

            return schedule;
        }

        public async Task<byte[]> ExportFileScheduleXML(Guid customerId)
        {
            var schedules = await _rpScheduleRepo.AsQueryable().Where(s => s.Invoice.CustomerId == customerId && s.PracticeDay >= DateTime.Now).ToListAsync();

            if (!schedules.Any())
            {
                throw new Exception("Không tìm thấy lịch tập cho khách hàng này.");
            }

            var customerName = schedules.First().Invoice.Customer.Name;

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Lịch tập");
            
            var title = $"LỊCH TẬP CỦA KHÁCH HÀNG: {customerName.ToUpper()}";
            worksheet.Cell(1, 1).Value = title;
            worksheet.Range(1, 1, 1, 4).Merge().Style
                .Font.SetBold()
                .Font.SetFontSize(16)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            // Header
            worksheet.Cell(3, 1).Value = "Ngày tập";
            worksheet.Cell(3, 2).Value = "Dịch vụ";
            worksheet.Cell(3, 3).Value = "HLV";
            worksheet.Cell(3, 4).Value = "Trạng thái";

            int row = 4;
            int startHeaderRow = 3;
            int startDataRow = 4;
            foreach (var s in schedules)
            {
                worksheet.Cell(row, 1).Value = s.PracticeDay.ToString("dd/MM/yyyy");
                worksheet.Cell(row, 2).Value = s.Invoice.Service?.ServiceName ?? "N/A";
                worksheet.Cell(row, 3).Value = s.Invoice.Trainer?.Name ?? "N/A";
                worksheet.Cell(row, 4).Value = s.IsCheckedIn ? "Đã điểm danh" : "Chưa điểm danh";
                row++;
            }

            worksheet.Columns().AdjustToContents();
            var dataRange = worksheet.Range(3,1,row - 1,4);
            dataRange.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
