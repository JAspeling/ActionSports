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
    [Route("api/League")]
    public class LeagueController : Controller {
        public ILeaguesRepository LeaguesRepository { get; }
        public ILogger Logger { get; }

        public LeagueController(ILeaguesRepository leaguesRepository, ILoggerFactory loggerFactory) {
            LeaguesRepository = leaguesRepository;
            Logger = loggerFactory.CreateLogger("LeagueController");
        }

        // POST: api/League
        [HttpPost]
        public IEnumerable<LeagueModel> Post([FromBody]VenueModel venue) {
            try {
                Logger.LogDebug($"Getting Venues for {venue.Title}");
                var leagues = LeaguesRepository.GetLeagues(venue);
                return leagues;
            }
            catch (Exception ex) {
                ex.CustomLog(Logger, $"Failed to retrieve Lagues for Venue");
                return null;
            }
        }
    }
}
