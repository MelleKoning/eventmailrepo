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
        /// This is for looking up the user based on the current registered (and validated) emailaddress
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public UserEmail GetUser(string email);
        /// <summary>
        /// Email is validated
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ValidateEmail(string email);
        /// <summary>
        /// A user can change the emailaddress; for example when logged in already
        /// we create an event to notify the old address about the change
        /// and sent a validation about the action to the new address
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public bool ChangeEmailFor(UserEmail userEmail, string newEmailaddress);
        /// <summary>
        /// Remove the user.
        /// The fact that user is removed only after 30 days is not handled here,
        /// but should be handled in the CONSUMER of the event, so the consumer
        /// maybe wants to place an additional 
        /// event itself that takes place in 30 days from now.
        /// TODO: passing only current email is probably not good enough, have to know the actual GUID of user
        /// so that all previously used emailaddresses are also removed for this user...
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public bool RemoveUser(UserEmail userEmail);

        /// <summary>
        /// Goal is to retrieve all events for a certain user/email, possibly have to change the key to a unique GUID for registration instead,
        /// also to handle the email-change functionality.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>

        public bool GetAllEventsFor(string user);
    }
    public class EventMailCommands : IEventMailCommands
    {
        public EventMailCommands(IEventStoreConnection connection, string streamname)
        {
            _connection = connection;
            _streamName = streamname;
        }

        private readonly string _streamName;
        private readonly IEventStoreConnection _connection;

        public bool RegisterUser(UserEmail userEmail)
        {
            var registerEmailData = new EventData(
                            Guid.NewGuid(),
                            "REGISTEREMAIL",
                            true,
                            System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(userEmail), // use .net core serialization for Utf8Bytes..
                            /*Encoding.UTF8.GetBytes(
                                "{emailaddress:" + userEmail.EmailAddress + "," +
                                "username:" + userEmail.UserName + "," +
                                "password:" + userEmail.Password + "," +
                            "}"),*/
                            new byte[] { } // metaData... to be determined
                            ); ; ;

            try
            {
                _connection.AppendToStreamAsync(
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

            // TODO probably within the Consumer/Projections of the event, thus not here: 
            // Sent out an email to the registered address for a validation link
            // When the user clicks this validation link, we should execute
            // the ValidateEmail action
            return true; 
        }

        public bool GetAllEventsFor(string user)
        {
            throw new NotImplementedException();
        }

        public bool ValidateEmail(string email)
        {
            // lookup the email in SQL store to check if it exists
            // set validation flag
            throw new NotImplementedException();
        }

        public bool ChangeEmailFor(UserEmail userEmail, string newEmailaddress)
        {
            throw new NotImplementedException();
        }

        public bool RemoveUser(UserEmail userEmail)
        {
            throw new NotImplementedException();
        }

        public UserEmail GetUser(string email)
        {
            throw new NotImplementedException();
        }
    }
}
