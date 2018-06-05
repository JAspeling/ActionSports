using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ActionSports.API.Models {
    public class MatchModel {
        public string Time { get; set; }
        public string Court { get; set; }
        public string TeamA { get; set; }
        public string TeamAHref { get; set; }
        public string TeamB { get; set; }
        public string TeamBHref { get; set; }
        public string Score { get; set; }
        private string scoreHref;
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
    }
}
