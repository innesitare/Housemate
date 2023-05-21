FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Housemate.WastesApi/Housemate.WastesApi.csproj", "Housemate.WastesApi/"]
COPY ["Housemate.Application/Housemate.Application.csproj", "Housemate.Application/"]
COPY ["Housemate.Contracts/Housemate.Contracts.csproj", "Housemate.Contracts/"]
RUN dotnet restore "Housemate.WastesApi/Housemate.WastesApi.csproj"
COPY . .
WORKDIR "/src/Housemate.WastesApi"
RUN dotnet build "Housemate.WastesApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Housemate.WastesApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Housemate.WastesApi.dll"]