# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY SFR.sln ./
COPY Shared/ ./Shared/
COPY Producer/ ./Producer/

RUN dotnet restore ./Producer/Producer.csproj
RUN dotnet publish ./Producer/Producer.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Producer.dll"]