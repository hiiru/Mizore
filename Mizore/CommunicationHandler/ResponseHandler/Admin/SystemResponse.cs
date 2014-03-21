using System;
using System.IO;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.ResponseHandler.ResponseData;
using Mizore.util;

namespace Mizore.CommunicationHandler.ResponseHandler.Admin
{
    public class SystemResponse : IResponse
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

        protected CoreData _core;

        public CoreData Core
        {
            get
            {
                if (_core == null && Content != null)
                {
                    var core = Content.Get("core") as INamedList;
                    if (core != null)
                    {
                        _core = new CoreData
                            {
                                Schema = core.Get("schema") as string,
                                Host = core.Get("host") as string,
                                Now = (DateTime)core.Get("now"),
                                Start = (DateTime)core.Get("start"),
                                Directory = core.Get("directory") as INamedList
                            };
                    }
                }
                return _core;
            }
        }

        public string Mode { get; protected set; }

        public class CoreData
        {
            public string Schema { get; set; }

            public string Host { get; set; }

            public DateTime Now { get; set; }

            public DateTime Start { get; set; }

            public INamedList Directory { get; set; }
        }
    }
}