FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Housemate.Backend/Housemate.WeatherApi/Housemate.WeatherApi.csproj", "Housemate.Backend/Housemate.WeatherApi/"]
COPY ["Housemate.Backend/Housemate.Application/Housemate.Application.csproj", "Housemate.Backend/Housemate.Application/"]
COPY ["Housemate.Backend/Housemate.Contracts/Housemate.Contracts.csproj", "Housemate.Backend/Housemate.Contracts/"]
RUN dotnet restore "Housemate.Backend/Housemate.WeatherApi/Housemate.WeatherApi.csproj"
COPY . .
WORKDIR "/src/Housemate.Backend/Housemate.WeatherApi"
RUN dotnet build "Housemate.WeatherApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Housemate.WeatherApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Housemate.WeatherApi.dll"]
