FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY EventCatalogAPI/EventCatalogAPI.csproj EventCatalogAPI/
RUN dotnet restore EventCatalogAPI/EventCatalogAPI.csproj
COPY . .
WORKDIR /src/EventCatalogAPI
RUN dotnet build EventCatalogAPI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish EventCatalogAPI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EventCatalogAPI.dll"]
