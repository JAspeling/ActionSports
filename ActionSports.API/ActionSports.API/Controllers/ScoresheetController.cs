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
    [Route("api/Scoresheet")]
    public class ScoresheetController : Controller {
        public ScoresheetController(ILoggerFactory Logger, IScoresheetRepository scoresheetRepository) {
            this.Logger = Logger.CreateLogger<ScoresheetController>();
            ScoresheetRepository = scoresheetRepository;
        }

        public ILogger Logger { get; }
        public IScoresheetRepository ScoresheetRepository { get; }

        // POST: api/Scoresheet
        [HttpPost]
        public string Post([FromBody]MatchModel match) {
            if (match == null) { throw new ArgumentNullException("match", "Could not deserialize JSON From Body into a MatchModel object."); };
            try {
                Logger.LogDebug("Scoresheet Controller hit!");
                var base64 = ScoresheetRepository.ConvertToPdf(match, match.ScoreHref);
                return base64;
            } catch (Exception ex) {
                ex.CustomLog(Logger, "Failed to convert the match scoresheet to PDF");
                return ex.ToString();
            }
        }
    }
}
