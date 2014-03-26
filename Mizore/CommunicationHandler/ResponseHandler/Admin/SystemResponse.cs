using System.IO;
using Mizore.CommunicationHandler.Data.Admin;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.util;

namespace Mizore.CommunicationHandler.ResponseHandler.Admin
{
    public class SystemResponse : AResponseBase, IResponse
    {
        public override void Parse(IRequest request, Stream content)
        {
            Request = request;
            Content = Request.Server.Serializer.Unmarshal(content);
        }

        protected SystemCoreData _core;

        public SystemCoreData Core
        {
            get
            {
                if (_core == null && Content != null)
                {
                    _core = new SystemCoreData(Content.GetOrDefault<INamedList>("core"));
                }
                return _core;
            }
        }

        protected SystemLuceneData _lucene;

        public SystemLuceneData Lucene
        {
            get
            {
                if (_lucene == null && Content != null)
                {
                    _lucene = new SystemLuceneData(Content.GetOrDefault<INamedList>("lucene"));
                }
                return _lucene;
            }
        }

        protected SystemJvmData _jvm;

        public SystemJvmData Jvm
        {
            get
            {
                if (_jvm == null && Content != null)
                {
                    _jvm = new SystemJvmData(Content.GetOrDefault<INamedList>("jvm"));
                }
                return _jvm;
            }
        }

        protected SystemSystemData _system;

        public SystemSystemData System
        {
            get
            {
                if (_system == null && Content != null)
                {
                    _system = new SystemSystemData(Content.GetOrDefault<INamedList>("system"));
                }
                return _system;
            }
        }

        protected string _mode;

        public string Mode
        {
            get
            {
                if (_mode == null && Content != null)
                {
                    _mode = Content.GetOrDefault<string>("mode");
                }
                return _mode;
            }
        }
    }
}