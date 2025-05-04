namespace IoTMonitoringAPI.Models
{
    public class GroupSensor
    {
        public int ID { get; set; }


        public int GroupID { get; set; }
        public virtual Group Group { get; set; }


        public int SensorDataID { get; set; }
        public virtual SensorData SensorData { get; set; }
    }
}
