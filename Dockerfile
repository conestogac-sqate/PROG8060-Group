FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Backend.MovieManagement/Backend.MovieManagement.csproj", "Backend.MovieManagement/"]
RUN dotnet restore "Backend.MovieManagement/Backend.MovieManagement.csproj"
COPY . .
WORKDIR "/src/Backend.MovieManagement"
RUN dotnet build "Backend.MovieManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Backend.MovieManagement.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backend.MovieManagement.dll"]
