/** License / Porting note
 * This File is ported from the apache solr sourcecode
 * org.apache.solr.common.params.CollectionParams
 * 
 * Original License text:
 * 
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
namespace Mizore.CommunicationHandler.Data.Params
{
    public static class CollectionParams
    {
        /** What action **/
        public const string ACTION = "action";
        public const string NAME = "name";

        public enum CollectionAction
        {
            CREATE,
            DELETE,
            RELOAD,
            SYNCSHARD,
            CREATEALIAS,
            DELETEALIAS,
            SPLITSHARD,
            DELETESHARD,
            CREATESHARD,
            DELETEREPLICA,
            MIGRATE,
            ADDROLE,
            REMOVEROLE,
            CLUSTERPROP,
            REQUESTSTATUS,
            ADDREPLICA,
            OVERSEERSTATUS
        }
    }
}