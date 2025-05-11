# Trust Market 

**Trust Market** is a simple ASP.NET Core application that demonstrates user authentication, product listing, and bidding functionality. It uses Razor Pages and local in-memory storage for ease of setup and testing, making it ideal for learning and lightweight use cases.

---

## ✨ Features

- 🔐 **User Authentication**
  - Register, Login, and Logout
  - Secure session-based access

- 🛍️ **Product Listing**
  - View available products
  - Add/Edit products (admin only)

- 💸 **Bidding System**
  - Place bids on listed products
  - View current highest bids

- 🧑‍💼 **Role Management**
  - Basic roles: User and Admin
  - Role-based page access

---

## 🧰 Tech Stack

- **Framework**: ASP.NET Core (Razor Pages)
- **Language**: C#
- **Authentication**: ASP.NET Core Identity
- **Storage**: In-memory (no database required)
- **UI**: Bootstrap (optional, for styling)

---

## 🚀 Getting Started

### ✅ Prerequisites

- [.NET SDK 6+](https://dotnet.microsoft.com/download)

### 🔧 How to Run

1. **Clone the Repository**:

   ```bash
   git clone https://github.com/kristianche/AuthOnlineApp.git
2. **Open Package Manager Console**
3. **Apply Migrations**
   ```bash
   update-database
4. **Run the App**
   ```bash
   dotnet run
   
