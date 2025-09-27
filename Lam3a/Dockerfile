FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Lam3a/Lam3a.csproj", "Lam3a/"]
RUN dotnet restore "Lam3a/Lam3a.csproj"
COPY . .
WORKDIR "/src/Lam3a"
RUN dotnet build "./Lam3a.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Lam3a.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
# UseAppHost=false: so we get a DLL file on both MacOS and Windows instead of a .exe on Windows

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lam3a.dll"]
