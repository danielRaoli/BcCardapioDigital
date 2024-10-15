#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.



FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ENV DATABASE_CONNECTION_STRING="Host=dpg-cs6k5lrtq21c73du3sug-a.oregon-postgres.render.com;Database=dbcardapio_bqew;Username=dbcardapio_bqew_user;Password=IPhDK2DSeyEfNOtzdYRF6Z3elhe3TPUj;Port=5432;SSL Mode=Require;Trust Server Certificate=true"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BcCardapioDigital.API/BcCardapioDigital.API.csproj", "BcCardapioDigital.API/"]
RUN dotnet restore "./BcCardapioDigital.API/BcCardapioDigital.API.csproj"
COPY . .
WORKDIR "/src/BcCardapioDigital.API"
RUN dotnet build "./BcCardapioDigital.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BcCardapioDigital.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BcCardapioDigital.API.dll"]