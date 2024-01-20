# Use the official .NET SDK image as a base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Define a build-time environment variable
ARG ASPNETCORE_ENVIRONMENT=Development

# Set the working directory
WORKDIR /src

# Copy and restore dependencies
COPY src/LineTen.TechnicalTask.Data/*.csproj ./LineTen.TechnicalTask.Data/
COPY src/LineTen.TechnicalTask.Domain/*.csproj ./LineTen.TechnicalTask.Domain/
COPY src/LineTen.TechnicalTask.Service/*.csproj ./LineTen.TechnicalTask.Service/
COPY src/LineTen.TechnicalTask.Service.Domain/*.csproj ./LineTen.TechnicalTask.Service/
COPY build/AnalysisPackages.targets ./build/

# Restore dependencies for only the necessary projects
RUN dotnet restore LineTen.TechnicalTask.Service/LineTen.TechnicalTask.Service.csproj

# Copy the remaining source code
COPY . .

# Build the application
RUN dotnet build src/LineTen.TechnicalTask.Service/LineTen.TechnicalTask.Service.csproj -c Release -o /app

# Publish the application
RUN dotnet publish src/LineTen.TechnicalTask.Service/LineTen.TechnicalTask.Service.csproj -c Release -o /app

# Use a smaller runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory to the published output
WORKDIR /app

# Copy the published files from the build image
COPY --from=build /app .

# Set the entry point to the application
ENTRYPOINT ["dotnet", "LineTen.TechnicalTask.Service.dll"]