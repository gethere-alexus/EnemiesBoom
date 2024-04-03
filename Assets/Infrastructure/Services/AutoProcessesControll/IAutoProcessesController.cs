using System.Collections.Generic;
using Infrastructure.Services.AutoProcessesControll.Connection;

namespace Infrastructure.Services.AutoProcessesControll
{
    /// <summary>
    /// Controller for all auto-processes
    /// </summary>
    public interface IAutoProcessesController
    {
        void StartAllProcesses();
        void RestartAllProcesses();
        void StopAllProcesses();
        void RegisterController(IAutoProcessController controller);
        
        List<IAutoProcessController> ProcessControllers { get; }
    }
}