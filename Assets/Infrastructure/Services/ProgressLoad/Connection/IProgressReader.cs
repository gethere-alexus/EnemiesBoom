using Infrastructure.ProgressData;

namespace Infrastructure.Services.ProgressLoad.Connection
{
    public interface IProgressReader
    {
        void LoadProgress(GameProgress progress);
    }
}