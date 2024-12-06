# Базовый образ для выполнения .NET приложений
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

# Устанавливаем пользователя, под которым будет запускаться приложение
USER $APP_UID

# Указываем рабочую директорию внутри контейнера
WORKDIR /app

# Открываем порты 8080 и 8081 для внешнего доступа
EXPOSE 8080
EXPOSE 8081


# Образ для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Устанавливаем аргумент для конфигурации сборки
ARG BUILD_CONFIGURATION=Release

# Указываем рабочую директорию для сборки
WORKDIR /src

# Копируем файл проекта в контейнер
COPY ["DockerHwApi.Server/DockerHwApi.Server.csproj", "DockerHwApi.Server/"]

# Выполняем восстановление зависимостей для проекта
RUN dotnet restore "DockerHwApi.Server/DockerHwApi.Server.csproj"

# Копируем остальные файлы проекта в контейнер
COPY . .

# Переходим в каталог проекта
WORKDIR "/src/DockerHwApi.Server"

# Компилируем проект в указанной конфигурации и помещаем артефакты сборки в /app/build
RUN dotnet build "DockerHwApi.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build


# Этап публикации
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
# Публикуем проект
RUN dotnet publish "DockerHwApi.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


# Финальный этап: созданем контейнер для выполнения
FROM base AS final

# Устанавливаем рабочую директорию для приложения
WORKDIR /app

# Копируем опубликованные файлы из предыдущего этапа
COPY --from=publish /app/publish .

# Определяем точку входа для контейнера
ENTRYPOINT ["dotnet", "DockerHwApi.Server.dll"]
