﻿namespace SCADA.DTOS
{
    public class TagDTO
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public string Driver { get; set; }
        public string IOAddress { get; set; }
        public double? InitialValue { get; set; }
        public int? ScanTime { get; set; }
        public bool? IsScanOn { get; set; }
        public double? LowLimit { get; set; }
        public double? HighLimit { get; set; }
        public string? Units { get; set; }
    }
}
