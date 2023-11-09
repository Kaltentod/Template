FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BNA.IB.Calificaciones.API.Web/BNA.IB.Calificaciones.API.Web.csproj", "BNA.IB.Calificaciones.API.Web/"]
RUN dotnet restore "BNA.IB.Calificaciones.API.Web/BNA.IB.Calificaciones.API.Web.csproj"
COPY . .
WORKDIR "/src/BNA.IB.Calificaciones.API.Web"
RUN dotnet build "BNA.IB.Calificaciones.API.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BNA.IB.Calificaciones.API.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BNA.IB.Calificaciones.API.Web.dll"]
