using ActionSpawtz.BaseClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ActionSpawtz.Models {
    public class VenueModel {

        public string Title { get; set; }
        public string Href { get; set; }

        public override string ToString() {
            return Title;
        }
    }

    public class VenueModels : BaseObservableCollection<VenueModel> {

    }
}
