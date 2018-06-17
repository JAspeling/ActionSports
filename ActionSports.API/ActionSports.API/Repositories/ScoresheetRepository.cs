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
            try {
                var fileName = $"{match.TeamA} vs {match.TeamB} - {match.Score}.pdf";
                fileName = stripIllegalCharacters(fileName);

                var filePath = Path.Combine(AppState.ScoresheetRepo, fileName);

                if (checkIfFileExists(filePath)) {
                    return File.ReadAllBytes(filePath).ToBase64();
                } else {
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

                    Logger.LogDebug($"Saving '{filePath}'...");

                    // save pdf document
                    doc.Save(filePath);
                    Logger.LogDebug($"'{filePath}' Saved!");
                    // close pdf document
                    doc.Close();

                    Logger.LogDebug($"Closing '{filePath}'...");
                    Logger.LogDebug($"Converting File Bytes to Base64...");

                    var base64 = File.ReadAllBytes(filePath).ToBase64();

                    Logger.LogDebug($"Done!");
                    return base64;
                }
            } catch (Exception ex) {
                ex.CustomLog(Logger, "Failed to convert HTML to PDF and save the file.");
                throw;
            }
        }

        private static string stripIllegalCharacters(string filePath) {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            foreach (char c in invalid) {
                filePath = filePath.Replace(c.ToString(), "");
            }

            return filePath;
        }

        bool checkIfFileExists(string filePath) {
            return File.Exists(filePath);
        }
    }
}
