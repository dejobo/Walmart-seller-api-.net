using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalmartAPI.Classes
{
    static class General
    {
        public static DataContext GetContext()
        {
            return new DataContext();
        }
        public static DataContext GetContext(Action<string> log)
        {
            Log.Verbose("Creating contex with logger");
            var ctx = new DataContext();
            ctx.Database.Log = log;
            return ctx;
        }
    }
}
