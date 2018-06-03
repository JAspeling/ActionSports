using ActionSpawtz.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace ActionSpawtz {
    public abstract class ModelBase : INotifyPropertyChanged {
        protected ICommand CreateCommand(Action<object> executeAction) {
            if (executeAction == null)
                throw new ArgumentNullException("executeAction");

            return new RelayCommand(executeAction);
        }

        private bool isBusy = false;
        /// <summary>
        /// Gets or sets the Busy Indicator of the ViewModel
        /// </summary>
        [XmlIgnore()]
        public bool IsBusy {
            [DebuggerNonUserCode]
            get { return this.isBusy; }
            [DebuggerNonUserCode]
            set {
                if (this.isBusy != value) {
                    this.isBusy = value;
                    SetPropertyChanged("IsBusy");
                }
            }
        }

        private string busyContent = "";
        /// <summary>
        /// Gets or sets the BusyContent to be displayed
        /// </summary>

        public string BusyContent {
            [DebuggerNonUserCode]
            get { return this.busyContent; }
            [DebuggerNonUserCode]
            set {
                if (this.busyContent != value) {
                    this.busyContent = value;
                    SetPropertyChanged("BusyContent");
                }
            }
        }

        public void SetBusy(bool isBusy, string busyContent) {
            IsBusy = isBusy;
            BusyContent = busyContent;
        }

        public void InvokeOnUIThread(Action action) {
            if (Application.Current == null) throw new NullReferenceException("No instance of the Current Application can be found");
            if (Application.Current.Dispatcher == null) throw new NullReferenceException("The Dispatcher of the Current Application is null");
            Application.Current.Dispatcher.Invoke(action);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void SetPropertyChanged(string propName) {
            var handler = PropertyChanged;
            if (handler != null) {
                InvokeOnUIThread(() => {
                    handler.Invoke(this, new PropertyChangedEventArgs(propName));
                });
            }
        }
    }
}
