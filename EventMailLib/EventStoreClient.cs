using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using EventStore.ClientAPI;
using EventStore.ClientAPI.Projections;
using EventStore.ClientAPI.SystemData;

namespace EventMailLib
{
    /// <summary>
    /// This represents the EventStoreClient for interacting with the eventStore...
    /// 
    /// ...should be getting certain commands to sent events and retrieve lists of earlier commands sent...
    /// note: running the actual EventStore SERVER is considered out of scope, so this class
    /// only serves as a 'stub' for the connection
    /// </summary>
    public static class EventStoreClient
    {
        public static IEventStoreConnection Connection { get; private set; }
    }
}