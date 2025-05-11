
#  Trust Market – ASP.NET Core Web Application for Products and Bidding

**Trust Market** is a web application developed with ASP.NET Core that enables secure product listings and bidding functionality. Users can register, log in, browse available products, and place bids in a simple and safe environment.

---

## 🚀 Getting Started

### 📋 Prerequisites
- [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Docker](https://www.docker.com/) (optional)

### 🔧 Installation and Setup

1. **Clone the repository**:
   ```bash
   git clone https://github.com/kristianche/AuthOnlineApp.git
   cd AuthOnlineApp
   ```

2. **Apply migrations and update the database**:
   ```bash
   dotnet ef database update
   ```

3. **Run the application**:
   ```bash
   dotnet run
   ```
   Then open your browser and navigate to:
   ```
   https://localhost:{port}
   ```

---

## 🐳 Running with Docker (Optional)

1. **Build the Docker image**:
   ```bash
   docker build -t authonlineapp .
   ```

2. **Run the Docker container**:
   ```bash
   docker run -d -p 5000:80 --name authonlineapp_container authonlineapp
   ```
   Access the app at:
   ```
   http://localhost:5000
   ```

---

## ✨ Features

- **User Registration and Login**  
  Secure authentication system using ASP.NET Core Identity.

- **Product Management**  
  Users can create, update, and delete product listings.

- **Bidding System**  
  Users can place bids on available products.

- **User-Friendly Interface**  
  Built with Razor Pages for intuitive navigation and interaction.

- **Docker Support**  
  Easily deploy the application using Docker containers.

---

## 🛠️ Technologies Used

- **Language**: C#  
- **Framework**: ASP.NET Core 6.0  
- **Frontend**: Razor Pages  
- **Database**: Entity Framework Core + SQL Server  
- **Authentication**: ASP.NET Core Identity  
- **Containerization**: Docker

---

## 📁 Project Structure

- `Areas/Identity/Pages` – Authentication and authorization pages  
- `Controllers/` – HTTP request handlers  
- `Data/` – Database context and seed data  
- `Models/` – Application data models  
- `Views/` – Razor view templates  
- `wwwroot/` – Static assets (CSS, JavaScript, images)  
- `Dockerfile` – Docker image instructions  
- `Program.cs` – Application entry point

---
