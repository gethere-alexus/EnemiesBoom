using System;
using Infrastructure.Configurations.Config;

namespace Infrastructure.Services.ConfigLoad
{
    public interface IConfigReader
    {
        void LoadConfiguration(ConfigContent configContainer);
        public event Action ConfigLoaded;
    }
}