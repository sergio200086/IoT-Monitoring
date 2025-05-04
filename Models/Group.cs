using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IoTMonitoringAPI.Models
{
    public class Group
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int ID { get; set; }
        public string Name { get; set; }
        public string? Network { get; set; }


        public virtual ICollection<GroupSensor> GroupSensor { get; set; }

    }
}
