using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using Mizore.ContentSerializer;
using Mizore.ContentSerializer.JavaBin;

namespace SimpleTestApp
{
    class JavaBinDev
    {
        public static void Main(string[] args)
        {
            var solrBackCompTestfile = File.ReadAllBytes(@"..\..\TestJavaBin\javabin_backcompat.bin");
            var myJB = new SolrJavaBinConverter();
            var myList = myJB.ReadJavaBin(new MemoryStream(solrBackCompTestfile));
            myJB = new SolrJavaBinConverter();
            var ms = new MemoryStream();
            myJB.WriteJavaBin(myList, ms);
            File.WriteAllBytes(@"..\..\TestJavaBin\javabin_backcompat-test.bin", ms.ToArray());
            var solrBackCompTestfile2 = File.ReadAllBytes(@"..\..\TestJavaBin\javabin_backcompat-test.bin");
            myJB = new SolrJavaBinConverter();
            var myList2 = myJB.ReadJavaBin(new MemoryStream(solrBackCompTestfile));
            myJB = new SolrJavaBinConverter();
            var ms2 = new MemoryStream();
            myJB.WriteJavaBin(myList2, ms2);
            File.WriteAllBytes(@"..\..\TestJavaBin\javabin_backcompat-test2.bin", ms.ToArray());

            //var javabin = File.ReadAllBytes(@"..\..\..\MizoreTests\Resources\ResponseFiles\ping.javabin");
            //var javabin = File.ReadAllBytes(@"..\..\TestJavaBin\selectAllExampleDocs.bin");
            //for (int i = 0; i < 10000; i++)
            //    Parse(javabin);
        }

        public static void Parse(byte[] bytes)
        {
            var myJB = new JavaBinSerializer();
            var myList = myJB.Unmarshal(new MemoryStream(bytes));
        }
    }
}
