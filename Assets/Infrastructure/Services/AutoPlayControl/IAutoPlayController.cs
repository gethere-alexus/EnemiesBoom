using System.Collections.Generic;

namespace Infrastructure.Services.AutoPlayControl
{
    /// <summary>
    /// Controller for all auto-processes
    /// </summary>
    public interface IAutoPlayController : IService
    {
        void StartAutoPlays();
        void RestartAutoPlays();
        void StopAutoPlays();
        void RegisterAutoPlay(IAutoPlay controller);

        void ClearControllers();
        List<IAutoPlay> ProcessControllers { get; }
    }
}