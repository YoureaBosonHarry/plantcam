using Dapper;
using Npgsql;
using PlantCam.Models;
using PlantCam.Repositories.Interfaces;

namespace PlantCam.Repositories
{
    public class FrameRepository: IFrameRepository
    {
        private readonly string connectionString;

        public FrameRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<Snapshot> AddSnapshotAsync(Snapshot snapshot)
        {
            using (var sql = new NpgsqlConnection(this.connectionString))
            {
                var sqlParams = new DynamicParameters();
                sqlParams.Add("_snapshot_uuid", snapshot.SnapshotUUID);
                sqlParams.Add("_camera_uuid", snapshot.CameraUUID);
                sqlParams.Add("_plant_uuid", snapshot.PlantUUID);
                sqlParams.Add("_snapshot_data", snapshot.SnapshotData);
                sqlParams.Add("_snapshot_metadata", snapshot.SnapshotMetadata);
                await sql.ExecuteAsync("plantcam_schema.add_snapshot", sqlParams, commandType: System.Data.CommandType.StoredProcedure);
                return snapshot;
            }
        }
    }
}
