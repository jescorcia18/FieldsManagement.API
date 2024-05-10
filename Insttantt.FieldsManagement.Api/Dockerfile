#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Insttantt.FieldsManagement.Api/Insttantt.FieldsManagement.Api.csproj", "Insttantt.FieldsManagement.Api/"]
COPY ["Insttant.FieldsManagement.Infrastructure/Insttantt.FieldsManagement.Infrastructure.csproj", "Insttant.FieldsManagement.Infrastructure/"]
COPY ["Insttantt.FieldsManagement.Application/Insttantt.FieldsManagement.Application.csproj", "Insttantt.FieldsManagement.Application/"]
COPY ["Insttantt.FieldsManagement.Domain/Insttantt.FieldsManagement.Domain.csproj", "Insttantt.FieldsManagement.Domain/"]
RUN dotnet restore "./Insttantt.FieldsManagement.Api/./Insttantt.FieldsManagement.Api.csproj"
COPY . .
WORKDIR "/src/Insttantt.FieldsManagement.Api"
RUN dotnet build "./Insttantt.FieldsManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Insttantt.FieldsManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Insttantt.FieldsManagement.Api.dll"]