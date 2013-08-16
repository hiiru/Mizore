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
        private static void Main(string[] args)
        {
            //BenchmarkNamedLists();
            //return;
            //var server = new HttpSolrServer("http://127.0.0.1:8080/solr", new EasynetJavabinSerializer());
            var server = new HttpSolrServer("http://127.0.0.1:8983/solr", new EasynetJavabinSerializer());
            var ping = server.Ping();
            if (ping.ContentENL != null)
            {
                var reqHead = ping.ContentENL.Get("responseHeader") as SimpleOrderedMap;
                Console.WriteLine("requestHeader:");
                Console.WriteLine("\tstatus: " + reqHead.Get("status"));
                Console.WriteLine("\tQTime: " + reqHead.Get("QTime"));
                Console.WriteLine("status: " + ping.ContentENL.Get("status"));
            }
            Console.WriteLine("Raw:");
            Console.WriteLine(ping.ContentString);
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