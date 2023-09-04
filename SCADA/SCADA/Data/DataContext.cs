using Microsoft.EntityFrameworkCore;
using SCADA.Model;

namespace SCADA.Data;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Alarm> Alarms { get; set; }
    public DbSet<AlarmActivated> AlarmsActivated { get; set; }
    public DbSet<AnalogInput> AnalogInputs { get; set; }
    public DbSet<AnalogOutput> AnalogOutputs { get; set; }
    public DbSet<DigitalOutput> DigitalOutputs { get; set; }
    public DbSet<DigitalInput> DigitalInputs { get; set; }
    public DbSet<TagRecord> TagRecords { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    //     // Pass the connection string explicitly to the base DbContext class
    // Database.SetConnectionString(options.GetExtension<SqlServerOptionsExtension>().ConnectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasKey(u => u.Id);
        
        builder.Entity<AnalogInput>()
            .HasMany(e => e.Alarms)
            .WithOne(e => e.AnalogInput)
            .HasForeignKey(e => e.TagId);
        
        builder.Entity<Alarm>()
            .HasKey(a => a.Id);
        builder.Entity<Alarm>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();

        
        builder.Entity<AlarmActivated>()
            .HasKey(a => a.Id);
        builder.Entity<AlarmActivated>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();
        builder.Entity<AlarmActivated>()
            .HasOne(p => p.Alarm)
            .WithMany()
            .HasForeignKey(p => p.AlarmId)
            .OnDelete(DeleteBehavior.Cascade);
        
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
			
        builder.Entity<Tag>()
            .HasKey(t => t.Id);

        builder.Entity<AnalogInput>()
            .HasBaseType<Tag>();
        base.OnModelCreating(builder);
    }
    
}