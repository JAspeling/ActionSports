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
    [Route("api/Venues")]
    public class VenuesController : Controller {
        private readonly IVenuesRepository venuesRepository;
        private readonly ILogger logger;

        public VenuesController(IVenuesRepository venuesRepository, ILoggerFactory loggerFactory) {
            this.venuesRepository = venuesRepository;
            this.logger = loggerFactory.CreateLogger("VenuesController");
            //this.logger = loggerFactory;
        }

        [HttpGet]
        public IEnumerable<VenueModel> Get() {
            try {
                logger.LogInformation("Getting Venues");
                var venues = venuesRepository.GetVenues();
                logger.LogInformation($"{venues.Count} Venues retrieved");
                return venues;
            } catch (Exception ex ) {
                ex.CustomLog(logger, "Failed to retrieve the venues");
                return null;
            }
        }
    }
}