#ContentSerializer TODO

## 1. Implement JSON serializer
JSON <-> NamedList

## 2. Review Data structure
Check where NamedList is the right format, and where it would be better to serialize to an custom object (e.g. Document list)

## 3. Implement JavaBin Serializer which respects our dataclasses
create an javabin serializer which doesn't use the easynet classes.

This will allow cleanup of the project (remove obsolete classes and allow a single license to cover the whole project)