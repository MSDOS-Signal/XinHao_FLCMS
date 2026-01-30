FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ERPWMS.Job.Service/ERPWMS.Job.Service.csproj", "ERPWMS.Job.Service/"]
COPY ["ERPWMS.Application/ERPWMS.Application.csproj", "ERPWMS.Application/"]
COPY ["ERPWMS.Domain/ERPWMS.Domain.csproj", "ERPWMS.Domain/"]
COPY ["ERPWMS.Shared/ERPWMS.Shared.csproj", "ERPWMS.Shared/"]
COPY ["ERPWMS.Infrastructure/ERPWMS.Infrastructure.csproj", "ERPWMS.Infrastructure/"]
COPY ["ERPWMS.Barcode.RFID/ERPWMS.Barcode.RFID.csproj", "ERPWMS.Barcode.RFID/"]
COPY ["ERPWMS.APS.EAM.MOM/ERPWMS.APS.EAM.MOM.csproj", "ERPWMS.APS.EAM.MOM/"]
RUN dotnet restore "./ERPWMS.Job.Service/ERPWMS.Job.Service.csproj"
COPY . .
WORKDIR "/src/ERPWMS.Job.Service"
RUN dotnet build "./ERPWMS.Job.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ERPWMS.Job.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ERPWMS.Job.Service.dll"]
