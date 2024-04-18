namespace Infrastructure.Services.ConfigLoad
{
    public interface IConfigProvider : IService
    {
        void LoadConfigs();
        void RegisterLoader(IConfigReader configLoader);
        void ClearLoaders();
    }
}