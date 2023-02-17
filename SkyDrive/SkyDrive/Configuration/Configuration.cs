using Mapster;

namespace SkyDrive.Configuration
{
    public static class Configuration
    {
        public static void SetMapster()
        {
            TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
            TypeAdapterConfig.GlobalSettings.Default.MaxDepth(2);
        }
    }
}
