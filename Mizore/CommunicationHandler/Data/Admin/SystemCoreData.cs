using Mizore.ContentSerializer.Data;
using System;

namespace Mizore.CommunicationHandler.Data.Admin
{
    public class SystemCoreData
    {
        public SystemCoreData(INamedList nl)
        {
            if (nl.IsNullOrEmpty()) return;
            Schema = nl.GetOrDefault<string>("schema");
            Host = nl.GetOrDefault<string>("host");
            Now = nl.GetOrDefaultStruct<DateTime>("now");
            Start = nl.GetOrDefaultStruct<DateTime>("start");
            Directory = nl.GetOrDefault<INamedList>("directory");
        }

        public string Schema { get; protected set; }

        public string Host { get; protected set; }

        public DateTime Now { get; protected set; }

        public DateTime Start { get; protected set; }

        public INamedList Directory { get; protected set; }
    }
}