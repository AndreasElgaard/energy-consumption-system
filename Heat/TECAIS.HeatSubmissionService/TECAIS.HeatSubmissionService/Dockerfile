#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TECAIS.HeatSubmissionService/TECAIS.HeatSubmissionService.csproj", "TECAIS.HeatSubmissionService/"]
RUN dotnet restore "TECAIS.HeatSubmissionService/TECAIS.HeatSubmissionService.csproj"
COPY . .
WORKDIR "/src/TECAIS.HeatSubmissionService"
RUN dotnet build "TECAIS.HeatSubmissionService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TECAIS.HeatSubmissionService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TECAIS.HeatSubmissionService.dll"]