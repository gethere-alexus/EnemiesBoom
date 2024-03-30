using Infrastructure.ProgressData;

namespace Infrastructure.Services.ProgressProvider
{
    /// <summary>
    /// Provides saved progress 
    /// </summary>
    public interface IProgressProvider
    {
      
        /// <param name="progress">Progress data</param>
        /// <typeparam name="TProgress">IProgressData</typeparam>
        void SaveProgress<TProgress>(TProgress progress) where TProgress : IProgressData;
        
        /// <typeparam name="TProgress">IProgressData</typeparam>
        /// <returns>Stored progress, if not found - null</returns>
        TProgress LoadProgress<TProgress>() where TProgress : IProgressData;
    }
}