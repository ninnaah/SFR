# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY SFR.sln ./
COPY Shared/ ./Shared/
COPY Consumer/ ./Consumer/

RUN dotnet restore ./Consumer/Consumer.csproj
RUN dotnet publish ./Consumer/Consumer.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Consumer.dll"]