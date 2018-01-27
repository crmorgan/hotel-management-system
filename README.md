An example Service-Oriented Architecture using distributed system architecture and design practices from
Udi Dahan's [Advanced Distributed Systems Design Course](https://particular.net/adsd "Advanced Distributed Systems Design Course").  One of the assignments in the ADSD course is to design the services and interactions of those services for the domain problem of booking a room at a hotel.  This project is an implementation of that design.

# System Requirements

To run everything you will need:

- [NodeJS](https://nodejs.org "NodeJS") 6.0 or higher
- [NPM](https://www.npmjs.com/ "NPM") 3 or higher
- [RavenDB](https://ravendb.net/download) 3.5 or higher
- [Visual Studio 2015](https://www.visualstudio.com/downloads/ "Visual Studio 2015")
- Microsoft MSMQ

The specific setup steps for each of these are detailed below.

## Node and NPM
Install both [NodeJS](https://nodejs.org "NodeJS") and [NPM](https://www.npmjs.com/ "NPM") using the standard instructions provided on their sites. 

## Aurelia SPA
The client side application is a Single Page Application build using the [Aurelia](http://aurelia.io/) Javascript client framework.

#### Install the required JavaScript packages

1. Open a Windows command prompt and go to the `src/PublicWebsite` directory
1. Execute the following command: `npm install` or: `yarn install`


#### Run the application

1. Execute the following command: `npm start` or: `yarn start`

You can now browse to the hotel management web application at [http://localhost:9000](http://localhost:9000 "http://localhost:9000") (or the next available port which is displayed the output of the command).

## MSMQ and MSDTC

To ensure MSMQ and MSDTC are installed, download and run the [Particular Platform Installer](https://particular.net/start-platform-download).  On the component selection screen select the "Configure MSDTC for NServiceBus" and "Configure Microsoft Message Queuing" options.  You can install the other Particular components if you want to but they are not required.

## RavenDB

1. Download the RavenDB server as a zip package from [https://ravendb.net/download](https://ravendb.net/download).
1. Extract contents the zip file to a folder on your machine.
1. Open the extracted folder and double click the `Start.cmd` file to start the RavenDB console.  This will also open the RavenDB Management Studio in your web browser at [http://localhost:8080](http://localhost:8080).

## Visual Studio
1. Open the `src/HotelManagementSystem.sln` solution file in Visual Studio
1. Right click on the solution and select **Restore NuGet Packages**
1. Configure solution's startup projects to start multiple projects
	1. In Solution Explorer right-click on **Solution HotelManagementSystem** and select **Properties**
	1. Under the **Common Properties** folder select **Startup Project**
	1. Select the **Multiple startup projects** option
	1. Change the **Action** for the following projects to **Start**:
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
	1. Click **OK**
1. Run the projects by pressing <kbd>Ctrl</kbd>+<kbd>F5</kbd>
