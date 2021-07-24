#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["TECAIS.ElectricitySaga/TECAIS.ElectricitySaga.csproj", "TECAIS.ElectricitySaga/"]
RUN dotnet restore "TECAIS.ElectricitySaga/TECAIS.ElectricitySaga.csproj"
COPY . .
WORKDIR "/src/TECAIS.ElectricitySaga"
RUN dotnet build "TECAIS.ElectricitySaga.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TECAIS.ElectricitySaga.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TECAIS.ElectricitySaga.dll"]