using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ActionSpawtz {
    public static class AppState {
        public static readonly HttpClient client = new HttpClient();
        public static IConfiguration config = Configuration.Default.WithDefaultLoader();
        public static string baseURL = "http://actionsport.spawtz.com";
    }
}
