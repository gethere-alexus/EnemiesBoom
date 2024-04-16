namespace Infrastructure.Services.Factories.AnvilFactories
{
    /// <summary>
    /// Factory for anvil and its extensions
    /// </summary>
    public interface IAnvilFactory
    {
        void CreateAnvil();
        void CreateAnvilExtensions();
        Sources.AnvilBase.Anvil Anvil { get; }
    }
}