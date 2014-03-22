using System;
using System.Collections.Generic;
using System.IO;
using Mizore.CommunicationHandler.Data;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.util;
using Mizore;

namespace Mizore.CommunicationHandler.ResponseHandler.Admin
{
    public class CoresResponse : IResponse
    {
        public void Parse(IRequest request, Stream content)
        {
            Request = request;
            Content = Request.Server.Serializer.Unmarshal(content);

            Mode = Content.Get("mode") as string;
        }

        public IRequest Request { get; protected set; }

        public INamedList Content { get; protected set; }

        protected ResponseHeader _responseHeader;

        public ResponseHeader ResponseHeader
        {
            get
            {
                if (_responseHeader == null && Content != null)
                {
                    var head = Content.Get("responseHeader") as INamedList;
                    if (head != null)
                    {
                        _responseHeader = new ResponseHeader
                        {
                            Status = (int)head.Get("status"),
                            QTime = (int)head.Get("QTime"),
                            Parameters = head.Get("params") as INamedList,
                        };
                    }
                }
                return _responseHeader;
            }
        }

        protected string _defaultCore;

        public string DefaultCore
        {
            get
            {
                if (_defaultCore == null && Content != null)
                {
                    _defaultCore = Content.Get("defaultCoreName") as string;
                }
                return _defaultCore;
            }
        }

        protected IEnumerable<CoreData> _cores;

        public IEnumerable<CoreData> Cores
        {
            get
            {
                if (_cores == null && Content != null)
                {
                    var core = Content.GetOrDefault<INamedList>("status");
                    if (core.IsNullOrEmpty())
                        _cores = new List<CoreData>(0);
                    else
                    {
                        var list = new List<CoreData>(core.Count);
                        for (int i = 0; i < core.Count; i++)
                        {
                            var coreItem = core.Get(i) as INamedList;
                            if (coreItem.IsNullOrEmpty()) continue;
                            var coreData = new CoreData
                            {
                                Name = coreItem.GetOrDefault<string>("name"),
                                IsDefaultCore = coreItem.GetOrDefaultStruct<bool>("isDefaultCore"),
                                InstanceDir = coreItem.GetOrDefault<string>("instanceDir"),
                                DataDir = coreItem.GetOrDefault<string>("dataDir"),
                                Config = coreItem.GetOrDefault<string>("config"),
                                Schema = coreItem.GetOrDefault<string>("schema"),
                                StartTime = coreItem.GetOrDefaultStruct<DateTime>("startTime"),
                                Uptime = coreItem.GetOrDefaultStruct<int>("uptime"),
                                Index = coreItem.GetOrDefault<INamedList>("index")
                            };
                            list.Add(coreData);
                        }
                        if (list.Count == 1)
                            _defaultCore = list[0].Name;
                        _cores = list;
                    }
                }
                return _cores;
            }
        }

        public string Mode { get; protected set; }

        public class CoreData
        {
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
}