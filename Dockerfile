# Базовый образ для .NET
FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

# Копируем все файлы из вашего репозитория в контейнер
COPY . .

# Используем образ SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Копируем проект и восстанавливаем зависимости
COPY ["DiscordBot2.0/DiscordBot2.0.csproj", "DiscordBot2.0/"]
RUN dotnet restore "DiscordBot2.0/DiscordBot2.0.csproj"

# Публикуем приложение
COPY . .
RUN dotnet publish "DiscordBot2.0/DiscordBot2.0.csproj" -c Release -o /app

# Финальный образ для выполнения
FROM base AS final
WORKDIR /app
RUN rm -rf /app # Очистка старых файлов в /app
COPY --from=build /app .
COPY DiscordBot2.0/config.json /app/config.json
ENTRYPOINT ["dotnet", "DiscordBot2.0.dll"]
