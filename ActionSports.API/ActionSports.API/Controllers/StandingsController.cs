using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActionSports.API.Models;
using ActionSports.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ActionSports.API.Controllers {
    [Produces("application/json")]
    [Route("api/Standings")]
    public class StandingsController : Controller {

        public ILogger Logger { get; }
        public IStandingsRepository StandingsRepository { get; }

        public StandingsController(IStandingsRepository standingsRepository, ILoggerFactory factory) {
            Logger = factory.CreateLogger("StandingsController");
            StandingsRepository = standingsRepository;
        }


        [HttpPost]
        public IEnumerable<StandingsModel> Post([FromBody]LeagueModel league) {
            try {
                Logger.LogDebug($"Getting Standings for League {league.Title}");
                var standings = StandingsRepository.GetStandings(league);

                return standings;
            }
            catch (Exception ex) {
                ex.CustomLog(Logger, "Failed to retrieve Standings from league");
                return null;
            }
        }
    }
}
