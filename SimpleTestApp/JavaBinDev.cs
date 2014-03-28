using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using Mizore.ContentSerializer;
using Mizore.ContentSerializer.JavaBin;
using Mizore.util;

namespace SimpleTestApp
{
    class JavaBinDev
    {
        public static void Main(string[] args)
        {
            var solrBackCompTestfile = File.ReadAllBytes(@"..\..\TestJavaBin\javabin_backcompat.bin");
            Parse(solrBackCompTestfile);

            //var javabin = File.ReadAllBytes(@"..\..\..\MizoreTests\Resources\ResponseFiles\ping.javabin");
            //for (int i = 0; i < 100000; i++)
            //    Parse(javabin);
        }

        public static void Parse(byte[] bytes)
        {
        //    var enJB = new EasynetJavabinSerializer();
        //    var enList = enJB.Unmarshal(new MemoryStream(bytes));

            var myJB = new JavaBinSerializer();
            var myList = myJB.Unmarshal(new MemoryStream(bytes));
            
        }
    }
}
