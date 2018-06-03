using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionSports.API.Models {
    public class VenueModel {
        public string Title { get; set; }
        public string Href { get; set; }

        public override string ToString() {
            return Title;
        }
    }

    public class VenueModels : List<VenueModel> {

    }
}
