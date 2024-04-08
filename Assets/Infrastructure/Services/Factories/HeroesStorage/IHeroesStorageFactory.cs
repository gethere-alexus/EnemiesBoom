namespace Infrastructure.Services.Factories.HeroesStorage
{
    public interface IHeroesStorageFactory : IService
    {
        void CreateActiveHeroesStorage();
    }
}