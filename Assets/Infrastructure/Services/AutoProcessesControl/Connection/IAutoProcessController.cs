namespace Infrastructure.Services.AutoProcessesControl.Connection
{
    public interface IAutoProcessController
    {
        void StartProcess();
        void RestartProcess();
        void StopProcess();
    }
}