using ActionSports.API.Models;
using ActionSports.API.Services;
using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionSports.API.Repositories {
    public class StandingsRepository : IStandingsRepository {

        public List<StandingsModel> GetStandings(LeagueModel league) {
            Task<IDocument> task = BrowsingContext.New(AppState.config).OpenAsync($"{AppState.baseURL}{league.Standing}");
            task.Wait();

            return processStandings(task.Result);
        }

        private List<StandingsModel> processStandings(IDocument response) {
            var standings = new List<StandingsModel>();
            IDocument document = response;
            var tables = document.QuerySelectorAll(".FTable");
            foreach (var table in tables) {
                var Standing = new StandingsModel();
                var headerRow = table.QuerySelector(".FHeader");
                Standing.Date = headerRow.QuerySelector("td").TextContent;
                var matches = table.QuerySelectorAll(".FRow");
                foreach (var match in matches) {
                    var matchModel = new MatchModel();
                    matchModel.Time = match.QuerySelector(".FDate").TextContent;
                    try {
                        matchModel.TeamA = match.QuerySelector(".FHomeTeam").TextContent;
                        matchModel.TeamAHref = match.QuerySelector(".FHomeTeam").QuerySelector("a").GetAttribute("href");

                        matchModel.TeamB = match.QuerySelector(".FAwayTeam").TextContent;
                        matchModel.TeamBHref = match.QuerySelector(".FAwayTeam").QuerySelector("a").GetAttribute("href");

                        matchModel.Court = match.QuerySelector(".FPlayingArea").TextContent;
                        
                        matchModel.Score = Extensions.Purify(match.QuerySelector(".FScore").TextContent);
                        matchModel.ScoreHref = match.QuerySelector(".FScore").QuerySelector("a").GetAttribute("href");
                    }
                    catch (Exception ex) {

                    }

                    Standing.Matches.Add(matchModel);
                }
                standings.Add(Standing);
            }
            return standings;
        }

    }
}
