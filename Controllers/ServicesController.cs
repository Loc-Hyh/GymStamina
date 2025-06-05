
using GymManagementProject.Dtos.Service;
using GymManagementProject.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : Controller
    {
        private readonly IServiceService _Service;

        public ServicesController(IServiceService Service)
        {
            _Service = Service;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create(CreateServiceRequest request)
        {
            try
            {
                var result = await _Service.Create(request);
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
            var result = await _Service.GetAll();
            return Ok(result);
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> Update(UpdateServiceRequest request)
        {
            try
            {
                var result = await _Service.Update(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _Service.Delete(id);
            return Ok("Xóa thành công dịch vụ có id: "+id);
        }
    }
}

