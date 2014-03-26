using Mizore.util;

namespace Mizore.CommunicationHandler.Data.Admin
{
    public class SystemSystemData
    {
        public SystemSystemData(INamedList responseHeader)
        {
            if (responseHeader.IsNullOrEmpty()) return;
            Name = responseHeader.GetOrDefault<string>("name");
            Version = responseHeader.GetOrDefault<string>("version");
            Arch = responseHeader.GetOrDefault<string>("arch");
            SystemLoadAverage = responseHeader.GetOrDefaultStruct<double>("systemLoadAverage");
            CommitedVirtualMemorySize = responseHeader.GetOrDefaultStruct<long>("committedVirtualMemorySize");
            FreePhysicalMemorySize = responseHeader.GetOrDefaultStruct<long>("freePhysicalMemorySize");
            FreeSwapSpaceSize = responseHeader.GetOrDefaultStruct<long>("freeSwapSpaceSize");
            ProcessCpuTime = responseHeader.GetOrDefaultStruct<long>("processCpuTime");
            TotalPhysicalMemorySize = responseHeader.GetOrDefaultStruct<long>("totalPhysicalMemorySize");
            TotalSwapSpaceSize = responseHeader.GetOrDefaultStruct<long>("totalSwapSpaceSize");
        }

        public string Name { get; protected set; }

        public string Version { get; protected set; }

        public string Arch { get; protected set; }

        public double SystemLoadAverage { get; protected set; }

        public long CommitedVirtualMemorySize { get; protected set; }

        public long FreePhysicalMemorySize { get; protected set; }

        public long FreeSwapSpaceSize { get; protected set; }

        public long ProcessCpuTime { get; protected set; }

        public long TotalPhysicalMemorySize { get; protected set; }

        public long TotalSwapSpaceSize { get; protected set; }
    }
}