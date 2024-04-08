namespace Infrastructure.Services.ConfigLoad
{
    public interface IConfigLoader : IService
    {
        void LoadConfigs();
        void RegisterLoader(IConfigReader configLoader);
        void ClearLoaders();
    }
}