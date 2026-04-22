FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

ARG NUGET_TOKEN

RUN dotnet nuget remove source drkb-private || true
RUN dotnet nuget locals all --clear

# Добавляем приватный источник

RUN dotnet nuget add source https://baget.drkb-portal.ru/v3/index.json \
    --name drkb-private \
    --username drkb \
    --password $NUGET_TOKEN \
    --store-password-in-clear-text


COPY ["Drkb.UniversalBot/Drkb.UniversalBot.csproj", "Drkb.UniversalBot/"]
COPY ["Drkb.UniversalBot.Infrastructure/Drkb.UniversalBot.Infrastructure.csproj", "Drkb.UniversalBot.Infrastructure/"]
COPY ["Drkb.UniversalBot.Application/Drkb.UniversalBot.Application.csproj", "Drkb.UniversalBot.Application/"]
COPY ["Drkb.UniversalBot.Domain/Drkb.UniversalBot.Domain.csproj", "Drkb.UniversalBot.Domain/"]
COPY ["Drkb.UniversalBot.Contracts/Drkb.UniversalBot.Contracts.csproj", "Drkb.UniversalBot.Contracts/"]
COPY ["Drkb.UniversalBot.Integration/Drkb.UniversalBot.Integration.csproj", "Drkb.UniversalBot.Integration/"]
RUN dotnet restore "Drkb.UniversalBot/Drkb.UniversalBot.csproj"
COPY . .
WORKDIR "/src/Drkb.UniversalBot"
RUN dotnet build "Drkb.UniversalBot.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Drkb.UniversalBot.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Drkb.UniversalBot.dll"]
