#!/bin/sh
echo "Applying EF Core Migrations..."
dotnet ef database update
echo "Starting the app..."
dotnet MyPortfolio.dll
