using Sources.GameFieldBase.Extensions.SlotsUnlock;

namespace Infrastructure.Services.ConfigLoad
{
    public interface IConfigLoader
    {
        void LoadConfigs();
        void RegisterLoader(IConfigReader configLoader);
        void ClearLoaders();
    }
}