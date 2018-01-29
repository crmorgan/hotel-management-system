An example Service-Oriented Architecture using distributed system architecture and design practices from
Udi Dahan's [Advanced Distributed Systems Design Course](https://particular.net/adsd "Advanced Distributed Systems Design Course").  One of the assignments in the ADSD course is to design the services and interactions of those services for the domain problem of booking a room at a hotel.  This project is an implementation of that design.

# Setup

To run everything in this project you will need to have the following:

- [NodeJS](https://nodejs.org "NodeJS") 6.0 or higher
- [NPM](https://www.npmjs.com/ "NPM") 3 or higher
- [RavenDB](https://ravendb.net/download) 3.5 or higher
- [Visual Studio 2015](https://www.visualstudio.com/downloads/ "Visual Studio 2015")
- Microsoft MSMQ

The specific setup steps for each of these are detailed below.

## Node and NPM
Install both [NodeJS](https://nodejs.org "NodeJS") and [NPM](https://www.npmjs.com/ "NPM") using the standard instructions provided on their sites. 

## Client Side Packages
The client side application is a Single Page Application (SPA) built using the [Aurelia](http://aurelia.io/) Javascript client framework and uses NPM to manage all the packages.

1. Open a Windows command prompt and go to the `src/PublicWebsite` directory
1. Execute the following command: `npm install` or: `yarn install`

## MSMQ and MSDTC

To ensure MSMQ and MSDTC are installed.

 1. Download and run the [Particular Platform Installer](https://particular.net/start-platform-download).
 1. On the component selection screen select the "Configure MSDTC for NServiceBus" and "Configure Microsoft Message Queuing" options.
  - You can install the other Particular components if you want to try them out but they are not required.

## RavenDB

1. Download the RavenDB server as a zip package from [https://ravendb.net/download](https://ravendb.net/download).
1. Extract contents the zip file to a folder on your machine.

## Visual Studio
1. Open the `src/HotelManagementSystem.sln` solution file in Visual Studio
1. Right click on the solution and select **Restore NuGet Packages**
1. Configure solution's startup projects to start multiple projects
    1. In Solution Explorer right-click on **Solution HotelManagementSystem** and select **Properties**
    1. Under the **Common Properties** folder select **Startup Project**
    1. Select the **Multiple startup projects** option
    1. Change the **Action** for the following projects to **Start**:
        - Availability.Api
        - Finance
        - Finance.Api
        - Guests
        - Guests.Api
        - ITOps
        - Marketing.Api
        - Rates.Api
        - Reservations
        - Reservations.Api
    1. Click **OK**
    2. Make sure everything compiles by pressing <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>B</kbd>

# Running the System

#### 1. Start the RavenDB Server
1. In the folder where you extracted the RavenDB zip double click the `Start.cmd` to start the RavenDB console.

You should now have a RavenDB console applicaiton running and the RavenDB Management Studio open in your web browser at [http://localhost:8080](http://localhost:8080).

#### 2. Start the NServiceBus Hosts and Web APIs
1. From Visual Studio run all the projects by pressing <kbd>Ctrl</kbd>+<kbd>F5</kbd>

You should now have several NServiceBus process hosts running as console applications and a web page for each Web API.  You can close the web pages for the APIs if you like.

#### 3. Start the web site SPA
1. Open a Windows command prompt and go to the `src/PublicWebsite` directory
1. Execute the following command: `npm start` or: `yarn start`
1. In a web browser go to the Hotel Management System home page at [http://localhost:9000](http://localhost:9000 "http://localhost:9000") (or the next available port which is displayed the output of the command).
