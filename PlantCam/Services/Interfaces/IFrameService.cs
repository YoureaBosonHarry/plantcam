using PlantCam.Models;

namespace PlantCam.Services.Interfaces
{
    public interface IFrameService
    {
        Task TakePhotoAsync(SnapshotRequest snapshotRequest);
    }
}
