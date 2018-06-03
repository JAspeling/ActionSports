using ActionSports.API.Models;
using ActionSports.API.Services;
using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionSports.API.Repositories {
    public class VenuesRepository : IVenuesRepository {


        public List<VenueModel> GetVenues() {
            Task<IDocument> task = BrowsingContext.New(AppState.config).OpenAsync($"{AppState.baseURL}/External/Fixtures/");
            task.Wait();
            var venues = processVenues(task.Result);
            return venues;
        }

        private List<VenueModel> processVenues(IDocument response) {
            var venues = new List<VenueModel>();
            IDocument document = response;
            var venueList = document.QuerySelectorAll("div .Content a");
            foreach (var anchor in venueList) {
                var venue = new VenueModel();
                venue.Href = anchor.GetAttribute("href");
                venue.Title = anchor.TextContent;
                venues.Add(venue);
            }
            return venues;
        }
    }
}
