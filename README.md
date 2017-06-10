This is an example of a service oriented distributed system built using NServiceBus as the message bus.  The application is a hotel management system that allows guests to find and book a room at a hotel.  Designing a system like this was a homework assignment from Udi Dahan's [Advanced Distributed Systems Design Course](https://particular.net/adsd "Advanced Distributed Systems Design Course").

### Getting Started

Before you start you will need [Visual Studio 2015](https://www.visualstudio.com/downloads/ "Visual Studio 2015") and a recent version of [NodeJS](https://nodejs.org "NodeJS") environment 6.0 or higher and [NPM](https://www.npmjs.com/ "NPM") 3 or higher.

#### Server Side Processes and APIs
1. Open the `src/HotelManagementSystem.sln` solution file in Visual Studio 2015 or higher.
2. Restore NuGet packages for the solution
3. Make sure the solution compiles

To run the server side processes and APIs run the solution using `CTRL+F5`. 

#### Client Application
The client side application is a SPA build with the Aurelia framework.

From the `src/PublicWebsite` folder, execute the following commands:

    npm install # or: yarn install

This will install all required dependencies, including a local version of Webpack that is going to build and bundle the app. There is no need to install Webpack globally.

To run the application execute the following command:

    npm start # or: yarn start

You can now browse to the hotel management web application at [http://localhost:9000](http://localhost:9000 "http://localhost:9000") (or the next available port which is displayed the output of the command).
