using System.Reflection;
using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence;

public class DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options) : DbContext(options)
{
    public DbSet<Project>? Projects { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<Skill>? Skills { get; set; }
    public DbSet<UserSkill>? UserSkills { get; set; }
    public DbSet<ProjectComment>? Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}