using ActionSpawtz.Models;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Parser.Html;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionSpawtz.ViewModels {
    public class ScoreSheetVM : ViewModelBase {
        public MatchModel MatchModel { get; set; }

        private string url = "";
        /// <summary>
        /// Gets or sets the URL of the scoresheet
        /// </summary>

        public string Url {
            [DebuggerNonUserCode]
            get { return this.url; }
            [DebuggerNonUserCode]
            set {
                if (this.url != value) {
                    this.url = value;
                    SetPropertyChanged("Url");
                }
            }
        }


        private string htmlContent = "<p>Test</p>";
        /// <summary>
        /// Gets or sets the HTML COntent
        /// </summary>

        public string HtmlContent {
            get { return this.htmlContent; }
            set {
                if (this.htmlContent != value) {
                    this.htmlContent = value;
                    SetPropertyChanged("HtmlContent");
                }
            }
        }

        private string filePath = "";
        /// <summary>
        /// Gets or sets the FilePath
        /// </summary>

        public string FilePath {
            [DebuggerNonUserCode]
            get { return this.filePath; }
            [DebuggerNonUserCode]
            set {
                if (this.filePath != value) {
                    this.filePath = value;
                    SetPropertyChanged("FilePath");
                }
            }
        }

        internal void Initalize(MatchModel matchModel) {
            this.MatchModel = matchModel;
            getScoreSheet();
        }

        private void getScoreSheet() {
            SetBusy(true, "Getting Scoresheet...");
            Url = $"{AppState.baseURL}{MatchModel.ScoreHref}";
            ConvertToPdf();
            //var document = BrowsingContext.New(AppState.config).OpenAsync(url).ContinueWith((t) => {
            //    processStandings(t);
            //    SetBusy(false, "Done!");
            //});
        }

        private void processStandings(Task<IDocument> response) {
            HtmlContent = "";
            var parser = new HtmlParser();
            IDocument document = response.Result;
            HtmlContent = document.DocumentElement.InnerHtml;
        }

        public void ConvertToPdf() {
            string url = Url;

            string pdf_page_size = "A4";
            PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize), pdf_page_size, true);

            string pdf_orientation = "Portrait";
            PdfPageOrientation pdfOrientation = (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation), pdf_orientation, true);

            int webPageWidth = 1024;

            int webPageHeight = 0;

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = pageSize;
            converter.Options.PdfPageOrientation = pdfOrientation;
            converter.Options.WebPageWidth = webPageWidth;
            converter.Options.WebPageHeight = webPageHeight;

            SetBusy(true, "Retrieving PDF...");
            PdfDocument doc = null;
            Task.Factory.StartNew(() => {
                // create a new pdf document converting an url
                doc = converter.ConvertUrl(url);
            }).ContinueWith((t) => {
                // save pdf document
                doc.Save("Sample.pdf");
                // close pdf document
                doc.Close();

                FilePath = "Sample.pdf";

                FilePath = Path.GetFullPath(FilePath);

                SetBusy(false, "Done!");
            });

        }
    }
}
