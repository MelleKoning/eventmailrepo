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
    public class EventStoreClient
    {
        public IEventStoreConnection Connection { get; private set; }
        /// <summary>
        /// Code found to setup a connection, exposed via Connection property
        /// </summary>
        public void CreateConnection()
        {
            var conn = EventStoreConnection.Create(new Uri("tcp://admin:changeit@localhost:1113"),
                    "InputFromFileConsoleApp");
            conn.ConnectAsync().Wait();
            Connection = conn;
            
            /*
            var tcp = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1113);
            var http = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2113);
            Connection = EventStoreConnection.Create(tcp);
            Connection.ConnectAsync().Wait();
            var pManager = new ProjectionsManager( new NullLogger(), http, TimeSpan.FromSeconds(5));
            var creds = new UserCredentials("admin", "changeit");
            bool ready = false;
            int retry = 0;
            while (!ready)
            {
                try
                {
                    pManager.EnableAsync("$streams", creds).Wait();
                    pManager.EnableAsync("$by_event_type", creds).Wait();
                    pManager.EnableAsync("$by_category", creds).Wait();
                    pManager.EnableAsync("$stream_by_category", creds).Wait();
                    ready = true;
                }
                catch
                {
                    retry++;
                    if (retry > 8)
                        throw new Exception("EventStore Projection Start Error.");
                    System.Threading.Thread.Sleep(250);
                }
            }*/
        }

        /// <summary>
        /// We need to pass an EventStore.ILogger, do not (yet) know why but lets give it an implementation
        /// </summary>
        public class NullLogger : ILogger
        {
            public void Debug(string format, params object[] args)
            {
            }

            public void Debug(Exception ex, string format, params object[] args)
            {
            }

            public void Error(string format, params object[] args)
            {
            }

            public void Error(Exception ex, string format, params object[] args)
            {
            }

            public void Info(string format, params object[] args)
            {
            }

            public void Info(Exception ex, string format, params object[] args)
            {
            }
        }
    }
    
}