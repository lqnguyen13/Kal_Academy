FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY WebMvc/WebMvc.csproj WebMvc/
RUN dotnet restore WebMvc/WebMvc.csproj
COPY . .
WORKDIR /src/WebMvc
RUN dotnet build WebMvc.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebMvc.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebMvc.dll"]
