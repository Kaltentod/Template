# Apoyo y Regímenes Informativos - IB.Calificaciones API 

## Comenzando 🚀

_Estas instrucciones permiten obtener una copia del proyecto en funcionamiento en tu máquina local para propósitos de desarrollo y pruebas._

### Pre-requisitos 📋

* [.NET 6+](https://dotnet.microsoft.com/)

### Creación desde cero 🔧

_Una serie de ejemplos paso a paso que debes ejecutar para tener un entorno de desarrollo ejecutandose_

1. Configuración

_BNA.IB.Calificaciones.API.Web/appsettings.Development.json_

2. Build

   ```bash
   dotnet build
   ```

### Base de datos 🔧

_Instrucciones para conectarse a la base de datos, crear y ejecutar migraciones de EF_

1. Iniciar SQL Server mediante Docker (opcional)

   ```bash
   docker run --cap-add SYS_PTRACE -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=bnapassword' -p 1433:1433 --name azuresqledge -d mcr.microsoft.com/azure-sql-edge
   ```

2. Agregar nueva migración (desde BNA.IB.Calificaciones.API.Infrastructure.SQLServer)

   ```bash
   dotnet ef migrations add InitialCreate -s ../BNA.IB.Calificaciones.API.Web
   ```

3. Ejecutar migración en base de datos (desde BNA.IB.Calificaciones.API.Infrastructure.SQLServer)

   ```bash
   dotnet ef database update -s ../BNA.IB.Calificaciones.API.Web/ --configuration Development
   ```

## Construido con 🛠️

_herramientas utilizadas para crear el backend_

- [.NET 6+](https://dotnet.microsoft.com/)

\+ other smaller dependencies

## Versionado 📌

Usamos [SemVer](http://semver.org/) para el versionado. Para todas las versiones disponibles, mira los [tags en este repositorio](https://github.com/Banco-Nacion/ari-ib-calificaciones-api/tags).

## Documentación
- 