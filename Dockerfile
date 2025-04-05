# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY SFR.sln ./
COPY SFR/ ./SFR/
RUN dotnet restore ./SFR/SFR.csproj
RUN dotnet publish ./SFR/SFR.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Arguments: "producer" or "consumer"
ENTRYPOINT ["dotnet", "SFR.dll"]