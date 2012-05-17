using System.Timers;
using RecreateMe.Configuration;

namespace RecreateMe.GameMaintenance
{
    public class TimedGameDeleter
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IOldGameRemover _oldGameRemover;

        public TimedGameDeleter(IConfigurationProvider configurationProvider, IOldGameRemover oldGameRemover)
        {
            _configurationProvider = configurationProvider;
            _oldGameRemover = oldGameRemover;
        }

        public void BeginDeleting()
        {
            var timeer = new Timer {Interval = 15};
            timeer.Start();

            
            throw new System.NotImplementedException();
        }
    }
}