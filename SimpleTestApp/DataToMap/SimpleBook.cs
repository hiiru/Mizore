using System;
using System.Collections.Generic;
using Mizore.DataMappingHandler.Attributes;

namespace SimpleTestApp.DataToMap
{
    public class SimpleBook
    {
        [SolrIdField("id")]
        public string Iban { get; set; }

        [SolrField("title_s")]
        public string Title { get; set; }

        [SolrField("description")]
        public string Description { get; set; }

        [SolrField("author",2f)]
        public string Author { get; set; }

        [SolrField("pages_i")]
        public int Pages { get; set; }

        [SolrField("price")]
        public decimal Price { get; set; }

        [SolrField("released_dt")]
        public DateTime ReleaseDate { get; set; }

        [SolrField("inprint_b")]
        public bool InPrint { get; set; }

        [SolrField("tags_ss")]
        public List<string> Tags { get; set; }
    }
}