using ActionSports.API.Models;
using ActionSports.API.Services;
using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionSports.API.Repositories {
    public class LeaguesRepository : ILeaguesRepository {
        public List<LeagueModel> GetLeagues(VenueModel venue) {
            Task<IDocument> task = BrowsingContext.New(AppState.config).OpenAsync($"{AppState.baseURL}{venue.Href}");
            task.Wait();
            var leagues = processLeagues(task.Result);
            return leagues;
        }

        private List<LeagueModel> processLeagues(IDocument response) {
            var leagues = new LeagueModels();
            IDocument document = response;
            IHtmlCollection<IElement> leagueListTable = document.QuerySelectorAll("table tr:not(.LSport)");

            foreach (IElement row in leagueListTable) {
                var league = new LeagueModel();
                var cols = row.QuerySelectorAll("td");
                cols.Each((td, index) => {
                    var href = "";
                    var anchor = td.FirstElementChild;
                    if (anchor != null) {
                        href = anchor.GetAttribute("href");
                    }
                    
                    switch (index) {
                        case 0: league.Title = td.TextContent; break;
                        case 1: league.Fixture = href; break;
                        case 2: league.Standing = href; break;
                    }
                });

                leagues.Add(league);
            }

            return leagues;




            //Leagues.ClearItems();
            //CQ dom = response.Result;
            //var table = dom["table tr:not(.LSport)"];
            //var results = from row in table select row;
            //foreach (var row in table) {
            //    var league = new LeagueModel();
            //    CQ rowDom = row.Render();
            //    var cols = rowDom["td"];

            //    cols.ForEach((td, index) => {
            //        CQ text = td.Render();
            //        var href = "";
            //        var anchor = td.FirstChild;
            //        anchor.TryGetAttribute("href", out href);
            //        switch (index) {
            //            case 0: league.Title = anchor.Render().Replace("\t", "").Replace("\n", ""); break;
            //            case 1: league.Fixture = href; break;
            //            case 2: league.Standing = href; break;
            //        }
            //    });
            //    Leagues.AddOnUIThread(league);
            //}




        }


    }
}
