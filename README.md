# Lifestream - Blood Bank System

## About
Lifestream is a blood donation management system designed to streamline blood donation activities and facilitate efficient management of donor information, appointments, and requests. It provides a user-friendly interface for staff members to perform various tasks related to blood donations and ensures smooth coordination between donors, appointments, and blood requests.

## Screenshots
Here are some screenshots of the Lifestream - Blood Bank System:

### Login Screen
![Login Screen](https://github.com/MaizNadeem/Lifestream-WPF/blob/master/Frontend%20Screenshots/Login.png?raw=true)

### Main View
![Main View](https://github.com/MaizNadeem/Lifestream-WPF/blob/master/Frontend%20Screenshots/Dashboard.png?raw=true)

### Other Views
![Donor View](https://github.com/MaizNadeem/Lifestream-WPF/blob/master/Frontend%20Screenshots/View%20Donors.png?raw=true)
![Staff View](https://github.com/MaizNadeem/Lifestream-WPF/blob/master/Frontend%20Screenshots/View%20Staff.png?raw=true)
![Staff Info View](https://github.com/MaizNadeem/Lifestream-WPF/blob/master/Frontend%20Screenshots/Staff's%20Info.png?raw=true)

You can find all the application screenshots [here](https://github.com/MaizNadeem/Lifestream-WPF/tree/master/Frontend%20Screenshots).

## Entity Relationship Diagram (ERD)
![ERD](https://github.com/MaizNadeem/Lifestream-WPF/blob/master/Frontend%20Screenshots/ERD.png?raw=true)

The ERD illustrates the logical structure and relationships between the entities (tables) in the Lifestream - Blood Bank System's database.

## Technologies Used
The Lifestream - Blood Bank System is built using the following technologies:

- **.NET Framework 4.8:** The application is developed using the .NET Framework 4.8, which provides a robust and stable development platform for Windows applications.

- **WPF (Windows Presentation Foundation):** The user interface is built using WPF, a powerful framework for creating desktop applications with rich UI and interactive user experiences.

- **MSSQL Server / Azure Database:** The application uses either Microsoft SQL Server or Azure Database as the backend database to store and manage donor information, appointments, and requests.

- **NuGet Packages:**
  - **Lepoco:** Used for UI design and controls, providing a modern and visually appealing user interface.
  - **LiveCharts.Wpf:** Used for creating interactive and dynamic charts to visualize blood donation data.
  - **FontAwesome.Sharp:** Used to incorporate a wide range of icons and fonts into the application for enhanced visual elements.

## Deployment Instructions
To deploy the Lifestream - Blood Bank System on your own Windows machine, follow these steps:

1. Download the source code from the [GitHub repository](https://github.com/MaizNadeem/Lifestream-WPF).
2. Ensure that you have the following dependencies installed:
   - Visual Studio (2019 or later) with .NET Framework 4.8 development tools.
   - .NET Framework 4.8 runtime.
   - Azure Cloud account with Azure Data Studio (or SQL Server Management Studio - SSMS) installed.
3. Open the project in Visual Studio.
4. Update the connection string in the `RepositoryBase.cs` file located at `Lifestream-WPF/Repositories/RepositoryBase.cs`. Replace `"AZURE_CONNECTION_STRING"` with your own connection string.
5. Build the solution to restore NuGet packages and compile the project.
6. Publish the project using Visual Studio to create the executable files.
7. Copy the published files to the target machine.
8. Install .NET Framework 4.8 runtime on the target machine if it's not already installed.
9. Run the executable file to start the Lifestream - Blood Bank System.

## Database Setup
To set up the database for the Lifestream - Blood Bank System, follow these steps:

1. Download the [BloodBank.bacpac](https://github.com/MaizNadeem/Lifestream-WPF/blob/master/BloodBank.bacpac) file from the GitHub repository.
2. Open Azure Data Studio or SQL Server Management Studio (SSMS).
3. Connect to your Azure Database or local MSSQL Server.
4. Right-click on the Databases folder and choose "Import Data-tier Application."
5. Select the downloaded BloodBank.bacpac file and follow the import wizard to restore the database.
6. Once the database is restored, update the connection string in the `RepositoryBase.cs` file of the application to point to the newly restored database.

## Installation
To install the Lifestream - Blood Bank System on your Windows machine, follow these steps:

1. Download the installation files from the [Install.rar](https://drive.google.com/file/d/1FtdHXHE4VaEmcc1aHR-ecpwvatUnHzYx/view?usp=sharing) folder.
2. Extract the contents of the Install.rar file to a local directory.
3. Inside the extracted folder, locate the `setup.exe` file.
4. Double-click the `setup.exe` file to start the installation process.
5. Follow the on-screen instructions to complete the installation.
6. The Lifestream - Blood Bank System will be installed on your machine.
7. To uninstall the application, go to "Apps and Features" or "Add or Remove Programs" in the Control Panel and uninstall the application from there.

For any further assistance or questions, please refer to the project's [GitHub repository](https://github.com/MaizNadeem/Lifestream-WPF) or contact the project owner.

<hr>

<h4 align="center">© M. Maiz Nadeem</h4>