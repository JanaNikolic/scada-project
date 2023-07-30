using Microsoft.EntityFrameworkCore;
using SCADA.Model;

namespace SCADA.Data;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Alarm> Alarms { get; set; }
    public DbSet<AlarmActivated> AlarmsActivated { get; set; }
    public DbSet<AnalogInput> AnalogInput { get; set; }
    public DbSet<AnalogOutput> AnalogOutput { get; set; }
    public DbSet<DigitalOutput> DigitalOutput { get; set; }
    public DbSet<DigitalInput> DigitalInput { get; set; }
    public DbSet<TagRecord> TagRecords { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasKey(u => u.Id);
        builder.Entity<Alarm>()
            .HasOne(a => a.AnalogInput)
            .WithMany(t => t.Alarms)
            .HasForeignKey(a => a.TagId);
        builder.Entity<AnalogOutput>().HasKey(t => t.Id);
        builder.Entity<AnalogInput>().HasKey(t => t.Id);
        builder.Entity<DigitalOutput>().HasKey(t => t.Id);
        builder.Entity<DigitalInput>().HasKey(t => t.Id);
        builder.Entity<AlarmActivated>()
            .HasKey(a => a.Id);
        // builder.Entity<AlarmActivated>()
        //     .Property(a => a.Id)
        //     .ValueGeneratedOnAdd();
        builder.Entity<AlarmActivated>()
            .HasOne(p => p.Alarm)
            .WithMany()
            .HasForeignKey(p => p.AlarmId);
        
        builder.Entity<TagRecord>()
            .HasKey(a => a.Id);
        builder.Entity<TagRecord>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();
        builder.Entity<TagRecord>()
            .HasOne(p => p.Tag)
            .WithMany()
            .HasForeignKey(p => p.TagId)
            .OnDelete(DeleteBehavior.Cascade); //TODO
			
        base.OnModelCreating(builder);
    }
    
}