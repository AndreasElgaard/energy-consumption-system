#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["TECAIS.PublicChargingService/TECAIS.PublicChargingService.csproj", "TECAIS.PublicChargingService/"]
RUN dotnet restore "TECAIS.PublicChargingService/TECAIS.PublicChargingService.csproj"
COPY . .
WORKDIR "/src/TECAIS.PublicChargingService"
RUN dotnet build "TECAIS.PublicChargingService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TECAIS.PublicChargingService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TECAIS.PublicChargingService.dll"]