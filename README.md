This is an example of a service oriented distributed system built using NServiceBus as the message bus.  The application is a hotel management system that allows guests to find and book a room at a hotel.  Designing a system like this was a homework assignment from Udi Dahan's [Advanced Distributed Systems Design Course](https://particular.net/adsd "Advanced Distributed Systems Design Course").

### Getting Started

Before you start you will need [Visual Studio 2015](https://www.visualstudio.com/downloads/ "Visual Studio 2015"), [NodeJS](https://nodejs.org "NodeJS") 6.0 or higher, [NPM](https://www.npmjs.com/ "NPM") 3 or higher, and RavenDB 3.5 or higher.

#### Server Side Processes and APIs
#### RavenDB

1. Download RavenDB from https://ravendb.net/download and extract it.
1. Open the extracted folder and double click the `Start.cmd` file to start the RavenDB console.  This will also open the RavenDB Management Studio in your web browser at http://localhost:8080.

#### Visual Studio
1. Open the src/HotelManagementSystem.sln solution file in Visual Studio 2015 or higher
1. Right click on the solution and select **Restore NuGet Packages**
1. Run the all the server side processes and APIs by pressing `CTRL+F5` 

#### Client Application
The client side application is a SPA build with the Aurelia framework.

From the src/PublicWebsite folder, execute the following commands:

    npm install # or: yarn install

To run the application execute the following command:

    npm start # or: yarn start

You can now browse to the hotel management web application at [http://localhost:9000](http://localhost:9000 "http://localhost:9000") (or the next available port which is displayed the output of the command).
