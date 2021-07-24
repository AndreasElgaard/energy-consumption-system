#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TECAIS.HeatPricingCommandHandler/TECAIS.HeatPricingCommandHandler.csproj", "TECAIS.HeatPricingCommandHandler/"]
RUN dotnet restore "TECAIS.HeatPricingCommandHandler/TECAIS.HeatPricingCommandHandler.csproj"
COPY . .
WORKDIR "/src/TECAIS.HeatPricingCommandHandler"
RUN dotnet build "TECAIS.HeatPricingCommandHandler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TECAIS.HeatPricingCommandHandler.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TECAIS.HeatPricingCommandHandler.dll"]