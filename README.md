🚖 SmartCab Dispatch Simulator (Phase 1)
📌 Objective
Develop a backend-only simulation of a cab dispatching system in a grid-based city using C# and .NET Core. This project focuses on clean object-oriented design, adherence to SOLID principles, and robust unit testing.


✅ Phase 1: Features Implemented

📍 Grid City Layout
City is modeled as an NxN grid

🚖 Cab Entity
Each Cab has:
1. Unique ID
2. Current location
3. Availability status
4. Total earnings

🧠 Core Features
1. 🚗 Ride Request
A user provides pickup and drop-off coordinates.
Dispatcher assigns the nearest available cab using Manhattan distance.
Cab movement is instantaneous in this phase.

2. 📊 Check Cab Status
Displays a list of all cabs.
User can select any cab to view its current status (Available/Booked).

3. 📁 Cab Ride History
For a given cab, displays pickup/drop locations, individual fare, and ride summary.

4. ❌ Exit
Gracefully shuts down the simulator.
   
Fare Calculation:
Base fare: ₹50
Additional ₹10 × Manhattan Distance from pickup to drop-off

Unit tests cover:
1. Fare calculation
2. Cab assignment
3. Cab state updates

📁 Project Structure
SmartCab/
├── SmartCab.Core/       # Core models, interfaces, and business logic
├── SmartCab.Console/    # Console-based simulation interface
└── SmartCab.Tests/      # MSTest + NSubstitute + FluentAssertions

🧪 Running Instructions
1. Clone and Navigate 
   git clone https://github.com/<your-username>/SmartCab.git
   cd SmartCab
   
2. Run the Console App
  cd SmartCab.Console
  dotnet run

3. Run Unit Tests
  cd SmartCab.Tests
  dotnet test

Sample Output
Ride requested from (2, 3) to (5, 6)
CAB1 assigned (current location: (0,0))
Fare: ₹110
CAB1 earnings updated. Total: ₹110
