namespace Infrastructure.Services.AutoProcessesControll.Connection
{
    public interface IAutoProcessController
    {
        void StartProcess();
        void RestartProcess();
        void StopProcess();
    }
}