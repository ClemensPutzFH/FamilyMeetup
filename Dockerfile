# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./Family-Meetup.csproj" --disable-parallel
RUN dotnet publish "./Family-Meetup.csproj" -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

#Expose all ports used in the launchSettings.json
EXPOSE 37328
EXPOSE 44355
EXPOSE 7168
EXPOSE 5168

ENTRYPOINT ["dotnet", "Family-Meetup.dll"]




# im root ordner: docker build --rm -t productive-dev/fm:latest .
# "productive-dev/fm:latest" ist nur der Name