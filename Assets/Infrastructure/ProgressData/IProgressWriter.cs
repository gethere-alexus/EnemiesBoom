namespace Infrastructure.ProgressData
{
    public interface IProgressWriter : IProgressReader
    {
        void SaveProgress();
    }
}