# FlowStock - Inventory & Warehouse Management System

**A modern, lightweight and almost-free Inventory Management System built with .NET 10 for online shops and small-medium businesses.**

![.NET](https://img.shields.io/badge/.NET-10.0-blue) 
![License](https://img.shields.io/badge/License-MIT-green)

## 🎯 Vision & Goal
FlowStock is designed to be a **professional yet simple and affordable** inventory management solution especially for online shops (and later for gold/jewelry shops and other verticals).

The main goal is to provide powerful warehouse & stock features without expensive monthly subscriptions.

## ✨ Current Features
(اینجا لیست کن — حتی اگر هنوز کامل نیست)
- Product & Category Management
- Stock In / Stock Out tracking
- ... (هر چیزی که تا حالا زدی)

## 📋 Future Roadmap
- Multi-tenant support
- Gold/Jewelry specific features
- Reporting & Analytics
- Integration with popular online shop platforms
- User roles & permissions

## 🛠 Tech Stack
- **Backend**: .NET 10
- **Architecture**: Micro Services + Clean Architecture + CQRS + MediatR
- **Database**: PostgreSQL + Entity Framework Core + Redis
- **Container**: Docker + docker-compose
- **Validation**: FluentValidation
- **Others**: Serilog, Message Brocker, ...

## 📁 Project Structure
(اگر diagram داری، عکسش رو اینجا بگذار)

## 🚀 How to Run (Local Development)
```bash
# Clone the repo
git clone https://github.com/Ali7495/FlowStock.git

# Go to project
cd FlowStock

# Run with Docker (recommended)
docker-compose up -d

# Or run manually
dotnet restore
dotnet run --project src/Services/FlowStock.Usermanagement/Usermanagement.Api
