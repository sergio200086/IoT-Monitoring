using System;
using System.Collections.Generic;

namespace IoTMonitoringAPI.Models
{
    public class SensorData
    {
        public int ID { get; set; }

        public string SensorName { get; set; }

        public string Type { get; set; }

        public float Value { get; set; }

        public string Unity { get; set; }

        public DateTime TimeStamp { get; set; }

        public virtual ICollection<GroupSensor> SensorGroup { get; set; }
    }
}
