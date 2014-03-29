using System;
using Mizore.ContentSerializer.Data;

namespace Mizore.CommunicationHandler.Data.Admin
{
    public class SystemCoreData
    {
        public SystemCoreData(INamedList responseHeader)
        {
            if (responseHeader.IsNullOrEmpty()) return;
            Schema = responseHeader.GetOrDefault<string>("schema");
            Host = responseHeader.GetOrDefault<string>("host");
            Now = responseHeader.GetOrDefaultStruct<DateTime>("now");
            Start = responseHeader.GetOrDefaultStruct<DateTime>("start");
            Directory = responseHeader.GetOrDefault<INamedList>("directory");
        }

        public string Schema { get; protected set; }

        public string Host { get; protected set; }

        public DateTime Now { get; protected set; }

        public DateTime Start { get; protected set; }

        public INamedList Directory { get; protected set; }
    }
}