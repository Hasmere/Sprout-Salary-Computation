
<!-- ABOUT THE PROJECT -->
## About The Project

Sprout Solutions automates all the administrative tasks around HR and Payroll. It’s important to their clients that their applications run correctly and efficiently. 

<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these steps.

### Prerequisites

This is an example of how to list things you need to use the software and how to install them.
* Visual Studio 2019 with .net 5 installed
* SQL Server/ SQL Express 2016 or up
* NodeJS (latest version)


### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/Hasmere/Sprout-Salary-Computation.git
   ```
2. Restore the database in your local sql server using SproutExamDb.bak File
3. Build the dotnet project

### Login Form Credentials
- Username: sprout.test@gmail.com
- Password: P@$$word6

<!-- ROADMAP -->
## Roadmap
- Create a web app that computes the salary of two types of employees:
    - Regular Employee
    - Contractual Employee
- Create a new employee and save it to the Database using inputs:
    - Name
    - Birthdate
    - TIN
    - Employee Type
- Delete and edit the employee details and make this reflect in the Database
- To compute the pay once “Calculate” button is clicked
    - If it's regular, you should be able to input the number of absences in days.
    - If it's contractual, you should be able to input the number of worked days.
    - Absent and worked days can have decimal places.
    - There should be a calculate button or whatever that will show the computed salary.
    - The answer should be rounded to 2 decimal places.
    - The answer should always show 2 decimal places (ex. 10,000.00).
