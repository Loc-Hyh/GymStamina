using GymManagementProject.Dtos.Customer;
using GymManagementProject.Services.Customer;
using GymManagementProject.SMTP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly  IEmailService _emailService;

        public CustomerController(ICustomerService customerService, IEmailService emailService)
        {
            _customerService = customerService;
            _emailService = emailService;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create(CreateCustomerRequest request)
        {
            try
            {
                var result = await _customerService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        
        [HttpGet("getAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var result = await _customerService.GetAll();
            return Ok(result);
        }
        
        [HttpPost("update")]
        [Authorize]
        public async Task<IActionResult> Update(CustomerUpdateRequest request)
        {
            try
            {
                var result = await _customerService.Update(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _customerService.Delete(id);
            return Ok("Xóa thành công khách hàng có id:"+id);
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetPaging(SearchCustom request)
        {
            var result = await _customerService.SearchCustomer(request);
            return Ok(result);
        }
        [HttpPost("sendMailAll")]
        [Authorize]
        public async Task<IActionResult> SendEmailToAll([FromBody] SendEmailToAllCustomers request)
        {
            try
            {
                await _customerService.SendEmailAllCustomer(request);
                return Ok(new { message = "Đã gửi email cho tất cả khách hàng" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Gửi email thất bại", error = ex.Message });
            }
        }
    }
}
