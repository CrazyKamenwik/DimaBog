using SkyDrive.DAL;

namespace SkyDrive.Tests.Initialize
{
    public static class InitializeDb
    {
        public static async void Initialize(ApplicationContext context)
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            context.Members.AddRange(InitializeData.GetMembers());
            context.Events.AddRange(InitializeData.GetEvents());
            context.Instructors.AddRange(InitializeData.GetInstructors());

            await context.SaveChangesAsync();
        }
    }
}
