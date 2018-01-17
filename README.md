A service oriented distributed system built using NServiceBus.  The application is a hotel management system that allows guests to find and book a room at a hotel.  Designing a system like this was a homework assignment from Udi Dahan's [Advanced Distributed Systems Design Course](https://particular.net/adsd "Advanced Distributed Systems Design Course").

# Getting Started

Before you start you will need [Visual Studio 2015](https://www.visualstudio.com/downloads/ "Visual Studio 2015"), [NodeJS](https://nodejs.org "NodeJS") 6.0 or higher, [NPM](https://www.npmjs.com/ "NPM") 3 or higher, RavenDB 3.5 or higher and MSMQ.  Specific setup steps for these are detailed below.

## MSMQ and MSDTC

To ensure MSMQ and MSDTC are installed, download and run the [Particular Platform Installer](https://particular.net/start-platform-download).  On the component selection screen select the "Configure MSDTC for NServiceBus" and "Configure Microsoft Message Queuing" options.  You can install the other Particular components if you want to but they are not required.

## RavenDB

1. Download RavenDB from [https://ravendb.net/download](https://ravendb.net/download) and extract contents the zip file.
1. Open the extracted folder and double click the `Start.cmd` file to start the RavenDB console.  This will also open the RavenDB Management Studio in your web browser at [http://localhost:8080](http://localhost:8080).

## Visual Studio
1. Open the `src/HotelManagementSystem.sln` solution file in Visual Studio
1. Right click on the solution and select **Restore NuGet Packages**
1. Configure solution's startup projects to start multiple projects
	1. In Solution Explorer right-click on **Solution HotelManagementSystem** and select **Properties**
	1. Under the **Common Properties** folder select **Startup Project**
	1. Select the **Multiple startup projects** option
	1. Change the **Action** for the following projects to **Start**:
		- Availability
		- Availability.Api
		- Finance
		- Finance.Api
		- Guests
		- Guests.Api
		- Marketing.Api
		- Rates.Api
		- Reservations
		- Reservations.Api
	1. Click **OK**
1. Run the projects by pressing `CTRL+F5`

## Aurelia SPA
The client side application is a Single Page Application build using the [Aurelia](http://aurelia.io/) Javascript client framework.

#### Install the required JavaScript packages

1. Open a Windows command prompt and got to the `src/PublicWebsite` directory
1. Execute the following command: `npm install` or: `yarn install`


#### Run the application

1. Execute the following command: `npm start` or: `yarn start`

You can now browse to the hotel management web application at [http://localhost:9000](http://localhost:9000 "http://localhost:9000") (or the next available port which is displayed the output of the command).
