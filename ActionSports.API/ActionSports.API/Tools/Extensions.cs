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
    }
}
