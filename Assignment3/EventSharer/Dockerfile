FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY EventSharer/EventSharer.csproj EventSharer/
RUN dotnet restore EventSharer/EventSharer.csproj
COPY . .
WORKDIR /src/EventSharer
RUN dotnet build EventSharer.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish EventSharer.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EventSharer.dll"]
