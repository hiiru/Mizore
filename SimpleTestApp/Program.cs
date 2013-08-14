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
            var server = new HttpSolrServer("http://127.0.0.1:8080/solr", new EasynetJavabinSerializer());
			  //var server = new HttpSolrServer("http://127.0.0.1:8983/solr",new EasynetJavabinSerializer());
            var ping = server.Ping();
			  if (ping.ContentENL!=null)
			  {
				  var reqHead = ping.ContentENL.Get("responseHeader") as SimpleOrderedMap;
				  Console.WriteLine("requestHeader:");
				  Console.WriteLine("\tstatus: " + reqHead.Get("status"));
				  Console.WriteLine("\tQTime: " + reqHead.Get("QTime"));
				  Console.WriteLine("status: "+ping.ContentENL.Get("status"));
			  }
			  Console.WriteLine("Raw:");
            Console.WriteLine(ping.ContentString);
            Console.WriteLine("Done");
            Console.ReadKey();
        }
        public static void BenchmarkNamedLists()
        {
            const int OUTERLIMIT = 5000;
            const int INNERMOD = 10;
            NamedList<object> nl = new NamedList<object>();
            EasynetNamedList easynetNamedList = new EasynetNamedList();

            Stopwatch swNamedListAdd = new Stopwatch();
            Stopwatch swEasyNamedListAdd = new Stopwatch();
            Stopwatch swNamedListGetId = new Stopwatch();
            Stopwatch swEasyNamedListGetId = new Stopwatch();
            Stopwatch swNamedListGetKey = new Stopwatch();
            Stopwatch swEasyNamedListGetKey = new Stopwatch();
            List<string> keys = Enumerable.Range(0, OUTERLIMIT+1).Select(i => i + "_key").ToList();

            swNamedListAdd.Start();
            for (int i = 0; i < OUTERLIMIT; i++)
            {
                string key = keys[i];
                for (int j = 0; j <= i % INNERMOD; j++)
                {
                    var list = new NamedList<int>();
                    list.Add(key, j);
                    nl.Add(key, list);
                }
            }
            swNamedListAdd.Stop();
            swEasyNamedListAdd.Start();
            for (int i = 0; i < OUTERLIMIT; i++)
            {
                string key = keys[i];
                for (int j = 0; j <= i % INNERMOD; j++)
                {
                    var list = new EasynetNamedList<int>();
                    list.Add(key, j);
                    easynetNamedList.Add(key, list);
                }
            }
            swEasyNamedListAdd.Stop();
            swNamedListGetId.Start();
            for (int i = 0; i < OUTERLIMIT; i++)
            {
                string key = keys[i % OUTERLIMIT];
                var x = nl.Get(i);
            }
            swNamedListGetId.Stop();
            swEasyNamedListGetId.Start();
            for (int i = 0; i < OUTERLIMIT; i++)
            {
                string key = keys[i % OUTERLIMIT];
                var x = easynetNamedList.GetVal(i);
            }
            swEasyNamedListGetId.Stop();
            swNamedListGetKey.Start();
            for (int i = 0; i < OUTERLIMIT; i++)
            {
                string key = keys[i % OUTERLIMIT];
                var x = nl.Get(key);
            }
            swNamedListGetKey.Stop();
            swEasyNamedListGetKey.Start();
            for (int i = 0; i < OUTERLIMIT; i++)
            {
                string key = keys[i % OUTERLIMIT];
                var x = easynetNamedList.Get(key);
            }
            swEasyNamedListGetKey.Stop();


            Console.WriteLine("Add speedtest (" + OUTERLIMIT + " limit, " + INNERMOD + " mod):");
            Console.WriteLine("\tNamedList: " + swNamedListAdd.ElapsedMilliseconds + "ms");
            Console.WriteLine("\tEasynetNamedList: " + swEasyNamedListAdd.ElapsedMilliseconds + "ms");
            Console.WriteLine("GetId speedtest (" + OUTERLIMIT + " limit, " + INNERMOD + " mod):");
            Console.WriteLine("\tNamedList: " + swNamedListGetId.ElapsedMilliseconds + "ms");
            Console.WriteLine("\tEasynetNamedList: " + swEasyNamedListGetId.ElapsedMilliseconds + "ms");
            Console.WriteLine("GetKey speedtest (" + OUTERLIMIT + " limit, " + INNERMOD + " mod):");
            Console.WriteLine("\tNamedList: " + swNamedListGetKey.ElapsedMilliseconds + "ms");
            Console.WriteLine("\tEasynetNamedList: " + swEasyNamedListGetKey.ElapsedMilliseconds + "ms");
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
                    for (int j = 0; j <= i%INNERMOD; j++)
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
                    for (int j = 0; j <= i%INNERMOD; j++)
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