using ActionSpawtz.Models;
using ActionSpawtz.Views;
using AngleSharp;
using AngleSharp.Dom;
using CsQuery;
using CsQuery.ExtensionMethods;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace ActionSpawtz.ViewModels {
    public class MainVM : ViewModelBase {

        //POST

        //  var values = new Dictionary<string, string>
        //  {
        //      { "thing1", "hello" },
        //      { "thing2", "world" }
        //  };

        //  var content = new FormUrlEncodedContent(values);

        //  var response = await client.PostAsync("http://www.example.com/recepticle.aspx", content);

        //  var responseString = await response.Content.ReadAsStringAsync();
        //  GET

        //  var responseString = await client.GetStringAsync("http://www.example.com/recepticle.aspx");


        public MainVM() {
            Initialize();
        }

        public void Initialize() {
            getLeagues(null);
            getVenues();
        }

        IConfiguration config = Configuration.Default.WithDefaultLoader();
        public VenueModels Venues { get; set; } = new VenueModels();
        public LeagueModels Leagues { get; set; } = new LeagueModels();

        public FixturesVM FixturesVM { get; set; } = new FixturesVM();
        public StandingsVM StandingsVM { get; set; } = new StandingsVM();

        private ICommand _ConvertToPdfCommand;
        public ICommand ConvertToPdfCommand {
            get {
                if (_ConvertToPdfCommand == null) {
                    _ConvertToPdfCommand = CreateCommand(ConvertToPdf);
                }
                return _ConvertToPdfCommand;
            }
        }
        public void ConvertToPdf(object obj) {
            string url = "https://actionsport.spawtz.com/External/Fixtures/CricketScoreSheet.aspx?FixtureId=1518645";

            string pdf_page_size = "A4";
            PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize), pdf_page_size, true);

            string pdf_orientation = "Portrait";
            PdfPageOrientation pdfOrientation = (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation), pdf_orientation, true);

            int webPageWidth = 1024;

            int webPageHeight = 0;

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = pageSize;
            converter.Options.PdfPageOrientation = pdfOrientation;
            converter.Options.WebPageWidth = webPageWidth;
            converter.Options.WebPageHeight = webPageHeight;

            SetBusy(true, "Retrieving PDF...");
            PdfDocument doc = null;
            Task.Factory.StartNew(() => {
                // create a new pdf document converting an url
                doc = converter.ConvertUrl(url);
            }).ContinueWith((t) => {
                // save pdf document
                doc.Save("Sample.pdf");
                // close pdf document
                doc.Close();
                SetBusy(false, "Done!");
            });

        }

        private VenueModel selectedVenue = null;
        /// <summary>
        /// Gets or sets the Selected Venue
        /// </summary>

        public VenueModel SelectedVenue {
            [DebuggerNonUserCode]
            get { return this.selectedVenue; }
            set {
                if (this.selectedVenue != value) {
                    this.selectedVenue = value;
                    getLeagues($"{AppState.baseURL}{value.Href}");
                    SetPropertyChanged("SelectedVenue");
                }
            }
        }

        private LeagueModel selectedLeague = null;
        /// <summary>
        /// Gets or sets the Selected league
        /// </summary>

        public LeagueModel SelectedLeague {
            [DebuggerNonUserCode]
            get { return this.selectedLeague; }
            set {
                if (this.selectedLeague != value) {
                    this.selectedLeague = value;
                    SetPropertyChanged("SelectedLeague");
                }
            }
        }

        private void getLeagues(string url) {
            if (url != null) {
                SetBusy(true, "Getting Leagues...");
                AppState.client.GetStringAsync(url).ContinueWith(t => {
                    processLeagues(t);
                    SetBusy(false, "Done!");
                });
            }
        }

        private void processLeagues(Task<string> response) {
            Leagues.ClearItems();
            CQ dom = response.Result;
            var table = dom["table tr:not(.LSport)"];
            var results = from row in table select row;
            foreach (var row in table) {
                var league = new LeagueModel();
                CQ rowDom = row.Render();
                var cols = rowDom["td"];

                cols.ForEach((td, index) => {
                    CQ text = td.Render();
                    var href = "";
                    var anchor = td.FirstChild;
                    anchor.TryGetAttribute("href", out href);
                    switch (index) {
                        case 0: league.Title = anchor.Render().Replace("\t", "").Replace("\n", ""); break;
                        case 1: league.Fixture = href; break;
                        case 2: league.Standing = href; break;
                    }
                });
                Leagues.AddOnUIThread(league);
            }
        }

        private void getVenues() {

            SetBusy(true, "Getting Venues...");
            AppState.client.GetStringAsync("http://actionsport.spawtz.com/External/Fixtures/").ContinueWith(t => {
                processVenues(t);
                SetBusy(false, "Done!");
            });
        }

        private void processVenues(Task<string> response) {
            Venues.ClearItems();
            CQ dom = response.Result;
            var venueList = dom["div .Content a"];
            foreach (var anchor in venueList) {
                var venue = new VenueModel();

                venue.Href = anchor.GetAttribute("href");
                venue.Title = anchor.InnerText;

                Venues.AddOnUIThread(venue);
            }
        }

        private ICommand _GetStandingsCommand;
        public ICommand GetStandingsCommand {
            get {
                if (_GetStandingsCommand == null) {
                    _GetStandingsCommand = CreateCommand(GetStandings);
                }
                return _GetStandingsCommand;
            }
        }
        public void GetStandings(object obj) {
            var window = $"Standings - {SelectedVenue.Title}, {SelectedLeague.Title}".GetNewWindowInstance();
            var standingsView = new StandingsView();
            standingsView.DataContext = this.StandingsVM;
            window.Content = standingsView;
            StandingsVM.Initialize(SelectedLeague);

            window.ShowDialog();
        }
        
        private ICommand _GetFixturesCommand;
        public ICommand GetFixturesCommand {
            get {
                if (_GetFixturesCommand == null) {
                    _GetFixturesCommand = CreateCommand(GetFixtures);
                }
                return _GetFixturesCommand;
            }
        }
        public void GetFixtures(object obj) {
            var window = $"Fixtures - {SelectedVenue.Title}, {SelectedLeague.Title}".GetNewWindowInstance();
            var fixturesView = new FixturesView();
            fixturesView.DataContext = this.FixturesVM;
            window.Content = fixturesView;

            FixturesVM.Initialize(SelectedLeague);

            window.ShowDialog();
            
        }
    }
}
