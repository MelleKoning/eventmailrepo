using EventStore.ClientAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventMailLib
{
    public interface IEventMailCommands
    {
        /// <summary>
        /// Register an email address
        /// </summary>
        /// <param name="userEmail">the email to be registered</param>
        /// <returns>true when command was sent off to eventstore correctly...</returns>
        public bool RegisterUser(UserEmail userEmail);
        /// <summary>
        /// Goal is to retrieve all events for a certain email, so email is the key
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool GetAllEventsFor(string user);
    }
    public class EventMailCommands : IEventMailCommands
    {
        public EventMailCommands(string streamname)
        {
            _streamName = streamname;
        }

        private readonly string _streamName;

        public bool RegisterUser(UserEmail userEmail)
        {
            var registerEmailData = new EventData(
                            Guid.NewGuid(),
                            "REGISTEREMAIL",
                            true,
                            Encoding.UTF8.GetBytes(
                                "{emailaddress:" + userEmail.EmailAddress + "," +
                                "username:" + userEmail.UserName + "," +
                                "password:" + userEmail.Password + "," +
                            "}"),
                            new byte[] { } // metaData... to be determined
                            );

            try
            {
                EventStoreClient.Connection.AppendToStreamAsync(
                    _streamName, // It's probably ok to use same stream for different events
                    ExpectedVersion.Any, // see docs...
                    registerEmailData); // the event created above
            }
            catch (Exception ex)
            {
                // todo some sensible logging: could not Append to stream async?
                Console.WriteLine(ex);
                return false; // or no try..catch and just bubble up the exception for console and webapi to handle it
            }
            return true; 
        }

        public bool GetAllEventsFor(string user)
        {
            throw new NotImplementedException();
        }
    }
}
