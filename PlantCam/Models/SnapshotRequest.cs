﻿namespace PlantCam.Models
{
    public class SnapshotRequest
    {
        public Guid CameraUUID { get; set; }
        public Guid PlantUUID { get; set; }
        public string? SnapshotMetadata { get; set; }
    }
}
