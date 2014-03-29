# Data/Solr

In this directory are all the solr dataclasses which are benefical for this project.
Those are ported from the java solr sourcecode and adapted for .NET.

The main usecase is javabin de-/serialization and it simplifies the understandig of the solr structure/source because the data classes have the same names and capabillities.


These classes will be a lowlevel structure, similar to the NamedList, and in most cases be invisible to the users of the library. One exception might be the solrdocument* classes, which might be used directly when no object mapping is used.