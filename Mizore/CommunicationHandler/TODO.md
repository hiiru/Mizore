# CommunicationHandler TODO List


## Important

### Select 

### Update

### Get

## Admin / Debug / Management

TODO: Definie which one we will implement.

INFO: these urls are based on the example 4.4.0 solr server with example data imported

### Logging
http://127.0.0.1:20440/solr/collection1/admin/logging?wt=json&since=0

### Properties
http://127.0.0.1:20440/solr/collection1/admin/properties?wt=json

### Threads
http://127.0.0.1:20440/solr/collection1/admin/threads?wt=json

### Replication
http://127.0.0.1:20440/solr/collection1/replication?command=details&wt=json

### DataImportHandler
https://127.0.0.1:20440/solr/collection1/dataimport?command=details&wt=json

### Analysis Fields + document
FAIL: http://127.0.0.1:20440/solr/collection1/analysis/field?wt=json&analysis.showmatch=true&analysis.fieldvalue=test&analysis.query=test&analysis.fieldname=_version_

SUCCESS: http://127.0.0.1:20440/solr/collection1/analysis/field?wt=json&analysis.showmatch=true&analysis.fieldvalue=test&analysis.query=test&analysis.fieldname=author

TODO: example url for document

### Luke
http://127.0.0.1:20440/solr/collection1/admin/luke?wt=json&show=index&numTerms=0

### File / Tpl
http://127.0.0.1:20440/solr/collection1/admin/file?file=solrconfig.xml&contentType=text/xml;charset=utf-8
http://127.0.0.1:20440/solr/tpl/file.html?_=1395498159508

### mbeans
http://127.0.0.1:20440/solr/collection1/admin/mbeans?cat=QUERYHANDLER&wt=json

## Other

Example hanlder or special handler
### Query

### Browse
### Elevate


### Spell
### Terms

### tvrh

### Debug

/admin/plugins

/debug/dump