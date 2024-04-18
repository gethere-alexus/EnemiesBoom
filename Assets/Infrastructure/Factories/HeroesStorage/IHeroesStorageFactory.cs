using Infrastructure.Services;

namespace Infrastructure.Factories.HeroesStorage
{
    /// <summary>
    /// Creates hero slots
    /// </summary>
    public interface IHeroesStorageFactory : IService
    {
        void CreateHeroesField();
    }
}