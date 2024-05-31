FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MigrationConsoleApplication/MigrationConsoleApplication.csproj", "MigrationConsoleApplication/"]
RUN dotnet restore "MigrationConsoleApplication/MigrationConsoleApplication.csproj"
COPY . .
WORKDIR "/src/MigrationConsoleApplication"
RUN dotnet build "MigrationConsoleApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MigrationConsoleApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MigrationConsoleApplication.dll"]
