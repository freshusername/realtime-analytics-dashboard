# realtime-analytics-dashboard
Test Task for Realtime analytics dashboard including frontend written in React 18 as well as backend written in .NET 8

---
## Requirements
### 1. Backend
   - [x] Create a RESTful API using any backend framework (e.g., C# .NET).
   - [x] Implement endpoints to fetch:
     - [x] Active users (simulated data with random values)
     - [x] Total sales (simulated data with random values)
     - [x] Top-selling products (list of products with sales figures)
   - [x] Use WebSockets to push updates to the frontend in real-time.
   - [x] Include necessary unit tests for the API endpoints.
### 2. Frontend
   - [x] Create a single-page application (SPA) using a modern JavaScript framework/library (e.g., React).
   - [x] Display the following metrics on the dashboard:
     - [x] Active users (updated every 10 seconds)
     - [x] Total sales (updated every 10 seconds)
     - [x] Top-selling products (updated every 30 seconds)
   - [x] Implement real-time updates using WebSockets.
   - [x] Use a charting library (e.g., Chart.js, D3.js) to visualize the data.
   - [x] Include responsive design to ensure the dashboard is mobile-friendly.
### 3. Data Simulation
   - [x] Implement a simple data simulation mechanism in the backend to
   generate random data for the metrics.
   - [x] Ensure the data changes over time to simulate a real-world scenario.
### 4. Documentation
   - [x] Provide clear documentation on how to set up and run the project.
   - [x] Include a brief explanation of the architecture and design choices.

---
##  Setup Guide

### Prerequisites
1. **Node.js**: Ensure you have Node.js installed. You can download it from [Node.js](https://nodejs.org/).
2. **.NET 8 SDK**: Ensure you have the .NET SDK installed. You can download it from [.NET SDK](https://dotnet.microsoft.com/download).

### Folder Structure
realtime-analytics-dashboard
```
├── .git
├── .idea
├── src
│ ├── backend
│ │ ├── Dashboard.API
│ │ ├── Dashboard.Tests
│ │ ├── Dashboard.API.sln
│ ├── frontend
│ │ ├── dashboard-ui
│ │ │ ├── node_modules
│ │ │ ├── public
│ │ │ ├── src
│ │ │ ├── .gitignore
│ │ │ ├── package.json
│ │ │ ├── package-lock.json
```

## Running the Backend
1. **Navigate to the backend directory:**
    ```bash
    cd src/backend/Dashboard.API
    ```

2. **Restore dependencies:**
    ```bash
    dotnet restore
    ```

3. **Run the backend:**
    ```bash
    dotnet run
    ```

4. **Access the backend API:**
     - The backend API should now be running at `http://localhost:5000`.

## Running the Frontend
1. **Navigate to the frontend directory:**
    ```bash
    cd src/frontend/dashboard-ui
    ```

2. **Install dependencies:**
    ```bash
    npm install
    ```

3. **Run the frontend:**
    ```bash
    npm start
    ```

4. **Access the frontend:**
     - The frontend should now be running at `http://localhost:3000`.

---
## Further improvements:
### Backend:
- Add storage for historical data (e.g. SQL database(Postgres) or NoSQL database(MongoDB))
- Add authentication and authorization
- Add logging, monitoring, alerting, metrics and tracing
- Add caching