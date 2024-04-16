using Sources.AnvilBase;

namespace Infrastructure.Services.Factories.AnvilFactory
{
    /// <summary>
    /// Factory for anvil and its extensions
    /// </summary>
    public interface IAnvilFactory
    {
        void CreateAnvil();
        void CreateAnvilExtensions();
        Anvil Anvil { get; }
    }
}