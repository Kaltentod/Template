FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ari-ib-calificaciones-api-web/ari-ib-calificaciones-api-web.csproj", "ari-ib-calificaciones-api-web/"]
RUN dotnet restore "ari-ib-calificaciones-api-web/ari-ib-calificaciones-api-web.csproj"
COPY . .
WORKDIR "/src/ari-ib-calificaciones-api-web"
RUN dotnet build "ari-ib-calificaciones-api-web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ari-ib-calificaciones-api-web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ari-ib-calificaciones-api-web.dll"]
