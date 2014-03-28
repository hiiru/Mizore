using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mizore;
using Mizore.ContentSerializer.JsonNet;
using Mizore.Data;
using Newtonsoft.Json;

namespace SimpleTestApp
{
    internal class JsonDev
    {
        public static void Main(string[] args)
        {
            foreach (var file in Directory.GetFiles(@"..\..\TestJson"))
            {
                //Console.WriteLine(file);
                //PrintNamedList(ParseJsonFile(file));
                //Console.WriteLine();

                var json = File.ReadAllText(file);
                var list = JsonConvert.DeserializeObject<NamedList>(json, new JsonSerializerSettings() { Converters = new List<JsonConverter> { new SolrJsonConverter() } });
                var json2 = JsonConvert.SerializeObject(list, new JsonSerializerSettings() { Converters = new List<JsonConverter> { new SolrJsonConverter() } });
            }

            //var json = File.ReadAllText(@"..\..\..\MizoreTests\Resources\ResponseFiles\ping.json");
            //var list = JsonConvert.DeserializeObject<NamedList>(json, new JsonSerializerSettings() { Converters = new List<JsonConverter> { new SolrJsonConverter() } });
            //var json2 = JsonConvert.SerializeObject(list, new JsonSerializerSettings() { Converters = new List<JsonConverter> { new SolrJsonConverter() } });
        }

        protected static INamedList ParseJsonFile(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException();
            var json = File.ReadAllText(filename);
            try
            {
                return JsonConvert.DeserializeObject<NamedList>(json,
                    new JsonSerializerSettings() { Converters = new List<JsonConverter> { new SolrJsonConverter() } });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        protected static void PrintNamedList(INamedList list, int level = 0)
        {
            var prefix = Tabs(level);
            if (level > 10)
            {
                Console.WriteLine(prefix + "too much recursion");
                return;
            }

            if (list.IsNullOrEmpty())
            {
                Console.WriteLine(prefix + "List is empty.");
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(prefix + list.GetKey(i));
                var item = list.Get(i);
                if (item is INamedList)
                {
                    PrintNamedList(item as INamedList, level + 1);
                }
            }
        }

        private static string Tabs(int numTabs)
        {
            IEnumerable<string> tabs = Enumerable.Repeat(" ", numTabs);
            return (numTabs > 0) ? tabs.Aggregate((sum, next) => sum + next) : "";
        }
    }
}