using GymManagementProject.Dtos.Common;
using GymManagementProject.Dtos.Equipment;
using GymManagementProject.Entity;
using GymManagementProject.Services.Equipment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : Controller
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create(CreateEquipmentRequest request)
        {
            try
            {
                var result = await _equipmentService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _equipmentService.GetAll();
            return Ok(result);
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> Update(UpdateEquipmentRequest request)
        {
            try
            {
                var result = await _equipmentService.Update(request);
                return Ok(result);
            }
            catch (Exception e)
            {
               return BadRequest(new { message = e.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _equipmentService.Delete(id);
            return Ok("Xóa thành công thiết bị có id: "+id);
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchEquipment(GetPagingRequest request)
        {
            var result = await _equipmentService.SearchEquipment(request);
            return Ok(result);
        }

        [HttpPost("upload-image")]
        [Authorize]
        public async Task<IActionResult> UploadImage(IFormFile file, Guid EquipmentId)
        {
            try
            {
                var result = await _equipmentService.UploadImageAsync(file, EquipmentId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
