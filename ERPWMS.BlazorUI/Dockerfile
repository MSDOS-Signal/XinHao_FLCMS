FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ERPWMS.BlazorUI/ERPWMS.BlazorUI.csproj", "ERPWMS.BlazorUI/"]
COPY ["ERPWMS.Application/ERPWMS.Application.csproj", "ERPWMS.Application/"]
COPY ["ERPWMS.Domain/ERPWMS.Domain.csproj", "ERPWMS.Domain/"]
COPY ["ERPWMS.Shared/ERPWMS.Shared.csproj", "ERPWMS.Shared/"]
COPY ["ERPWMS.Infrastructure/ERPWMS.Infrastructure.csproj", "ERPWMS.Infrastructure/"]
COPY ["ERPWMS.Barcode.RFID/ERPWMS.Barcode.RFID.csproj", "ERPWMS.Barcode.RFID/"]
COPY ["ERPWMS.APS.EAM.MOM/ERPWMS.APS.EAM.MOM.csproj", "ERPWMS.APS.EAM.MOM/"]
RUN dotnet restore "./ERPWMS.BlazorUI/ERPWMS.BlazorUI.csproj"
COPY . .
WORKDIR "/src/ERPWMS.BlazorUI"
RUN dotnet build "./ERPWMS.BlazorUI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ERPWMS.BlazorUI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ERPWMS.BlazorUI.dll"]
