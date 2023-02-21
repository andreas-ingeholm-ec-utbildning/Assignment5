using BugReportClient.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BugReportClient.Contexts;

internal class DataContext : DbContext
{

    static readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\andre\Projects\Assignment5\BugReportClient\Data\db.mdf;Integrated Security=True;Connect Timeout=30";

    #region Constructors

    public DataContext()
    { }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    #endregion
    #region Overrides

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            _ = optionsBuilder.UseSqlServer(_connectionString);
    }

    #endregion
    #region Entities

    public DbSet<AddressEntity> Addresses { get; set; } = null!;
    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<BugReportEntity> BugReports { get; set; } = null!;

    #endregion

}
