FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Housemate.IdentityApi/Housemate.IdentityApi.csproj", "Housemate.IdentityApi/"]
COPY ["Housemate.Application/Housemate.Application.csproj", "Housemate.Application/"]
COPY ["Housemate.Contracts/Housemate.Contracts.csproj", "Housemate.Contracts/"]
RUN dotnet restore "Housemate.IdentityApi/Housemate.IdentityApi.csproj"
COPY . .
WORKDIR "/src/Housemate.IdentityApi"
RUN dotnet build "Housemate.IdentityApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Housemate.IdentityApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Housemate.IdentityApi.dll"]
