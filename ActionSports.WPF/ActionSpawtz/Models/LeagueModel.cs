using ActionSpawtz.BaseClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ActionSpawtz.Models {
    public class LeagueModel : ModelBase {
        public LeagueModel() {

        }

        public LeagueModel(string name, string fixtureRef, string standingRef) {
            this.Title = name;
            this.Fixture = fixtureRef;
            this.Standing = standingRef;
        }

        public string Title { get; set; }
        public string Fixture { get; set; }
        public string Standing { get; set; }

        public override string ToString() {
            return Title;
        }
    }

    public class LeagueModels : BaseObservableCollection<LeagueModel> {
    }
}
