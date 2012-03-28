using System;
using RecreateMe.Scheduling;

namespace RecreateMe.GameMaintenance
{
    public class OldGameRemover
    {
        private readonly ITeamGameRepository _teamGameRepository;

        public OldGameRemover(ITeamGameRepository teamGameRepository)
        {
            _teamGameRepository = teamGameRepository;
        }

        public void Clean()
        {
            var teams = _teamGameRepository.GetAllGamesBeforeDate(DateTime.Now);

            foreach(var team in teams)
            {
                _teamGameRepository.DeleteGame(team.Id);
            }
        }
    }
}