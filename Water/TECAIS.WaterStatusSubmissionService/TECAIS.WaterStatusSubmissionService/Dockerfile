#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["TECAIS.WaterStatusSubmissionService/TECAIS.WaterStatusSubmissionService.csproj", "TECAIS.WaterStatusSubmissionService/"]
RUN dotnet restore "TECAIS.WaterStatusSubmissionService/TECAIS.WaterStatusSubmissionService.csproj"
COPY . .
WORKDIR "/src/TECAIS.WaterStatusSubmissionService"
RUN dotnet build "TECAIS.WaterStatusSubmissionService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TECAIS.WaterStatusSubmissionService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TECAIS.WaterStatusSubmissionService.dll"]