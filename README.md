# SMS-Sending-Service
An SMS Sending service that has a rest api controller that we can sent an http request to send the message. The service was created as a demonstration of skills in dependency injection, Entity Framework, AutoMapper, design patterns and xUnit testing tool for the .NET Framework.

# Getting Started
 
To get started with the SMS Sending Service, you'll need to clone this repository in Visual Studio. You'll also need to set up the local database to persist SMS messages. According to the instructions below.
# Prerequisites
 
Before you can run the SMS Sending Service, you'll need to have the following installed on your machine:
.NET 5 SDK
Visual Studio
Postman

 
# To install the SMS Sending Service:
Clone this repository to your local machine.
Go to the Package Manager Console and type the following two instructions:
   Add-Migration InitialCreate
   Update-Database
This will create the database.

Run the project by pressing F5 or clicking the "Play" button in Visual Studio and use Postman in order to make some CRUD operations.
# Usage
 
To use the SMS Sending Service, you can send a POST request to the /api/sms endpoint with a JSON payload containing the Number and Message properties of the SMS message you want to send. The service will automatically choose a vendor to send the message through based on availability and cost.
##  Example Request
 

POST /api/sms  
Content-Type: application/json  
  
{  
  "Number": "+306989999999",  
  "Message":"Γειά σου κόσμε!",  
  "SenderNumber": "+306989999999",  
  "SentRate": "2022-08-31T08:00:00Z",  
  "Status": "sent"  
}  

## Example Response
 

HTTP/1.1 200 OK  
Content-Type: application/json  
  
{
    "id": 0,
    "number": "+447777777777",
    "message": "Hello World  ",
    "status": "sent"
}
 

# License
 
This project is licensed under the MIT License - see the LICENSE file for details.


