# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./WhoseTurn/WhoseTurn.csproj" --disable-parallel
RUN dotnet publish "./WhoseTurn/WhoseTurn.csproj" -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 9000
EXPOSE 9001

ENTRYPOINT ["dotnet", "WhoseTurn.dll"]

docker run --rm -p 9000:9000 -p 9001:9001 -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_URLS="http://+:9000" whoseturn
