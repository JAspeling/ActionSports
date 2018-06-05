using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionSports.API.Models {
    public class StandingsModel {
        public string Date { get; set; }
        public List<MatchModel> Matches { get; set; } = new List<MatchModel>();
    }
}
