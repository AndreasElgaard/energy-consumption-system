#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR C:\Users\Mark\source\repos\DISP_TECAIS\
COPY ["/TECAIS.HeatStatusSubmissionService/TECAIS.HeatStatusSubmissionService.csproj", "TECAIS.HeatStatusSubmissionService/"]
RUN dotnet restore "TECAIS.HeatStatusSubmissionService/TECAIS.HeatStatusSubmissionService.csproj"
COPY . .
WORKDIR "C:/Users/Mark/source/repos/DISP_TECAIS/Heat/TECAIS.HeatStatusSubmissionService"
RUN dotnet build "TECAIS.HeatStatusSubmissionService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TECAIS.HeatStatusSubmissionService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TECAIS.HeatStatusSubmissionService.dll"]