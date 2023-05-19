FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Housemate.Backend/Housemate.WastesApi/Housemate.WastesApi.csproj", "Housemate.Backend/Housemate.WastesApi/"]
RUN dotnet restore "Housemate.Backend/Housemate.WastesApi/Housemate.WastesApi.csproj"
COPY . .
WORKDIR "/src/Housemate.Backend/Housemate.WastesApi"
RUN dotnet build "Housemate.WastesApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Housemate.WastesApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Housemate.WastesApi.dll"]
