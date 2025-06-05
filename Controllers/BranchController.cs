using GymManagementProject.Dtos.Branch;
using GymManagementProject.Services.Brach;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchServices;
        public BranchController(IBranchService branchServices)
        {
            _branchServices = branchServices;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(BranchCreateRequest request)
        {
            try
            {
                var result = await _branchServices.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _branchServices.GetAll();
            return Ok(result);
        }
        [HttpPost("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(BranchUpdateRequest request)
        {
            try
            {
                var result = await _branchServices.Update(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _branchServices.Delete(id);
            return Ok("Xóa thành công chi nhánh có id: "+id);
        }
    }
}
