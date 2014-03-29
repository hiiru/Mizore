using System;
using System.Collections.Generic;
using System.Text;
using Mizore.CommunicationHandler;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.CommunicationHandler.ResponseHandler.Admin;
using Mizore.ContentSerializer.Data.Solr;
using Mizore.SolrServerHandler;

namespace SimpleTestApp
{
    internal class Program
    {
        private const string SERVERURL = "http://127.0.0.1:20440/solr/";
        private const string SERVERURL_362 = "http://127.0.0.1:20362/solr/";
        private static readonly List<ISolrServerHandler> Servers = new List<ISolrServerHandler>();

        private static void Main(string[] args)
        {
            try
            {
                //Servers.Add(new HttpSolrServer(SERVERURL, new JavaBinSerializer()));
                Servers.Add(new HttpSolrServer(SERVERURL));
                //Servers.Add(new HttpSolrServer(SERVERURL_362, new EasynetJavabinSerializer()));

                foreach (var server in Servers)
                {
                    Console.WriteLine("Checking Server " + server.GetUriBuilder().ServerAddress);
                    Ping(server);
                    SystemInfo(server);
                    Console.WriteLine();
                    Query(server);
                    Console.WriteLine();
                    var docId = DateTime.Now.ToString("yyyyMMdd-HH:mm");
                    CreateDoc(server, docId);
                    Console.WriteLine();
                    Query(server, "id:\"" + docId + "\"");
                    Console.WriteLine();
                }
                Console.WriteLine("Done");
            }
            catch (Exception e)
            {
                Console.WriteLine("EXCEPTION!!!!!!");
                var inner = e;
                while (inner != null)
                {
                    Console.WriteLine(inner.Message);
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
            }
            Console.ReadKey();
        }

        private static void CreateDoc(ISolrServerHandler server, string docId)
        {
            var doc = new SolrInputDocument();
            doc.Fields.Add("id", new SolrInputField("id", docId));
            doc.Fields.Add("name", new SolrInputField("name", "Test Document with ID " + docId));

            //var updateRequest = server.RequestFactory.CreateRequest("update", server, server.DefaultCore, doc);
            //var updateResponse = server.Request<UpdateResponse>(updateRequest);
        }

        private const string TestQuery = "*:*";

        private static void Query(ISolrServerHandler server, string queryString = null)
        {
            if (server == null) return;
            queryString = queryString ?? TestQuery;
            var sbOutput = new StringBuilder();
            var queryRequest = server.RequestFactory.CreateRequest("select", server.GetUriBuilder(), queryBuilder: new SimpleQueryBuilder(queryString));
            var query = server.Request<SelectResponse>(queryRequest);
            sbOutput.AppendFormat("querying for {0}, ResultCount: {1}\n", queryString, query.Documents != null ? query.Documents.NumFound : 0);
            if (query.Documents != null)
            {
                foreach (var doc in query.Documents)
                {
                    var line = new StringBuilder();
                    line.AppendFormat("{0}", doc.Fields["id"]);
                    if (doc.Fields.ContainsKey("price"))
                        line.AppendFormat(", {0}", doc.Fields["price"]);
                    if (doc.Fields.ContainsKey("name"))
                        line.AppendFormat(", {0}", doc.Fields["name"]);
                    sbOutput.AppendLine(line.ToString());
                }
            }
            Console.WriteLine(sbOutput.ToString());
        }

        private static void Ping(ISolrServerHandler server)
        {
            if (server == null) return;
            var sbOutput = new StringBuilder();
            var ping = server.Request<PingResponse>("ping");
            sbOutput.AppendFormat("Status: {0} - QTime: {1}", ping.Status, ping.ResponseHeader.QTime);
            Console.WriteLine(sbOutput.ToString());
        }

        private static void SystemInfo(ISolrServerHandler server)
        {
            if (server == null) return;
            var sbOutput = new StringBuilder();
            var system = server.Request<SystemResponse>("system");
            sbOutput.AppendFormat("Host: {0} servertime: {1} StartTime: {2}", system.Core.Host, system.Core.Now, system.Core.Start);
            Console.WriteLine(sbOutput.ToString());
        }

        //public static Tuple<long, long, long> BenchmarkNamedList(INamedList list, int outerLimit = 5000, int innerMod = 10)
        //{
        //    List<string> keys = Enumerable.Range(0, outerLimit + 1).Select(i => i + "_key").ToList();
        //    var sw = Stopwatch.StartNew();
        //    for (int i = 0; i < outerLimit; i++)
        //    {
        //        string key = keys[i];
        //        for (int j = 0; j <= i % innerMod; j++)
        //        {
        //            var innerlist = new NamedList<int>();
        //            innerlist.Add(key, j);
        //            list.Add(key, list);
        //        }
        //    }
        //    sw.Start();
        //    long write = sw.ElapsedMilliseconds;
        //    sw.Restart();
        //    for (int i = 0; i < outerLimit; i++)
        //    {
        //        var x = list.Get(i);
        //    }
        //    sw.Start();
        //    long getval = sw.ElapsedMilliseconds;
        //    sw.Restart();
        //    for (int i = 0; i < outerLimit; i++)
        //    {
        //        string key = keys[i % outerLimit];
        //        var x = list.Get(key);
        //    }
        //    sw.Start();
        //    long getstr = sw.ElapsedMilliseconds;
        //    return new Tuple<long, long, long>(write, getval, getstr);
        //}

        //public static void BenchmarkNamedLists()
        //{
        //    var type = typeof(INamedList);
        //    var types = AppDomain.CurrentDomain.GetAssemblies().ToList()
        //        .SelectMany(s => s.GetTypes())
        //        .Where(type.IsAssignableFrom);

        //    const int OUTERLIMIT = 5000;
        //    const int INNERMOD = 10;
        //    foreach (Type nlType in types)
        //    {
        //        var fullname = nlType.FullName;
        //        if (fullname == "Mizore.util.INamedList") continue;
        //        Console.WriteLine("Starting benchmark for " + fullname + " with outer:" + OUTERLIMIT + " inner:" + INNERMOD);
        //        try
        //        {
        //            var times = BenchmarkNamedList(Activator.CreateInstance(nlType) as INamedList, OUTERLIMIT, INNERMOD);
        //            Console.WriteLine("Write: " + times.Item1 + "ms GetVal: " + times.Item2 + "ms GetStr: " + times.Item3 + "ms");
        //        }
        //        catch
        //        {
        //            Console.WriteLine("Test for " + fullname + " failes!");
        //        }
        //        Console.WriteLine();
        //    }
        //    Console.WriteLine("Benchmaks done.");
        //    Console.ReadKey();
        //}

        //public static void GeneratePlotCSV()
        //{
        //    const int INNERMOD = 10;

        //    Stopwatch swNamedListAdd = new Stopwatch();
        //    Stopwatch swEasyNamedListAdd = new Stopwatch();
        //    var times = new Dictionary<int, Tuple<long, long>>();
        //    const int INTERVAL = 50;
        //    for (int multi = 1; multi <= 200; multi++)
        //    {
        //        int limit = INTERVAL * multi;
        //        NamedList<object> nl = new NamedList<object>();
        //        EasynetNamedList easynetNamedList = new EasynetNamedList();
        //        swNamedListAdd.Restart();
        //        for (int i = 0; i < limit; i++)
        //        {
        //            string key = i + "_key";
        //            for (int j = 0; j <= i % INNERMOD; j++)
        //            {
        //                var list = new NamedList<int>();
        //                list.Add(key, j);
        //                nl.Add(key, list);
        //            }
        //        }
        //        swNamedListAdd.Stop();
        //        swEasyNamedListAdd.Restart();
        //        for (int i = 0; i < limit; i++)
        //        {
        //            string key = i + "_key";
        //            for (int j = 0; j <= i % INNERMOD; j++)
        //            {
        //                var list = new EasynetNamedList<int>();
        //                list.Add(key, j);
        //                easynetNamedList.Add(key, list);
        //            }
        //        }
        //        swEasyNamedListAdd.Stop();
        //        times[limit] = new Tuple<long, long>(swNamedListAdd.ElapsedTicks, swEasyNamedListAdd.ElapsedTicks);
        //    }
        //    Console.WriteLine("limit;NamedList;EasynetNamedList");
        //    foreach (KeyValuePair<int, Tuple<long, long>> kvp in times)
        //    {
        //        Console.WriteLine(kvp.Key + ";" + kvp.Value.Item1 + ";" + kvp.Value.Item2);
        //    }
        //}
    }
}