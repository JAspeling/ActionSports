using ActionSports.API.Models;
using ActionSports.API.Services;
using Microsoft.Extensions.Logging;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ActionSports.API.Repositories {
    public class ScoresheetRepository : IScoresheetRepository {
        public ScoresheetRepository(ILoggerFactory logger) {
            Logger = logger.CreateLogger<ScoresheetRepository>();
        }

        public ILogger Logger { get; }

        // need to do some sort of caching by retrievingthe file only if it has been converted before.

        public string ConvertToPdf(MatchModel match, string url) {
            Logger.LogDebug($@"Converting {url} to PDF...");
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

            PdfDocument doc = null;
            // create a new pdf document converting an url
            doc = converter.ConvertUrl(url);

            var fileName = $"{match.Score}.pdf";

            Logger.LogDebug($"Saving '{fileName}'...");

            // save pdf document
            doc.Save(fileName);
            Logger.LogDebug($"'{fileName}' Saved!");
            // close pdf document
            doc.Close();


            Logger.LogDebug($"Closing '{fileName}'...");

            var filePath = Path.GetFullPath(fileName);

            Logger.LogDebug($"Converting File Bytes to Base64...");

            var base64 = File.ReadAllBytes(filePath).ToBase64();

            Logger.LogDebug($"Done!");
            return base64;
        }
    }
}
