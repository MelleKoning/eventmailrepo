# eventmailrepo
Test for eventstore and mail registration 

#goal

The goal of this project:
- Investigate eventsourcing in C# .Net Core 
- Use the assignment text as provided in opdracht.txt
- This readme used to write down progress, remarks and questions when these pop-up.

#Initial Considerations
When looking at 'opdracht.txt' several design decisions are already made:
- One core library has the interface that is going to be used by the console app and the webapp.
- This core library should be able to communicate with eventstore -> makes it an eventstore client generating events...
- Therefore created three projects in the solution:

##EventMailLib
Core library that should contain the eventstore client so that it is able to communicate
events with the EventStore server
Further implementation to be done.

##EventMailConsole
The console application with a reference to EventMailLib so that it can use the EventMailLib exposed interface
Note: Although 'opdracht.txt' says '.Net framework console app' probably just a console app is meant, otherwise
      we would have to deal with .Net Standard, .Net Framework and .Net Core compatibility issues, not considered part
      of a small project now. However, if .Net Framework would be meant, use of 
      appropriate versions becomes important as can be seen at:
      https://docs.microsoft.com/en-us/dotnet/standard/net-standard

##WebAppEventMail
The web application with a reference to EventMailLib so that it can use the EventMailLib exposed interface

##EventMailLib
Contains the interface to communicate with the eventstore 
Added the eventstore client 5.0.5 as per shown .NET Cli command of:
https://www.nuget.org/packages/EventStore.Client/

Questions: 
What is the scope of the core library? 
 1) Should this also contain the consumer of events?
 2) What is the goal of the SQL database; should the actual registrated emailaddresses be in the SQL database (instead of eventstorage)? Probably yes.
 3) What is meant by 'key' in the 'opdracht.txt' what kind of 'key' to use? Would the emailaddress itself be a good key? Would eventstore hand out
 a nice key as a handle? Should we use a generated GUID that links with a username or an email? Hmmm.

 Looking at part 2) functionality: 
 - Let's add a user class
 Now what would be a good example of an event 'command' so that an emailaddress can be registered?
 
 (Reading up on EventStore, API Client)
 Class EventStoreClient: The command needs to be sent, so just have an EventStore.ClientApi.Connection to sent any command/eventtype to a 'stream'
 Class EventMailCommands: use the Connection of EventStoreClient to expose commands for the library.

 Later: The emailaddress is change-able so can't be the unique identifiable key for the user, let's use the username for that then.