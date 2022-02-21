using PlantCam.Models;

namespace PlantCam.Repositories.Interfaces
{
    public interface IFrameRepository
    {
        Task<Snapshot> AddSnapshotAsync(Snapshot snapshot);
    }
}
