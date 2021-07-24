#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app
#EXPOSE 9000 #docker-compose
EXPOSE 80 
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["TECAIS.IOT.HeatMeteringUnit/TECAIS.IOT.HeatMeteringUnit.csproj", "TECAIS.IOT.HeatMeteringUnit/"]
RUN dotnet restore "TECAIS.IOT.HeatMeteringUnit/TECAIS.IOT.HeatMeteringUnit.csproj"
COPY . .
WORKDIR "/src/TECAIS.IOT.HeatMeteringUnit"
RUN dotnet build "TECAIS.IOT.HeatMeteringUnit.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TECAIS.IOT.HeatMeteringUnit.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TECAIS.IOT.HeatMeteringUnit.dll"]