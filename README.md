This is an example of a distributed system built using design practices from
Udi Dahan's [Advanced Distributed Systems Design Course](https://particular.net/adsd "Advanced Distributed Systems Design Course").  This application uses a microservices-oriented architecture to learn how to build autonomous services using asynchronous message-based communication, composite UI/micro-front ends, and how to define service boundaries around business capabilities.

The problem domain is that of a boutique hotel with the main operations of a hotel such as booking, front desk operations and check-out.  The use cases of the domain are detailed in the *Use Cases* section below.  This domain was chosen because it was a homework assignment in the ADSD course and most people are familar with the general process.

## Setting up your development environment

To run everything in this project you will need to have the following:

- [NodeJS](https://nodejs.org "NodeJS") 6.0 or higher
- [NPM](https://www.npmjs.com/ "NPM") 3 or higher
- [RavenDB](https://ravendb.net/download) 3.5 or higher
- [Visual Studio 2015](https://www.visualstudio.com/downloads/ "Visual Studio 2015")
- Microsoft MSMQ

The specific setup steps for each of these are detailed below.

### Node and NPM
Install both [NodeJS](https://nodejs.org "NodeJS") and [NPM](https://www.npmjs.com/ "NPM") using the standard instructions provided on their sites. 

### Client Side Packages
The client side application is a Single Page Application (SPA) built using the [Aurelia](http://aurelia.io/) Javascript client framework and uses NPM to manage all the packages.

1. Open a Windows command prompt and go to the `src/PublicWebsite` directory
1. Execute the following command: `npm install` or: `yarn install`

### MSMQ and MSDTC
To ensure MSMQ and MSDTC are installed.

1. Download and run the [Particular Platform Installer](https://particular.net/start-platform-download).
1. On the component selection screen select the "Configure MSDTC for NServiceBus" and "Configure Microsoft Message Queuing" options.
  - You can install the other Particular components if you want to try them out but they are not required.

### RavenDB

1. Download the RavenDB server as a zip package from [https://ravendb.net/download](https://ravendb.net/download).
1. Extract contents the zip file to a folder on your machine.

### Visual Studio

1. Open the `src/HotelManagementSystem.sln` solution file in Visual Studio
2. Right click on the solution and select **Restore NuGet Packages**
3. Configure solution's startup projects to start multiple projects
    1. In Solution Explorer right-click on **Solution HotelManagementSystem** and select **Properties**
    2. Under the **Common Properties** folder select **Startup Project**
    3. Select the **Multiple startup projects** option
    4. Change the **Action** for the following projects to **Start**:
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
    5. Click **OK**
4. Make sure everything compiles by pressing <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>B</kbd>
 
## Running the System

#### 1. Start the RavenDB Server

1. In the folder where you extracted the RavenDB zip double click the `Start.cmd` to start the RavenDB console.

You should now have a RavenDB console applicaiton running and the RavenDB Management Studio open in your web browser at [http://localhost:8080](http://localhost:8080).

#### 2. Start the NServiceBus Hosts and Web APIs

1. From Visual Studio run all the projects by pressing <kbd>Ctrl</kbd>+<kbd>F5</kbd>

You should now have several NServiceBus process hosts running as console applications and a web page for each Web API.  You can close the web pages for the APIs if you like.

#### 3. Start the web site SPA

1. Open a Windows command prompt and go to the `src/PublicWebsite` directory
1. Execute the following command: `npm start` or: `yarn start`
1. In a web browser go to the Hotel Management System home page at [http://localhost:9000](http://localhost:9000 "http://localhost:9000") (or the next available port which is displayed the output of the command).

## Use Cases
This system simulates the process someone would go though to book a reservation to stay at a hotel.  The details of each function is detailed below.

#### Find a Room
A guest can enter the dates they want to stay, see a list of room types that are available and nightly rates.

#### Make a Reservation
After a guest has selected a room type they must enter their personal information and specify a credit card for payment.

#### Guest Check in
Coming soon:  A hotel reception desk employee can find a guest's reservation, assign them to a physical room in the hotel, and place a a new hold on the guest's credit card for the total amount of the stay.

#### Guest Checkout
Coming soon:  A check-out process will generate a guest's final invoice for their stay and charge the guest's credit card.

## Services
In the context of this system a *Service* is defined as the technical authority for a business capability and all business rules and data reside in the service.  In this code base a service will typically consist of a UI component, an ASP.NET Web API, a NServiceBus host, and a database.  For more information on service boundaries watch [Finding Service Boundaries – illustrated in healthcare by Udi Dahan](https://vimeo.com/album/3715841/video/113515335)

![Typical Message-Based Architecture](/imgs/typical-message-based-architecture.png)

### Business Services
The system consists of the following six services:
1. Availability
2. Rates
3. Guests
4. Finance
5. Marketing
6. Reservation

### Technical Services
There are two special services that handle infrastructure and the web site's look and feel: 
1. ITOps
2. Branding
