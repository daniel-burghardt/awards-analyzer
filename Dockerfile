# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY *.sln .
COPY MovieAnalyzer/*.csproj ./MovieAnalyzer/
COPY MovieAnalyzer.Tests/*.csproj ./MovieAnalyzer.Tests/
RUN dotnet restore

COPY . .
RUN dotnet build

# Run unit tests
FROM build AS tests
WORKDIR /app/MovieAnalyzer.Tests/
RUN dotnet test

# Copy everything else and build
FROM build AS publish
WORKDIR /app/MovieAnalyzer
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=publish /app/MovieAnalyzer/out .
ENTRYPOINT ["dotnet", "MovieAnalyzer.dll"]