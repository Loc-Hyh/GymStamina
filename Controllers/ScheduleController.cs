using GymManagementProject.Dtos.Schedule;
using GymManagementProject.Services.Schedule;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : Controller
    {
        private IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateScheduleRequest request)
        {
            try
            {
                var result = await _scheduleService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("createList")]
        public async Task<IActionResult> craeteList(CreateManualScheduleRequest request)
        {
            var result =await  _scheduleService.CreateList(request);
            return Ok(result);
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _scheduleService.GetAll();
            return Ok(result);
        }
        
        
        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var result = await _scheduleService.Get(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getByCustomerId/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(Guid customerId)
        {
            try
            {
                var result = await _scheduleService.GetByCustomerId(customerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        /*[HttpGet("getByTrainerId/{trainerId}")]
        public async Task<IActionResult> GetByTrainerId(Guid trainerId)
        {
            try
            {
                var result = await _scheduleService.GetByCustomerId(trainerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
               return BadRequest(new { message = ex.Message });
            }
        }*/
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateScheduleRequest request)
        {
            try
            {
                var result = await _scheduleService.Update(request);
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
            await _scheduleService.Delete(id);
            return Ok("Xóa thành công lịch tập có id: "+id);
        }
       
        [HttpPost("checkin/{scheduleId}")]
        public async Task<IActionResult> CheckIn(Guid scheduleId)
        {
            var result = await _scheduleService.CheckIn(scheduleId);
            return Ok("Điểm danh thành công!");
        }
        [HttpGet("exportExcel")]
        public async Task<IActionResult> ExportCustomerScheduleExcel([FromQuery] Guid customerId)
        {
            try
            {
                var fileData = await _scheduleService.ExportFileScheduleXML(customerId);
                var fileName = $"lich_tap_{customerId}.xlsx";

                return File(fileData,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
    
}