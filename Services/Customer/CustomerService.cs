using AutoMapper;
using GymManagementProject.Data;
using GymManagementProject.Dtos.Common;
using GymManagementProject.Dtos.Customer;
using GymManagementProject.Entity;
using GymManagementProject.Repository;
using GymManagementProject.SMTP;
using Microsoft.EntityFrameworkCore;

namespace GymManagementProject.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<CustomerEntity> _rp;
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public CustomerService(IRepository<CustomerEntity> rp, AppDbContext context, IMapper mapper, IEmailService emailService)
        {
            _mapper = mapper;
            _context=context;
            _rp = rp;
            _emailService = emailService;
        }

        public async Task<Guid> Create(CreateCustomerRequest request)
        {
            var customerEmail = await _rp.FirstOrDefault(c => c.Email == request.Email);
            if (customerEmail != null)
            {
                throw new Exception("Email already exists");
            }
            var customerPhone = await _rp.FirstOrDefault(c => c.Phone == request.Phone);
            if (customerPhone != null)
            {
                throw new Exception("Phone already exists");
            }
            var customerExits =  _mapper.Map<CustomerEntity>(request);
            await _rp.CreateAsync(customerExits);
            string subject = "Cảm ơn bạn đã đăng ký dịch vụ tại Gym XYZ";
            string body = $@"
            Xin chào {customerExits.Name},<br/><br/>
            Cảm ơn bạn đã đăng ký dịch vụ tại <strong>Gym Stamina</strong>.<br/>
            Chúng tôi rất hân hạnh được đồng hành cùng bạn trong hành trình rèn luyện sức khỏe.<br/><br/>
            Nếu bạn có bất kỳ câu hỏi nào, vui lòng liên hệ với chúng tôi.<br/><br/>
            Trân trọng,<br/>
            Đội ngũ Gym Stamina
            ";

            await _emailService.SendEmailAsync(customerExits.Email, subject, body, isHtml: true);
            return customerExits.Id;
        }

        public async Task<Guid> Update(CustomerUpdateRequest request)
        {
            var customerExits = await _rp.GetAsync(request.Id);
            _mapper.Map(request, customerExits);
            await _rp.UpdateAsync(customerExits);
            return customerExits.Id;
        }

        public async Task<List<CustomerDto>> GetAll()
        {
            var customerExits =await _rp.AsQueryable().ToListAsync();
            return _mapper.Map<List<CustomerDto>>(customerExits);
        }

        public async Task Delete(Guid id)
        {
            await _rp.DeleteAsync(id);
        }
        
        public Task<PageView<CustomerDto>> SearchCustomer(SearchCustom request)
        {
            
            IQueryable<CustomerEntity> query= _rp.AsQueryable();
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
            return Task.FromResult(new PageView<CustomerDto>
            {
                TotalRecord = total,
                Items = _mapper.Map<List<CustomerDto>>(query.ToList())
            });
        }

        public async Task SendEmailAllCustomer(SendEmailToAllCustomers request)
        {
            var customerExits =await _rp.AsQueryable().ToListAsync();
            foreach (var customer in customerExits)
            {
                try
                {
                    string personalizedBody = request.Body.Replace("{{name}}", customer.Name);
                    await _emailService.SendEmailAsync(customer.Email, request.Subject, personalizedBody, request.IsHtml);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Gửi email cho {customer.Email} thất bại: {ex.Message}");
                }
            }
        }
    }
}
