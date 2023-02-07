using Microsoft.EntityFrameworkCore;
using SkyDrive.BLL.Entities;

namespace SkyDrive.BLL
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<EventEntity> Events { get; set; } = null!;
        public DbSet<InstructorEntity> Instructors { get; set; } = null!;
        public DbSet<MemberEntity> Members { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
