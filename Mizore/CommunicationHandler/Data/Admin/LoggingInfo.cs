using System.Collections;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Mizore.ContentSerializer.Data;
using System.Collections.Generic;

namespace Mizore.CommunicationHandler.Data.Admin
{
    public class LoggingInfo
    {
        public LoggingInfo(INamedList nl)
        {
            if (nl.IsNullOrEmpty()) return;
            var list = nl.GetOrDefault<IList>("levels");
            Levels = list.Cast<string>().ToList(); 
            Last = nl.GetOrDefaultStruct<long>("last");
            Buffer = nl.GetOrDefaultStruct<int>("buffer");
            Threshold = nl.GetOrDefault<string>("threshold");
        }
        public LoggingInfo(IList list)
        {
            if (list.IsNullOrEmpty()) return;
            Levels = list.Cast<string>().ToList();
        }

        public List<string> Levels { get; set; }

        public long Last { get; set; }

        public int Buffer { get; set; }

        public string Threshold { get; set; }
    }
}