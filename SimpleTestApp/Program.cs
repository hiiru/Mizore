using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Mizore.ContentSerializer;
using Mizore.ContentSerializer.easynet_Javabin;
using Mizore.SolrServerHandler;
using Mizore.util;

namespace SimpleTestApp
{
    internal class Program
    {
        private const string SERVERURL = "http://127.0.0.1:20440/solr/";
        private const string SERVERURL_362 = "http://127.0.0.1:20362/solr/";

        private static void Main(string[] args)
        {
            Console.WriteLine("Pining solr server: " + SERVERURL);
            Console.WriteLine();
            var server = new HttpSolrServer(SERVERURL, new EasynetJavabinSerializer());
            var server_362 = new HttpSolrServer(SERVERURL_362, new EasynetJavabinSerializer());
            var ping = server.Ping();
            Console.WriteLine("Ping URL: "+ping.Request.Url);
            Console.WriteLine("Ping Status: " + ping.Status);
            Console.WriteLine();
            Console.WriteLine("RequestHeader:");
            Console.WriteLine("\tstatus: " + ping.ResponseHeader.Status);
            Console.WriteLine("\tQTime: " + ping.ResponseHeader.QTime);
            if (ping.ResponseHeader.Parameters != null)
            {
                for (int i = 0; i < ping.ResponseHeader.Parameters.Count; i++)
                {
                    Console.WriteLine("\t\t" + ping.ResponseHeader.Parameters.GetKey(i) + ": " + ping.ResponseHeader.Parameters.Get(i));
                }
            }
            Console.WriteLine();

            var system = server.GetSystemInfo();
            Console.WriteLine("System URL: " + system.Request.Url);
            Console.WriteLine("System Mode: " + system.Mode);
            Console.WriteLine();
            Console.WriteLine("System Core Info: ");
            Console.WriteLine("\tSchema: " + system.Core.Schema);
            Console.WriteLine("\tHost: " + system.Core.Host);
            Console.WriteLine("\tServer Time: " + system.Core.Now);
            Console.WriteLine("\tStart Time: " + system.Core.Start);
            if (system.Core.Directory != null)
            {
                for (int i = 0; i < system.Core.Directory.Count; i++)
                {
                    Console.WriteLine("\t\t" + system.Core.Directory.GetKey(i) + ": " + system.Core.Directory.Get(i));
                }
            }
            Console.WriteLine();
            Console.WriteLine("RequestHeader:");
            Console.WriteLine("\tstatus: " + system.ResponseHeader.Status);
            Console.WriteLine("\tQTime: " + system.ResponseHeader.QTime);
            if (system.ResponseHeader.Parameters != null)
            {
                for (int i = 0; i < system.ResponseHeader.Parameters.Count; i++)
                {
                    Console.WriteLine("\t\t" + system.ResponseHeader.Parameters.GetKey(i) + ": " + system.ResponseHeader.Parameters.Get(i));
                }
            }
            Console.WriteLine();



            Console.WriteLine("Done");
            Console.ReadKey();
        }

        public static Tuple<long, long, long> BenchmarkNamedList(INamedList list, int outerLimit = 5000, int innerMod = 10)
        {
            List<string> keys = Enumerable.Range(0, outerLimit + 1).Select(i => i + "_key").ToList();
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < outerLimit; i++)
            {
                string key = keys[i];
                for (int j = 0; j <= i % innerMod; j++)
                {
                    var innerlist = new NamedList<int>();
                    innerlist.Add(key, j);
                    list.Add(key, list);
                }
            }
            sw.Start();
            long write = sw.ElapsedMilliseconds;
            sw.Restart();
            for (int i = 0; i < outerLimit; i++)
            {
                var x = list.Get(i);
            }
            sw.Start();
            long getval = sw.ElapsedMilliseconds;
            sw.Restart();
            for (int i = 0; i < outerLimit; i++)
            {
                string key = keys[i % outerLimit];
                var x = list.Get(key);
            }
            sw.Start();
            long getstr = sw.ElapsedMilliseconds;
            return new Tuple<long, long, long>(write, getval, getstr);
        }

        public static void BenchmarkNamedLists()
        {
            var type = typeof(INamedList);
            var types = AppDomain.CurrentDomain.GetAssemblies().ToList()
                .SelectMany(s => s.GetTypes())
                .Where(type.IsAssignableFrom);

            const int OUTERLIMIT = 5000;
            const int INNERMOD = 10;
            foreach (Type nlType in types)
            {
                var fullname = nlType.FullName;
                if (fullname == "Mizore.util.INamedList") continue;
                Console.WriteLine("Starting benchmark for " + fullname + " with outer:" + OUTERLIMIT + " inner:" + INNERMOD);
                try
                {
                    var times = BenchmarkNamedList(Activator.CreateInstance(nlType) as INamedList, OUTERLIMIT, INNERMOD);
                    Console.WriteLine("Write: " + times.Item1 + "ms GetVal: " + times.Item2 + "ms GetStr: " + times.Item3 + "ms");
                }
                catch
                {
                    Console.WriteLine("Test for " + fullname + " failes!");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Benchmaks done.");
            Console.ReadKey();
        }

        public static void GeneratePlotCSV()
        {
            const int INNERMOD = 10;

            Stopwatch swNamedListAdd = new Stopwatch();
            Stopwatch swEasyNamedListAdd = new Stopwatch();
            var times = new Dictionary<int, Tuple<long, long>>();
            const int INTERVAL = 50;
            for (int multi = 1; multi <= 200; multi++)
            {
                int limit = INTERVAL * multi;
                NamedList<object> nl = new NamedList<object>();
                EasynetNamedList easynetNamedList = new EasynetNamedList();
                swNamedListAdd.Restart();
                for (int i = 0; i < limit; i++)
                {
                    string key = i + "_key";
                    for (int j = 0; j <= i % INNERMOD; j++)
                    {
                        var list = new NamedList<int>();
                        list.Add(key, j);
                        nl.Add(key, list);
                    }
                }
                swNamedListAdd.Stop();
                swEasyNamedListAdd.Restart();
                for (int i = 0; i < limit; i++)
                {
                    string key = i + "_key";
                    for (int j = 0; j <= i % INNERMOD; j++)
                    {
                        var list = new EasynetNamedList<int>();
                        list.Add(key, j);
                        easynetNamedList.Add(key, list);
                    }
                }
                swEasyNamedListAdd.Stop();
                times[limit] = new Tuple<long, long>(swNamedListAdd.ElapsedTicks, swEasyNamedListAdd.ElapsedTicks);
            }
            Console.WriteLine("limit;NamedList;EasynetNamedList");
            foreach (KeyValuePair<int, Tuple<long, long>> kvp in times)
            {
                Console.WriteLine(kvp.Key + ";" + kvp.Value.Item1 + ";" + kvp.Value.Item2);
            }
        }
    }
}