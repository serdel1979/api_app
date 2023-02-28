FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy everything
COPY *.csproj .
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
COPY . .
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /app
COPY --from=build /app/out .


ENTRYPOINT ["dotnet", "api_app.dll"]