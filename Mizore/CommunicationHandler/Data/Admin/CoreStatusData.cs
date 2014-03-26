using System;
using Mizore.util;

namespace Mizore.CommunicationHandler.Data.Admin
{
    public class CoresCoreData
    {
        public CoresCoreData(INamedList responseHeader)
        {
            if (responseHeader.IsNullOrEmpty()) return;
            Name = responseHeader.GetOrDefault<string>("name");
            IsDefaultCore = responseHeader.GetOrDefaultStruct<bool>("isDefaultCore");
            InstanceDir = responseHeader.GetOrDefault<string>("instanceDir");
            DataDir = responseHeader.GetOrDefault<string>("dataDir");
            Config = responseHeader.GetOrDefault<string>("config");
            Schema = responseHeader.GetOrDefault<string>("schema");
            StartTime = responseHeader.GetOrDefaultStruct<DateTime>("startTime");
            Uptime = responseHeader.GetOrDefaultStruct<int>("uptime");
            Index = responseHeader.GetOrDefault<INamedList>("index");
        }

        public string Name { get; set; }

        public bool IsDefaultCore { get; set; }

        public string InstanceDir { get; set; }

        public string DataDir { get; set; }

        public string Config { get; set; }

        public string Schema { get; set; }

        public DateTime StartTime { get; set; }

        public int Uptime { get; set; }

        public INamedList Index { get; set; }
    }
}