using AutoMapper;
using GymManagementProject.Dtos.Invoice;
using GymManagementProject.Entity;
using GymManagementProject.Repository;
using GymManagementProject.SMTP;
using Microsoft.EntityFrameworkCore;

namespace GymManagementProject.Services.Invoice
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IRepository<InvoiceEntity> _rpInv;
        private readonly IRepository<CustomerEntity> _rpCus;
        private readonly IRepository<ServiceEntity> _rpSer;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public InvoiceService(IRepository<InvoiceEntity> rpInv, IMapper mapper,IEmailService emailService, IHttpContextAccessor httpContextAccessor, IRepository<CustomerEntity> rpCus, IRepository<ServiceEntity> rpSer)
        {
            _mapper = mapper;
            _rpInv = rpInv;
            _httpContextAccessor = httpContextAccessor;
            _rpCus = rpCus;
            _rpSer = rpSer;
            _emailService = emailService;
        }

        public async Task<Guid> Create(CreateInvoiceRequest request)
        {
            var createbyId = Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.First(u => u.Type == "Id").Value);
            var fineCus = await _rpCus.FirstOrDefault(c => c.Id == request.CustomerId);
            if (fineCus == null)
            {
                throw new Exception("Không tìm thấy thông tin");
            }
            var fineSer = await _rpSer.FirstOrDefault(c => c.Id == request.ServicesId);
            if (fineCus == null)
            {
                throw new Exception("Không tìm thấy thông tin");
            }
            var invoice = _mapper.Map<InvoiceEntity>(request);
            invoice.DurationTime = (int)fineSer.DateTrain;
            invoice.TotalPrice = fineSer.Price-(fineSer.Price * ((int) fineCus.MembershipType) / 100);
            invoice.CreateById = createbyId;
            await _rpInv.CreateAsync(invoice);
            string subject = "Cảm ơn bạn đã đăng ký dịch vụ tại Gym Stamina";
            string body = $@"
            Xin chào {fineCus.Name},<br/><br/>
            Bạn đã thanh toán hóa đơn thành công tại <strong>Gym Stamina</strong>.<br/>
            Chúng tôi rất hân hạnh được đồng hành cùng bạn trong hành trình rèn luyện sức khỏe.<br/><br/>
            Nếu bạn có bất kỳ câu hỏi nào, vui lòng liên hệ với chúng tôi.<br/><br/>
            Trân trọng,<br/>
            Đội ngũ Gym Stamina
            ";
            await _emailService.SendEmailAsync(fineCus.Email, subject, body, isHtml: true);
            return invoice.Id;
        }

        public async Task<List<InvoiceDto>> GetAll()
        {
            var invoices = await _rpInv.AsQueryable().ToListAsync();
            return _mapper.Map<List<InvoiceDto>>(invoices);
        }

        public async Task<List<InvoiceDto>> GetByCustomerId(Guid CustomerId)
        {
            var getCustomer = await _rpInv.AsQueryable().Where(c => c.CustomerId == CustomerId).ToListAsync();
            if (getCustomer == null)
            {
                throw new Exception("Khong co du lieu");
            }
            return _mapper.Map<List<InvoiceDto>>(getCustomer);
        }


        public async Task<Guid> Update(UpdateInvoiceRequest request)
        {
            var invoice =await _rpInv.GetAsync(request.Id);
            _mapper.Map(request, invoice);
            await _rpInv.UpdateAsync(invoice);
            return invoice.Id;
        }

        public async Task Delete(Guid id)
        {
            await _rpInv.DeleteAsync(id);
        }
        
        public async Task<decimal> GetTotalRevenue()
        {
            var totalRevenue = await _rpInv.AsQueryable().SumAsync(i => i.TotalPrice);
            return totalRevenue;
        }

        public async Task<decimal> GetMonthlyRevenue(int month, int year)
        {
            var revenue = await _rpInv.AsQueryable()
                .Where(i => i.PaymentDate.Month == month && i.PaymentDate.Year == year)
                .SumAsync(i => i.TotalPrice);

            return revenue;
        }
    }
}
