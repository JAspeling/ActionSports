using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionSports.API.Models {
    public class LeagueModel {
        private string title;
        public string Title {
            get {
                return title;
            }
            set {
                if (this.title != value) {
                    if (value != null) {
                        Extensions.Purify(ref value);
                    }
                    this.title = value;
                }
            }
        }
        public string Fixture { get; set; }
        public string Standing { get; set; }

        public override string ToString() {
            return Title;
        }
    }

    public class LeagueModels : List<LeagueModel> {

    }
}
