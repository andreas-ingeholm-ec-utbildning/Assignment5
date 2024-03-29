﻿using IssuesClient.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IssuesClient;

public class DataContext : DbContext
{

    public static DataContext Instance { get; private set; } = null!;

    public DataContext() =>
        Instance = this;

    public DataContext(DbContextOptions options) : base(options) =>
        Instance = this;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\andre\Projects\Assignment5\IssuesClient\DB\db.mdf;Integrated Security=True;Connect Timeout=30");

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserProfileEntity> Profiles { get; set; }
    public DbSet<ReportEntity> Reports { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }

}
