using ActionSpawtz.Models;
using AngleSharp;
using AngleSharp.Dom;
using CsQuery.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ActionSpawtz.ViewModels {
    public class FixturesVM : ViewModelBase {

        public LeagueModel League { get; set; }
        public FixtureModels Fixtures { get; set; } = new FixtureModels();

        IConfiguration config = Configuration.Default.WithDefaultLoader();
        internal void Initialize(LeagueModel league) {
            League = league;
            getFixtures();
        }

        private string search = "";
        /// <summary>
        /// Gets or sets the Search query
        /// </summary>

        public string SearchQuery {
            [DebuggerNonUserCode]
            get { return this.search.ToLower(); }
            [DebuggerNonUserCode]
            set {
                if (this.search != value) {
                    this.search = value;
                    SetPropertyChanged("SearchQuery");
                }
            }
        }

        private ICommand _searchCommand;
        public ICommand SearchCommand {
            get {
                if (_searchCommand == null) {
                    _searchCommand = CreateCommand(Search);
                }
                return _searchCommand;
            }
        }
        public void Search(object obj) {
            this.Fixtures.ForEach(fixture => {
                fixture.Matches.ForEach(match => {
                    match.IsVisible = true;
                });
                fixture.SetPropertyChanged("IsVisible");
            });
            if (SearchQuery.Trim() != "") {
                this.Fixtures.ForEach(fixture => {
                    fixture.Matches.ForEach(match => {
                        if (!match.TeamA.ToLower().Contains(SearchQuery) && !match.TeamB.ToLower().Contains(SearchQuery)) {
                            match.IsVisible = false;
                        }
                    });
                    fixture.SetPropertyChanged("IsVisible");
                });
            }
        }

        private void getFixtures() {
            SetBusy(true, "Getting Fixtures...");
            var document = BrowsingContext.New(config).OpenAsync($"{AppState.baseURL}{League.Fixture}").ContinueWith((t) => {
                processFixtures(t);
                SetBusy(false, "Done!");
            });
        }

        private void processFixtures(Task<IDocument> response) {
            Fixtures.ClearItems();
            IDocument document = response.Result;
            var tables = document.QuerySelectorAll(".FTable");
            foreach (var table in tables) {
                var fixture = new FixtureModel();
                var headerRow = table.QuerySelector(".FHeader");
                fixture.Date = headerRow.QuerySelector("td").TextContent;
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
                    }
                    catch (Exception ex) {

                    }

                    fixture.Matches.AddOnUIThread(matchModel);
                }
                Fixtures.AddOnUIThread(fixture);
            }
        }
    }
}
