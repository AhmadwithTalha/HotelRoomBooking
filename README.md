                      Hotel Room Booking API
This project is a Hotel Room Booking System built with .NET 8 Web API and SQL Server.
It provides functionality to manage Rooms and Reservations with proper validations and persistence in SQL (no in-memory storage).
The backend is built in ASP.NET Core Web API with Entity Framework Core, and the frontend is a simple Angular app for listing rooms and creating reservations.
Features
- Manage hotel rooms (CRUD)
- Manage reservations with availability check
- Prevents overlapping bookings
- Business rules:
- `CheckOutDate` must always be **after** `CheckInDate`
- A room cannot be deleted if it already has reservations
- No two reservations for the same room can overlap

Technologies Used
Backend (API)
Frameworks & Tools
•	.NET 8 (ASP.NET Core Web API) → Main framework for building the API.
•	Entity Framework Core (EF Core) → ORM for database access and migrations.
•	SQL Server → Relational database used for storing rooms and reservations.
•	Swagger / Swashbuckle → Auto-generated API documentation & testing UI.
NuGet Packages (commonly used in this type of project):
•	Microsoft.EntityFrameworkCore
•	Microsoft.EntityFrameworkCore.SqlServer
•	Microsoft.EntityFrameworkCore.Tools
•	Swashbuckle.AspNetCore
Other Requirements
•	.NET SDK 8.0+ installed
•	SQL Server installed (local or remote)
•	Visual Studio or VS Code (for development)
________________________________________
Frontend (Angular)
Frameworks & Tools
•	Angular 16+ → Frontend framework.
•	TypeScript → Language for Angular.
•	RxJS → Reactive programming for handling API calls.
•	Bootstrap (optional, if you added styles).
NPM Packages (minimum required):
•	@angular/core
•	@angular/cli
•	@angular/forms
•	@angular/common/http → for API communication
•	rxjs
Other Requirements
•	Node.js (LTS 18 or 20) installed
•	npm (comes with Node.js)
•	Angular CLI installed globally:
Setup Instructions
When a new developer clones the repository, they will need:
Backend
1.	Install .NET 8 SDK
2.	Install SQL Server
3.	Navigate to /backend
4.	Run:
5.	dotnet restore
6.	dotnet ef database update
7.	dotnet run
Frontend
1.	Install Node.js (LTS version recommended)
2.	Navigate to /frontend
3.	Install dependencies:
4.	npm install
5.	Run the Angular app:
6.	ng serve
Hotel Booking API Documentation
Base URL: https://localhost:7251

Rooms API

1. Get All Rooms
Endpoint: GET /api/rooms
Description: Get a list of all available rooms.
Input: No input required.
Example Request (requests.http):
GET https://localhost:7251/api/rooms
Example Response (JSON):
[
{ "id": 22, "roomNumber": "101#200", "type": "Single", "pricePerNight": 5000 },
{ "id": 28, "roomNumber": "105#200", "type": "Double", "pricePerNight": 7000 }
]
Alerts:
•	Shows all rooms.
•	If no rooms exist → returns an empty list.

2. Create Room
Endpoint: POST /api/rooms
Description: Add a new room (Room number must be unique).
Input Fields (JSON body):
{
"roomNumber": "108#200",
"type": "Suite",
"pricePerNight": 12000
}
Example Request (requests.http):
POST https://localhost:7251/api/rooms
Content-Type: application/json

{
"roomNumber": "108#200",
"type": "Suite",
"pricePerNight": 12000
}
Example Response (JSON):
{ "id": 32, "roomNumber": "108#200", "type": "Suite", "pricePerNight": 12000 }
Alerts:
•	Room created successfully.
•	If the room number already exists → "Room number must be unique".

3. Update Room
Endpoint: PUT /api/rooms/{id}
Description: Update room details (cannot change id).
Input Fields (JSON body):
{
"roomNumber": "108#201",
"type": "Suite",
"pricePerNight": 13000
}
Example Request (requests.http):
PUT https://localhost:7251/api/rooms/32
Content-Type: application/json

{
"roomNumber": "108#201",
"type": "Suite",
"pricePerNight": 13000
}
Example Response (JSON):
{ "id": 32, "roomNumber": "108#201", "type": "Suite", "pricePerNight": 13000 }
Alerts:
•	Room updated successfully.
•	If id not found → "Room not found".

4. Delete Room
Endpoint: DELETE /api/rooms/{id}
Description: Delete a room (only if not reserved).
Example Request (requests.http):
DELETE https://localhost:7251/api/rooms/32
Example Response (JSON):
{ "message": "Room deleted successfully." }
Alerts:
•	Room deleted.
•	If the room is reserved → "Room cannot be deleted because it is reserved".

Reservations API

1. Check Availability
Endpoint: GET /api/reservations/check?roomId={roomId}&checkIn={date}&checkOut={date}
Description: Check if a room is free between given dates.
Example Request (requests.http):
GET https://localhost:7251/api/reservations/check?roomId=29&checkIn=2025-09-20&checkOut=2025-09-25
Example Response (Available):
{ "available": true, "message": "Room is available for booking." }
Example Response (Not Available):
{
"available": false,
"reservedBy": "Hamza",
"checkIn": "2025-09-21",
"checkOut": "2025-09-24"
}
Alerts:
•	Room available.
•	Room already booked by someone.
2. Get All Reservations
Endpoint: GET /api/reservations
Description: Get list of all reservations.
Example Request:
GET https://localhost:7251/api/reservations
Example Response:
[
{ "id": 35, "roomId": 29, "guestName": "Hamza", "checkIn": "2025-09-20", "checkOut": "2025-09-24" },
{ "id": 39, "roomId": 22, "guestName": "Hadi", "checkIn": "2025-09-22", "checkOut": "2025-09-25" }
]
Alerts:
•	All reservations shown.

3. Create Reservation (Booking)
Endpoint: POST /api/reservations
Description: Book a room (check availability automatically).
Input Fields (JSON body):
{
"roomId": 29,
"guestName": "Ahmad",
"checkIn": "2025-09-26",
"checkOut": "2025-09-29"
}
Example Request:
POST https://localhost:7251/api/reservations
Content-Type: application/json

{
"roomId": 29,
"guestName": "Ahmad",
"checkIn": "2025-09-26",
"checkOut": "2025-09-29"
}
Example Response:
{ "id": 42, "roomId": 29, "guestName": "Ahmad", "checkIn": "2025-09-26", "checkOut": "2025-09-29" }
Alerts:
•	Reservation created.
•	CheckOutDate must be after CheckInDate.
•	Room already reserved → booking rejected.
4. Update Reservation (Extend Stay)
Endpoint: PUT /api/reservations/{id}
Description: Extend the checkout date of a reservation.
Input Fields (JSON body):
{
"checkOut": "2025-10-02"
}
Example Request:
PUT https://localhost:7251/api/reservations/42
Content-Type: application/json

{
"checkOut": "2025-10-02"
}
Example Response:
{ "id": 42, "roomId": 29, "guestName": "Ahmad", "checkIn": "2025-09-26", "checkOut": "2025-10-02" }
Alerts:
•	Reservation updated.
•	Cannot extend if another booking overlaps.

5. Delete Reservation
Endpoint: DELETE /api/reservations/{id}
Description: Cancel a reservation.
Example Request:
DELETE https://localhost:7251/api/reservations/42
Example Response:
{ "message": "Reservation deleted successfully." }
Alerts:
•	Reservation deleted.
•	If reservation not found → "Reservation does not exist".

