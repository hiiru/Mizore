namespace Mizore
{
    /// <summary>
    /// "Mizore"
    /// Temporary Project Codename, based on Mizore Shirayuki (Rosario + Vampire)
    /// Note to myself: Don't choose project codenames while drinking beer and watching anime anymore... ^^
    ///
    /// TODO: JIRA SOLR-12: decide on final project name
    /// </summary>
    public class Mizore
    {
        
        // ConnectionHandler -- Low-level connection
        //-> How to handle HTTP connections?
        //-> How to handle Response? (Headers+Content)
        //-> Exceptions?

        // ContentSerializer -- Connection Content Classes (e.g. JSON, XML, JavaBin)
        //-> Content Serialization (Parsing/Writing)
        //-> Stream<->NamedList
        //-> Allow one-way serializer? also fallback for other way?
        //-> Raw content class? possibly easyier testcases and requests to custom solr handlers.

        // SolrServerHandler -- Solr connection / Low-Lever abstraction/manager
        //-> Manages Server Information and connection instance.
        //-> Simplifies useage (Add, Delete, Commit, etc.)
        //-> Handles additional logic (e.g. loadbalancing, client-side request queue, zookeeper negotiation)
        //-> Handles which Serializer is used

        // CommunicationHandler -- Request/Response / connection content abstraction/manager
        //-> Simplified abstraction for Requests/Responses
        //-> Handles how the Serializer output is used
        //-> Handles which DataMappingHandler is used as data source

        // DataMappingHandler -- Object/Data binding / e.g. NamedList (raw), reflection/attribute mapping, hybrid? (e.g. SolrDocument-like abstraction)
        //-> Handles how the data is read and wrote
        //-> reflection caching/optimization and datareader/-writer handling

        // CacheHandler
        //-> Handles the cache storage management (e.g. HttpCache, in-process dictionary, etc.)
        //-> Manages the lifetime of cached data
        //-> stores cache identifying information (e.g. etags)

        //Open Ideas/Points:
        //-> change detection? do we detect changes of single fields and perform atomic updates? if so, how?
        //-> Solr Admin features? Schema/config, statistics, core management, etc.
        //-> How do we handle Logging?, or rather "DebugHandler", which allows to store logs, but also messure performance metrics (e.g. communicationanalysis, time used per handler, result count, searchterms, etc.)
        //-> Where do we cache and how do we detect/communicate modifications/flushes? (etag?)
        //-> solr 4 _version_ field handling?

        // Additonal Notes:
        //Solrquery with escaping
        //Dismax
        //Solr serializer query version support, e.g. (http://wiki.apache.org/solr/XMLResponseFormat)  -> identify by version, contenttype, wt param

        //Future ideas:
        //-> WebAPI which provides a solr proxy for JS solr implementation like ajax-solr.

        // TBC...
    }
}