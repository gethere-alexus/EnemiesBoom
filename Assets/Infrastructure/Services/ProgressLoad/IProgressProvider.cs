using System.Collections.Generic;
using Infrastructure.ProgressData;
using Infrastructure.Services.ProgressLoad.Connection;

namespace Infrastructure.Services.ProgressLoad
{
    /// <summary>
    /// Provides saved progress 
    /// </summary>
    public interface IProgressProvider
    {
        
        /// <summary>
        /// Save progress on device
        /// </summary>
        void SaveProgress();

        /// <summary>
        /// Register progress observer in a list
        /// </summary>
        /// <param name="reader"></param>
        void RegisterObserver(IProgressReader reader);

        void ClearObservers();

        /// <summary>
        /// List of progress readers
        /// </summary>
        List<IProgressReader> ProgressReaders { get; }

        /// <summary>
        /// List of progress writers
        /// </summary>
        List<IProgressWriter> ProgressWriters { get; }

        GameProgress GameProgress { get; }
    }
}