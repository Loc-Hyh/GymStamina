using GymManagementProject.Dtos.Invoice;
using GymManagementProject.Services.Invoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create(CreateInvoiceRequest request)
        {
            try
            {
                var result = await _invoiceService.Create(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _invoiceService.GetAll();
            return Ok(result);
        }

        [HttpGet("getByCustomerId/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(Guid customerId)
        {
            try
            {
                var result = await _invoiceService.GetByCustomerId(customerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> Update(UpdateInvoiceRequest request)
        {
            try
            {
                var result = await _invoiceService.Update(request);
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
            await _invoiceService.Delete(id);
            return Ok("Xóa thành công hóa đơn có id :"+id);
        }
        
        [HttpGet("total-revenue")]
        public async Task<IActionResult> GetTotalRevenue()
        {
            var total = await _invoiceService.GetTotalRevenue();
            return Ok(total);
        }

        [HttpGet("monthly-revenue")]
        public async Task<IActionResult> GetMonthlyRevenue([FromQuery] int month, [FromQuery] int year)
        {
            var total = await _invoiceService.GetMonthlyRevenue(month, year);
            return Ok(total);
        }
    }
}
