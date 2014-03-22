using System;
using System.Collections.Generic;
using System.IO;
using Mizore.CommunicationHandler.Data.Admin;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.util;

namespace Mizore.CommunicationHandler.ResponseHandler.Admin
{
    public class CoresResponse : AResponseBase, IResponse
    {
        public override void Parse(IRequest request, Stream content)
        {
            Request = request;
            Content = Request.Server.Serializer.Unmarshal(content);
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

        protected IEnumerable<CoresCoreData> _cores;

        public IEnumerable<CoresCoreData> Cores
        {
            get
            {
                if (_cores == null && Content != null)
                {
                    var core = Content.GetOrDefault<INamedList>("status");
                    if (core.IsNullOrEmpty())
                        _cores = new List<CoresCoreData>(0);
                    else
                    {
                        var list = new List<CoresCoreData>(core.Count);
                        for (int i = 0; i < core.Count; i++)
                        {
                            var coreItem = core.Get(i) as INamedList;
                            if (coreItem.IsNullOrEmpty()) continue;
                            list.Add(new CoresCoreData(coreItem));
                        }
                        if (list.Count == 1)
                            _defaultCore = list[0].Name;
                        _cores = list;
                    }
                }
                return _cores;
            }
        }
    }
}