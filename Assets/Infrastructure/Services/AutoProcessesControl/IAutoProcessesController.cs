using System.Collections.Generic;
using Infrastructure.Services.AutoProcessesControl.Connection;

namespace Infrastructure.Services.AutoProcessesControl
{
    /// <summary>
    /// Controller for all auto-processes
    /// </summary>
    public interface IAutoProcessesController : IService
    {
        void StartAllProcesses();
        void RestartAllProcesses();
        void StopAllProcesses();
        void RegisterController(IAutoProcessController controller);

        void ClearControllers();
        List<IAutoProcessController> ProcessControllers { get; }
    }
}