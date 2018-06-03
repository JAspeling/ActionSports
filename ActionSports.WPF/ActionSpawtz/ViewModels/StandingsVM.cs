using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ActionSpawtz.Models;
using ActionSpawtz.Views;
using AngleSharp;
using AngleSharp.Dom;

namespace ActionSpawtz.ViewModels {
    public class StandingsVM : ViewModelBase {
        public LeagueModel League { get; set; }
        public StandingModels Standings { get; set; } = new StandingModels();

        internal void Initialize(LeagueModel selectedLeague) {
            League = selectedLeague;
            getStandings();
        }

        private void getStandings() {
            SetBusy(true, "Getting Standings...");
            var document = BrowsingContext.New(AppState.config).OpenAsync($"{AppState.baseURL}{League.Standing}").ContinueWith((t) => {
                processStandings(t);
                SetBusy(false, "Done!");
            });
        }

        private void processStandings(Task<IDocument> response) {
            Standings.ClearItems();
            IDocument document = response.Result;
            var tables = document.QuerySelectorAll(".FTable");
            foreach (var table in tables) {
                var Standing = new StandingModel();
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
                        matchModel.Score = match.QuerySelector(".FScore").TextContent;
                        matchModel.ScoreHref = match.QuerySelector(".FScore").QuerySelector("a").GetAttribute("href");
                    }
                    catch (Exception ex) {

                    }

                    Standing.Matches.AddOnUIThread(matchModel);
                }
                Standings.AddOnUIThread(Standing);
            }
        }
    }
}
