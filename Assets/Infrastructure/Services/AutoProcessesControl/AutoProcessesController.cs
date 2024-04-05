using System.Collections.Generic;
using Infrastructure.Services.AutoProcessesControl.Connection;

namespace Infrastructure.Services.AutoProcessesControl
{
    public class AutoProcessesController : IAutoProcessesController
    {
        public List<IAutoProcessController> ProcessControllers { get; } = new List<IAutoProcessController>();

        public void StartAllProcesses()
        {
            foreach (var processController in ProcessControllers)
            {
                processController.StartProcess();
            }
        }

        public void RestartAllProcesses()
        {
            foreach (var processController in ProcessControllers)
            {
                processController.StopProcess();
                processController.StartProcess();
            }
        }

        public void StopAllProcesses()
        {
            foreach (var processController in ProcessControllers)
            {
                processController.StopProcess();
            }
        }

        public void ClearControllers() => 
            ProcessControllers.Clear();

        public void RegisterController(IAutoProcessController controller) 
            => ProcessControllers.Add(controller);
    }
}