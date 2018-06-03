using ActionSpawtz.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionSpawtz.Models {
    public class StandingModel : ModelBase {
        public string Date { get; set; }
        public MatchModels Matches { get; set; } = new MatchModels();

        private bool isVisible = true;
        /// <summary>
        /// Gets or sets the Visibility of the Fixture
        /// </summary>

        public bool IsVisible {
            get { return Matches.All(m => m.IsVisible); }
        }
    }

    public class StandingModels : BaseObservableCollection<StandingModel> {

    }
}
