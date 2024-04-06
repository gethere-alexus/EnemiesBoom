using Infrastructure.Configurations.Config;

namespace Infrastructure.Services.ConfigLoad
{
    public interface IConfigReader
    {
        void LoadConfiguration(ConfigContent configContainer);
    }
}