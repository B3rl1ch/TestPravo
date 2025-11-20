# Базовый рантайм
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Слушаем на 8080 внутри контейнера
ENV ASPNETCORE_URLS=http://+:8080

# Сборка
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY TestTaskPravo.API/TestTaskPravo.API.csproj ./TestTaskPravo.API/
COPY TestTaskPravo.Config/TestTaskPravo.Config.csproj ./TestTaskPravo.Config/
COPY TestTaskPravo.Core/TestTaskPravo.Core.csproj ./TestTaskPravo.Core/
COPY TestTaskPravo.Data/TestTaskPravo.Data.csproj ./TestTaskPravo.Data/
COPY TestTaskPravo.Model/TestTaskPravo.Model.csproj ./TestTaskPravo.Model/

RUN dotnet restore ./TestTaskPravo.API/TestTaskPravo.API.csproj

# Копируем весь исходник и собираем
COPY . .
WORKDIR /src/TestTaskPravo.API
RUN dotnet publish TestTaskPravo.API.csproj -c Release -o /app/publish

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TestTaskPravo.API.dll"]