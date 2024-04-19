using System.Collections.Generic;

namespace Infrastructure.Services.AutoPlayControl
{
    public class AutoPlayController : IAutoPlayController
    {
        public List<IAutoPlay> ProcessControllers { get; } = new List<IAutoPlay>();

        public void StartAutoPlays()
        {
            foreach (var processController in ProcessControllers)
            {
                processController.StartProcess();
            }
        }

        public void RestartAutoPlays()
        {
            foreach (var processController in ProcessControllers)
            {
                processController.StopProcess();
                processController.StartProcess();
            }
        }

        public void StopAutoPlays()
        {
            foreach (var processController in ProcessControllers)
            {
                processController.StopProcess();
            }
        }

        public void ClearControllers() => 
            ProcessControllers.Clear();

        public void RegisterAutoPlay(IAutoPlay controller) 
            => ProcessControllers.Add(controller);
    }
}