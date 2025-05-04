using IoTMonitoringAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IoTMonitoringAPI.DAL
{
    public class MonitoringContext : DbContext
    {
        public MonitoringContext(DbContextOptions<MonitoringContext> options) : base(options)
        {
        }

        public DbSet<Group> Group { get; set; }
        public DbSet<GroupSensor> GroupSensor { get; set; }
        public DbSet<SensorData> SensorData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().ToTable("Group");
            modelBuilder.Entity<GroupSensor>().ToTable("GroupSensor");
            modelBuilder.Entity<SensorData>().ToTable("SensorData");
        }
    }
}
