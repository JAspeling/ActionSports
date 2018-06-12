using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionSports.API {
    public static class Extensions {
        public static void CustomLog(this Exception ex, ILogger logger, string message = "") {
            logger.LogError(message, new object[] { ex });
        }

        public static void Each<T>(this IEnumerable<T> ie, Action<T, int> action) {
            var i = 0;
            foreach (var e in ie) action(e, i++);
        }

        public static void Purify(ref string input) {
            input = input.Trim().Replace("\n", "").Replace("\t", "").Trim();
        }

        public static string Purify( string input) {
            return input.Trim().Replace("\n", "").Replace("\t", "").Trim();
        }

        public static byte[] FromBase64(this string base64) {
            Byte[] bytes = Convert.FromBase64String(base64);
            return bytes;
        }

        public static string ToBase64(this byte[] bytes) {
            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }
    }
}
