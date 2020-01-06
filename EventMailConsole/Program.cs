using System;
using EventMailLib;
using EventStore.ClientAPI;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace EventMailConsole
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("EventMailConsole application");

            EventMailCommands eventMailCommands = getAvailableCommands();

            var cmd = new RootCommand();
            
            cmd.AddCommand(greeting()); // dummy test command
            cmd.AddCommand(RegisterUser(eventMailCommands));

            return cmd.InvokeAsync(args).Result;
            
        }

        private static EventMailCommands getAvailableCommands()
        {
            // create connection via the EventMailLib
            EventStoreClient eClient = new EventStoreClient();
            eClient.CreateConnection();

            // Have a handle to the available commands
            string streamname = "InputFromFileConsoleApp";
            EventMailCommands eventMailCommands = new EventMailCommands(eClient.Connection, streamname);

            return eventMailCommands;
        }
        private static Command greeting()
        {
            var cmd = new Command("greeting", "Shows a greeting");
            cmd.Handler = CommandHandler.Create(() => {
                Console.WriteLine("Hello world");
            });
            return cmd;
        }

        private static Command RegisterUser(EventMailCommands eventMailCommands)
        {
            var cmd = new Command("register", "Registers a new useremail");
            cmd.AddOption(new Option("email", "emailaddress to be registered")
                {
                    Argument = new Argument<string>()
                }
            );
            cmd.AddOption(new Option("password", "password for login")
                {
                    Argument = new Argument<string>()
                }
            );

            cmd.Handler = CommandHandler.Create<string, string>((email, password) =>
            {
                UserEmail userEmail = new UserEmail();
                userEmail.EmailAddress = email;
                userEmail.Password = password;
                eventMailCommands.RegisterUser(userEmail);

                Console.WriteLine("user registered...");
            });

            return cmd;
        }
    }
}
