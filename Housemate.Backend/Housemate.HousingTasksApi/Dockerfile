FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Housemate.Backend/Housemate.HousingTasksApi/Housemate.HousingTasksApi.csproj", "Housemate.Backend/Housemate.HousingTasksApi/"]
COPY ["Housemate.Backend/Housemate.Contracts/Housemate.Contracts.csproj", "Housemate.Backend/Housemate.Contracts/"]
COPY ["Housemate.Backend/Housemate.Application/Housemate.Application.csproj", "Housemate.Backend/Housemate.Application/"]
RUN dotnet restore "Housemate.Backend/Housemate.HousingTasksApi/Housemate.HousingTasksApi.csproj"
COPY . .
WORKDIR "/src/Housemate.Backend/Housemate.HousingTasksApi"
RUN dotnet build "Housemate.HousingTasksApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Housemate.HousingTasksApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Housemate.HousingTasksApi.dll"]
