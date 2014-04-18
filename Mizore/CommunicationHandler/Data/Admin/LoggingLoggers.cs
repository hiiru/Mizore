using System.Collections;
using Mizore.ContentSerializer.Data;
using System.Collections.Generic;

namespace Mizore.CommunicationHandler.Data.Admin
{
    public class LoggingLoggers : List<LoggingLoggers.LogNode>
    {
        public class LogNode
        {
            public string Name { get; set; }

            public string Level { get; set; }

            public bool Set { get; set; }
        }

        public LoggingLoggers(IList list)
        {
            if (list.IsNullOrEmpty()) return;
            foreach (INamedList node in list)
            {
                Add(new LogNode
                {
                    Name = node.GetOrDefault<string>("name"),
                    Level = node.GetOrDefault<string>("level"),
                    Set = node.GetOrDefaultStruct<bool>("set"),
                });
            }
        }
    }
}