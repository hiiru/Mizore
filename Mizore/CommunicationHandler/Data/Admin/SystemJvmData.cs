using Mizore.Data;

namespace Mizore.CommunicationHandler.Data.Admin
{
    public class SystemJvmData
    {
        public SystemJvmData(INamedList responseHeader)
        {
            //NOTE: change INamedList to dataobject, if ever used
            if (responseHeader.IsNullOrEmpty()) return;
            Version = responseHeader.GetOrDefault<string>("version");
            Name = responseHeader.GetOrDefault<string>("name");
            Spec = responseHeader.GetOrDefault<INamedList>("spec");
            Jre = responseHeader.GetOrDefault<INamedList>("jre");
            Vm = responseHeader.GetOrDefault<INamedList>("vm");
            Processors = responseHeader.GetOrDefaultStruct<int>("processors");
            Memory = responseHeader.GetOrDefault<INamedList>("memory");
            Jmx = responseHeader.GetOrDefault<INamedList>("jmx");
        }

        public string Version { get; protected set; }

        public string Name { get; protected set; }

        public INamedList Spec { get; protected set; }

        public INamedList Jre { get; protected set; }

        public INamedList Vm { get; protected set; }

        public int Processors { get; protected set; }

        public INamedList Memory { get; protected set; }

        public INamedList Jmx { get; protected set; }
    }
}