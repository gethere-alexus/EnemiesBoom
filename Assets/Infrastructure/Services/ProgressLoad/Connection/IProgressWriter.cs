using Infrastructure.ProgressData;

namespace Infrastructure.Services.ProgressLoad.Connection
{
    public interface IProgressWriter : IProgressReader
    {
        void SaveProgress(GameProgress progress);
    }
}