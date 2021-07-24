FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TECAIS.Pricing.HeatPricingService/TECAIS.Pricing.HeatPricingService.csproj", "TECAIS.Pricing.HeatPricingService/"]

RUN dotnet restore "TECAIS.Pricing.HeatPricingService/TECAIS.Pricing.HeatPricingService.csproj"
COPY . .
WORKDIR "/src/TECAIS.Pricing.HeatPricingService"
RUN dotnet build "TECAIS.Pricing.HeatPricingService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TECAIS.Pricing.HeatPricingService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TECAIS.Pricing.HeatPricingService.dll"]