FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Housemate.Backend/Housemate.IdentityApi/Housemate.IdentityApi.csproj", "Housemate.Backend/Housemate.IdentityApi/"]
RUN dotnet restore "Housemate.Backend/Housemate.IdentityApi/Housemate.IdentityApi.csproj"
COPY . .
WORKDIR "/src/Housemate.Backend/Housemate.IdentityApi"
RUN dotnet build "Housemate.IdentityApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Housemate.IdentityApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Housemate.IdentityApi.dll"]
