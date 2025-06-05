using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using GymManagementProject.Dtos.Common;
using GymManagementProject.Dtos.Staff;
using GymManagementProject.Entity;
using GymManagementProject.Repository;
using GymManagementProject.SMTP;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymManagementProject.Services.Staff
{
    public class StaffService : IStaffService
    {
        private readonly IRepository<StaffEntity> _rp;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<StaffEntity> _passwordHasher; // Băm mật khẩu của người dùng
        private readonly IHttpContextAccessor _httpContextAccessor; // Lấy thông tin người dùng từ token
        private readonly IConfiguration _configuaration;
        public StaffService(IRepository<StaffEntity> rp,IEmailService emailService, IMapper mapper,IPasswordHasher<StaffEntity> passwordHasher,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _rp = rp;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _configuaration = configuration;
        }

        public async Task<Guid> Create(StaffCreateRequest request)
        {
            var staffUsername= await _rp.FirstOrDefault(s => s.Username == request.Username);
            if (staffUsername != null)
            {
                throw new Exception("Username đã tồn tại");
            }
            var staffPhone= await _rp.FirstOrDefault(s => s.Phone == request.Phone);
            if (staffPhone != null)
            {
                throw new Exception("Số điện thoại đã tồn tại");
            }
            var staffEmail= await _rp.FirstOrDefault(s => s.Email == request.Email);
            if (staffEmail != null)
            {
                throw new Exception("Email đã tồn tại");
            }
            var staff = _mapper.Map<StaffEntity>(request);
            staff.Password = _passwordHasher.HashPassword(staff, request.Password);
            await _rp.CreateAsync(staff);
            string subject = $"Xin chào {staff.Name}";
            string body = $@"
            Tài khoản nhân viên của bạn tại <strong>Gym Stamina</strong> đã được tạo thành công.<br/>
            Thông tin đăng nhập<br/><br/>
            Username: {staff.Username}<br/><br/>
            Password: {request.Password}<br/><br/>
            Vai trò: {staff.Role}<br/><br/>
            Vui lòng đăng nhập vào hệ thống để bắt đầu công việc.
            <p>Trân trọng,<br/>Đội ngũ Stamina Gym</p>
            <div class=""footer"">
            <i>Email này được gửi tự động. Vui lòng không trả lời.</i>
            </div>
   
            ";

            await _emailService.SendEmailAsync(staff.Email, subject, body, isHtml: true);
            return staff.Id;
        }

        public async Task<List<StaffDto>> getByBranchId(Guid branchId)
        {
            var getBranch = await _rp.AsQueryable().Where(b => b.BranchId == branchId).ToListAsync();
            if (getBranch == null)
            {
                throw new Exception("Không tìm thấy dữ liệu");
            }
            return _mapper.Map<List<StaffDto>>(getBranch);
        }

        public async Task Delete(Guid id)
        {
           await _rp.DeleteAsync(id);
        }

        public async Task<List<StaffDto>> getAll()
        {
            var result = await _rp.AsQueryable().ToListAsync();
            return _mapper.Map<List<StaffDto>>(result);
        }

        public async Task<Guid> Update(StaffUpdateRequest request)
        {
            var staffExits =await _rp.GetAsync(request.Id);
            if (staffExits == null)
            {
                throw new Exception("Không có dữ liệu");
            }
            _mapper.Map<StaffEntity>(request);
            await _rp.UpdateAsync(staffExits);
            return staffExits.Id;
        }
        public async Task<string> Login(LoginRequest request)
        {
            var user = await _rp.FirstOrDefault(u => u.Username == request.Username /*|| u.Email == request.Email*/);

            if(user == null)
            {
                throw new Exception("Người dùng không tồn tại");
            }

            // So sánh mật khẩu người dùng với mật khẩu trong CSDL
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);
            // Có trạng thái là Failed, nếu mật khẩu không đúng
            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("Mật khẩu không đúng");
            }

            return GenerateToken(user);

        }

        public Task<PageView<StaffDto>> SearchStaff(GetPagingRequest request)
        {
            IQueryable<StaffEntity> query = _rp.AsQueryable();
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
            return Task.FromResult(new PageView<StaffDto>
            {
                TotalRecord = total,
                Items = _mapper.Map<List<StaffDto>>(query.ToList())
            });
        }

        private string GenerateToken (StaffEntity user)
        {
            var jwtSettings = _configuaration.GetSection("JwtSettings");
     

            var claims = new[]
            {
                // Không hiện thị, hệ thống Authorize sẽ chỉ đọc có ClaimType.Role
                new Claim(ClaimTypes.Role, user.Role.ToString()),  // Không public
                new Claim("Role", user.Role.ToString()),
                new Claim("Name", user.Name),
                new Claim("Email", user.Email),
                new Claim("Id", user.Id.ToString()), // Lấy Id Claim. Type == Id => String => Guid
            };

            var key = new Microsoft.IdentityModel.Tokens
                .SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["Secret"]));
            var creds = new Microsoft.IdentityModel.Tokens
                .SigningCredentials(key, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
