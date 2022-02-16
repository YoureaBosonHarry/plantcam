using MMALSharp;
using MMALSharp.Common;
using MMALSharp.Handlers;
using PlantCam.Models;
using PlantCam.Services.Interfaces;

namespace PlantCam.Services
{
    public class FrameService: IFrameService
    {
        private readonly Serilog.ILogger log;
        public FrameService()
        {
            this.log = Serilog.Log.ForContext<FrameService>();
        }

        public async Task TakePhotoAsync(SnapshotRequest snapshotRequest)
        {
            MMALCamera cam = MMALCamera.Instance;
            var filePath = snapshotRequest.FilePath ?? $"/app/images/{snapshotRequest.PlantName.ToLower().Replace(" ", "_")}";
            this.log.Information($"Writing snapshot for object {snapshotRequest.PlantName} to directory {filePath}");
            using (var imgCaptureHandler = new ImageStreamCaptureHandler(filePath, "jpg"))
            {
                await cam.TakePicture(imgCaptureHandler, MMALEncoding.JPEG, MMALEncoding.I420);
            }
            cam.Cleanup();
        }
    }
}
