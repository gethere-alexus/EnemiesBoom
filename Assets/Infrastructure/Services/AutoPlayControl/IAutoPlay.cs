namespace Infrastructure.Services.AutoPlayControl
{
    public interface IAutoPlay
    {
        void StartProcess();
        void RestartProcess();
        void StopProcess();
    }
}