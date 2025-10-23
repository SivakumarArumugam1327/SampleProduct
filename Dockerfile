# Use the official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything and restore dependencies
COPY . .
RUN dotnet restore

# Build and publish
RUN dotnet publish -c Release -o /out

# Use runtime image for smaller final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# Set the entry point
ENTRYPOINT ["dotnet", "Myproducts.dll"]
