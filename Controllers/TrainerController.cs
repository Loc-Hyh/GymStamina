using GymManagementProject.Dtos.Common;
using GymManagementProject.Dtos.Trainer;
using GymManagementProject.Services.Trainer;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create( CreateTrainerRequest request)
        {
            var result =await _trainerService.Create(request);
            return Ok(result);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _trainerService.GetAll();
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateTrainerRequest request)
        {
            var result =await _trainerService.Update(request);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _trainerService.Delete(id);
            return Ok("Success");
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchTrainer(GetPagingRequest request)
        {
            var result = await _trainerService.SearchTrainer(request);
            return Ok(result);
        }
        [HttpGet("allSchedule")]
        public async Task<IActionResult> GetTrainerSchedule(Guid id)
        {
            var result = await _trainerService.GetTrainerSchedule(id);
            return Ok(result);
        }
    }
}

