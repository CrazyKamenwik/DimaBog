FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SkyDrive/SkyDrive.csproj", "SkyDrive/"]
COPY ["SkyDrive.BLL/SkyDrive.BLL.csproj", "SkyDrive.BLL/"]
COPY ["SkyDrive.DAL/SkyDrive.DAL.csproj", "SkyDrive.DAL/"]
RUN dotnet restore "SkyDrive/SkyDrive.csproj"
COPY . .
WORKDIR "/src/SkyDrive"
RUN dotnet build "SkyDrive.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SkyDrive.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkyDrive.dll"]