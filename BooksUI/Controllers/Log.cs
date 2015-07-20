using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace BooksUI.Controllers
{
    public static class Log
    {
        private static ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void LogError(Exception ex)
        {
            logger.Error(ex.Message);
        }
    }
}