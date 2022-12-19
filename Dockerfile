FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./PKIK_Project/PKIK_Project.csproj" --disable-parallel
RUN dotnet publish "./PKIK_Project/PKIK_Project.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "PKIK_Project.dll"]