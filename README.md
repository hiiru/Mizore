# Project "Mizore"

This is a simple C# solr library project codenamed "Mizore".<br/>
The name and state of the projects (documentation, tests, etc.) will improve when we leave the alpha

The goal of this project is to create a small but extendable library which is fast, stable and offers extensibility.

**NOTE:<br/> This is a very early alpha implementation and NOT yet intended for production use!<br/>
All the Interfaces, classes and other elements are still subject to change and might break every check-in.**

## Current Status

**Query**: Minimalistic Querying is possible (a complete QueryBuilder is currently missing)<br/>
**Update**: Implemented<br/>
**Serializer**: Json (based on JSON.NET), JavaBin (Note: post in javabin is untested)<br/>
**Object Mapping**: in progress, Reflection-based mapper implemented, but not integrated yet<br/>
**Cache**: missing / not implemented yet<br/>
**Admin Requests**: Implemented Ping, System and Cores<br/>
**Get Request**: Implemented<br/>

## Current priority TODO Queue (in no specific order)
Simple cache example (mostly a proof of concept of the interface integration)<br/>
better QueryBuilder(s)<br/>
Decide on final project name<br/>
Better Exception handling and types<br/>

----

Need a .NET solr library today?<br/>
check out solrnet (http://code.google.com/p/solrnet/) and easynet (http://easynet.codeplex.com).

-----

## Architecture

The core idea of the Project is to create an easy yet powerful framework for the solr communication.
To achieve this the Project is split into multiple layers, each layer focuses on their problem and accepts/passes the data in a defined interface.
So that every part can be changed individually, which allows to create custom handles easily and help with testing and performance analysis between versions.

### Layers

All Layers are currently Work in Progress, so their roles might sill change.
Also there is no order in this list nor any diagram which visualized how they work together yet.

#### ConnectionHandler -- Low-level connection
This Handler controls how the Connections are handled, in most case this will be a HTTP connection.

However even in the. .NET framework there are multiple ways of creating Requests. some are synchronized others asynchronous. So this layers allows you to have the power over the core connection.

In Theory this should enable some basic implementation of features like connection pooling or shard-awareness, Some of them might overlap with the SolrServerHandler.
Also some exotic things like tunneling the HTTP connection over SSH to access some distant network should be possible.

#### ContentSerializer -- Connection Content Classes (e.g. JSON, XML, JavaBin)
In the ContentSerializer the Data is de-/serialized from a Data-Object to a serialized format for the ConnectionHandler.

This allows to control the used formats and serialization libraries.
You always wanted to use csv/php/ruby format in C#? start here :P

Also other cases could be covered, e.g. a wrapper which uses JSON on DEBUG and javabin on live or special content filtering

#### CommunicationHandler -- Request/Response / connection content abstraction/manager
The CommunicationHandler understand the data structure of solr (NamedList) and converts them into a typed class.

They consist of 2 Parts (and optional data classes), Request and Response.<br/>
The Request is passed to the ConnectionHandler (and ContentSerializer) which returns a Response object.<br/>
Request -> ConnectionHandler/ContentSerializer -> Response

#### DataMappingHandler -- Object/Data binding
**Note: Work in Progess**, the mapper exists but isn't integrated in the design yet.<br/>
Still TBD how this layer will be integrated, However this will handle the conversion of Solr Documents to Data-Objects.<br/>
e.g. NamedList (raw), reflection/attribute mapping, etc. (aka all the voodoo to provide you with your "MyApplicationDocument" out of solr :) )

This might also handle how the SelectRequest will be created (e.g. fl param optimization).

#### CacheHandler - well, caching
**Note**: this is till TODO, **not implemented yet**!<br/>
This Handler will do everything related to caching and defines how the data is cached.

A simple implementation could be a KV Store based on request etag and/or request hashcode combined with an expiration.

There are 2 main points in which Caching might be beneficial: Solr Responses (parsed into raw NamedList) and fully parsed .NET objects (post-reflection)

#### SolrServerHandler -- Solr connection/manager
This is the Core element which managed what Handlers are used and prepare the requests, which is heavily inspired by solrj but created with .NET and the libraries design in mind.

When you access solr in your external project, it will be most likely thru this Layer.

This should allow you to implement all kind of thing, connection pooling, multi-server/farm/cloud, solr analytics/debugging or even creating special handlers like an embedded solr server (e.g. by using IKVM.NET and solrj)

## Tests and Documentation
In the alpha the features and basic functionality is more important than an up-to-date tests project and documentation, so it might be out of sync.

However as soon as the Project reaches beta, this will change. A Library is only as good as it's documentation and tests.
