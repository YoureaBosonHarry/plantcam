using MMALSharp;
using MMALSharp.Common;
using MMALSharp.Handlers;
using PlantCam.Models;
using PlantCam.Repositories.Interfaces;
using PlantCam.Services.Interfaces;
using System.Diagnostics;

namespace PlantCam.Services
{
    public class FrameService: IFrameService
    {
        private readonly Serilog.ILogger log;
        private MMALCamera camera;
        private readonly IFrameRepository frameRepository;
        public FrameService(IFrameRepository frameRepository)
        {
            this.log = Serilog.Log.ForContext<FrameService>();
            this.camera = MMALCamera.Instance;
            this.frameRepository = frameRepository;
        }

        public async Task TakePhotoAsync(SnapshotRequest snapshotRequest)
        {
            try
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var captureHandler = new InMemoryCaptureHandler();
                await this.camera.TakePicture(captureHandler, MMALEncoding.JPEG, MMALEncoding.I420);
                var outputFrames = captureHandler.WorkingData;
                stopWatch.Stop();
                this.log.Information($"Captured {outputFrames.Count} frames in {stopWatch.ElapsedMilliseconds} ms");
                var snapshot = new Snapshot
                {
                    CameraUUID = snapshotRequest.CameraUUID,
                    PlantUUID = snapshotRequest.PlantUUID,
                    SnapshotUUID = Guid.NewGuid(),
                    SnapshotData = Convert.ToBase64String(outputFrames.ToArray()),
                    SnapshotMetadata = snapshotRequest.SnapshotMetadata
                };
                var storedSnapshot = await this.frameRepository.AddSnapshotAsync(snapshot);
                this.log.Information($"Successfully stored new frame {storedSnapshot.SnapshotUUID} for plant: {storedSnapshot.PlantUUID}");
            }
            catch (Exception ex)
            {
                this.log.Error($"Unhandled Exception: {ex.Message}");
            }
        }
    }
}
