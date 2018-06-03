using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;

namespace ActionSpawtz {
    public static class Extensions {
        public static void AddRange<T>(this ObservableCollection<T> obj, List<T> items) {
            InvokeOnUIThread(() => {
                foreach (var item in items)
                    obj.Add(item);
            });
        }

        public static void InvokeOnUIThread(Action action) {
            if (Application.Current == null) throw new NullReferenceException("No instance of the Current Application can be found");
            if (Application.Current.Dispatcher == null) throw new NullReferenceException("The Dispatcher of the Current Application is null");
            Application.Current.Dispatcher.Invoke(action);
        }

        public static RadWindow GetNewWindowInstance(this string title) {
            var window = new RadWindow() { Header = title, Width = 800, Height = 600, WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen };
            window.Loaded += (s, e) => {
                if (window.Parent is Window) {
                    Window thisWindow = window.Parent as Window;
                    thisWindow.ShowInTaskbar = true;
                    thisWindow.Title = window.Header.ToString();
                }
            };
            return window;
        }

        public static void Purify(ref string input) {
            input = input.Trim().Replace("\n", "").Replace("\t", "").Trim();
        }
    }
}
