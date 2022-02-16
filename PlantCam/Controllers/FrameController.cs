using Microsoft.AspNetCore.Mvc;
using PlantCam.Models;
using PlantCam.Services.Interfaces;

namespace PlantCam.Controllers
{
    [ApiController]
    public class FrameController : ControllerBase
    {
        private readonly IFrameService frameService;
        public FrameController(IFrameService frameService)
        {
            this.frameService = frameService;
        }

        [HttpPost("TakePhoto")]
        public async Task<IActionResult> TakePhotoAsync([FromBody] SnapshotRequest snapshotRequest)
        {
            await this.frameService.TakePhotoAsync(snapshotRequest);
            return Ok();
        }
    }
}
