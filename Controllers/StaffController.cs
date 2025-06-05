using GymManagementProject.Dtos.Common;
using GymManagementProject.Dtos.Staff;
using GymManagementProject.Services.Staff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(StaffCreateRequest request)
        {
            try
            {
                var result = await _staffService.Create(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet("getAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _staffService.getAll();
            return Ok(result);
        }

        [HttpGet("getByBranchId/{branchId}")]
        public async Task<IActionResult> GetByBranchId(Guid branchId)
        {
            try
            {
                var result = await _staffService.getByBranchId(branchId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
        [HttpPost("update")]
        [Authorize]
        public async Task<IActionResult> Update(StaffUpdateRequest request)
        {
            try
            {
                var result = await _staffService.Update(request);
                return Ok(result);
            }
            catch (Exception e)
            {
               return BadRequest(new { message = e.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _staffService.Delete(id);
            return Ok("Xóa thành công nhân viên có id: "+id);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = await _staffService.Login(request);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpPost("search")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SearchStaff(GetPagingRequest request)
        {
            var result = await _staffService.SearchStaff(request);
            return Ok(result);
        }
    }

}
