#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TECAIS.AccountCommandHandler/TECAIS.AccountCommandHandler.csproj", "TECAIS.AccountCommandHandler/"]
RUN dotnet restore "TECAIS.AccountCommandHandler/TECAIS.AccountCommandHandler.csproj"
COPY . .
WORKDIR "/src/TECAIS.AccountCommandHandler"
RUN dotnet build "TECAIS.AccountCommandHandler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TECAIS.AccountCommandHandler.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TECAIS.AccountCommandHandler.dll"]