using ActionSpawtz.BaseClasses;
using ActionSpawtz.ViewModels;
using ActionSpawtz.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ActionSpawtz.Models {
    public class MatchModel : ModelBase {
        private string time = "";
        /// <summary>
        /// Gets or sets the Time
        /// </summary>

        public string Time {
            [DebuggerNonUserCode]
            get { return this.time; }
            [DebuggerNonUserCode]
            set {
                if (this.time != value) {
                    if (value != null) {
                        Extensions.Purify(ref value);
                    }
                    this.time = value;
                    SetPropertyChanged("Time");
                }
            }
        }

        private string court = "";
        /// <summary>
        /// Gets or sets the Court
        /// </summary>

        public string Court {
            [DebuggerNonUserCode]
            get { return this.court; }
            [DebuggerNonUserCode]
            set {
                if (this.court != value) {
                    if (value != null) {
                        Extensions.Purify(ref value);
                    }
                    this.court = value;
                    SetPropertyChanged("Court");
                }
            }
        }

        private string teamA = "";
        /// <summary>
        /// Gets or sets the TeamA
        /// </summary>

        public string TeamA {
            [DebuggerNonUserCode]
            get { return this.teamA; }
            [DebuggerNonUserCode]
            set {
                if (this.teamA != value) {
                    if (value != null) {
                        Extensions.Purify(ref value);
                    }
                    this.teamA = value;
                    SetPropertyChanged("TeamA");
                }
            }
        }

        private string teamAHref = "";
        /// <summary>
        /// Gets or sets the Href attribute text for the team A
        /// </summary>

        public string TeamAHref {
            [DebuggerNonUserCode]
            get { return this.teamAHref; }
            [DebuggerNonUserCode]
            set {
                if (this.teamAHref != value) {
                    this.teamAHref = value;
                    SetPropertyChanged("TeamAHref");
                }
            }
        }

        private string teamB = "";
        /// <summary>
        /// Gets or sets the Team B
        /// </summary>

        public string TeamB {
            [DebuggerNonUserCode]
            get { return this.teamB; }
            [DebuggerNonUserCode]
            set {
                if (this.teamB != value) {
                    if (value != null) {
                        Extensions.Purify(ref value);
                    }
                    this.teamB = value;
                    SetPropertyChanged("TeamB");
                }
            }
        }

        private string teamBHref = "";
        /// <summary>
        /// Gets or sets the Href attribute for team B
        /// </summary>

        public string TeamBHref {
            [DebuggerNonUserCode]
            get { return this.teamBHref; }
            [DebuggerNonUserCode]
            set {
                if (this.teamBHref != value) {
                    this.teamBHref = value;
                    SetPropertyChanged("TeamBHref");
                }
            }
        }

        private string score = "";
        /// <summary>
        /// Gets or sets the Score for the match
        /// </summary>

        public string Score {
            get { return this.score; }
            set {
                if (this.score != value) {
                    if (value != null) {
                        Extensions.Purify(ref value);
                    }
                    this.score = value;
                    SetPropertyChanged("Score");
                }
            }
        }

        private string scoreHref = "";
        public string ScoreHref {
            get { return scoreHref; }
            set {
                if (value != scoreHref) {
                    Extensions.Purify(ref value);
                    string pattern = @"\'(.*?)\'";
                    RegexOptions options = RegexOptions.Multiline;

                    foreach (Match m in Regex.Matches(value, pattern, options)) {
                        value = m.Value;
                        break;
                    }
                    scoreHref = value.Replace("\'", "");
                }
            }
        }

        private bool isVisible = true;
        /// <summary>
        /// Gets or sets the Visibility of the Match Model
        /// </summary>

        public bool IsVisible {
            [DebuggerNonUserCode]
            get { return this.isVisible; }
            [DebuggerNonUserCode]
            set {
                if (this.isVisible != value) {
                    this.isVisible = value;
                    SetPropertyChanged("IsVisible");
                }
            }
        }

        private ICommand _OpenScoreSheetCommand;
        public ICommand OpenScoreSheetCommand {
            get {
                if (_OpenScoreSheetCommand == null) {
                    _OpenScoreSheetCommand = CreateCommand(OpenScoreSheet);
                }
                return _OpenScoreSheetCommand;
            }
        }
        public void OpenScoreSheet(object obj) {
            var match = obj as MatchModel;
            if (match == null) return;
            var window = $"Scoresheet - {match.Score}".GetNewWindowInstance();
            var scoreView = new ScoreSheetView();
            var scoresheetVM = new ScoreSheetVM();
            scoresheetVM.Initalize(match);
            scoreView.DataContext = scoresheetVM;
            window.Content = scoreView;
            window.Show();
        }

    }

    public class MatchModels : BaseObservableCollection<MatchModel> {

    }
}
