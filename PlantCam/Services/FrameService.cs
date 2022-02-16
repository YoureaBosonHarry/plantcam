using MMALSharp;
using MMALSharp.Common;
using MMALSharp.Handlers;
using PlantCam.Models;
using PlantCam.Services.Interfaces;

namespace PlantCam.Services
{
    public class FrameService: IFrameService
    {
        public FrameService()
        {
            ;
        }

        public async Task TakePhotoAsync(SnapshotRequest snapshotRequest)
        {
            MMALCamera cam = MMALCamera.Instance;
            var filePath = snapshotRequest.FilePath ?? $"/home/pi/plantcam/{snapshotRequest.PlantName.ToLower().Replace(" ", "_")}";
            using (var imgCaptureHandler = new ImageStreamCaptureHandler(filePath, "jpg"))
            {
                await cam.TakePicture(imgCaptureHandler, MMALEncoding.JPEG, MMALEncoding.I420);
            }
            cam.Cleanup();
        }
    }
}
