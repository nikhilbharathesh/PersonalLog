FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Nikhil.PersonalLog.UI/*.csproj ./
RUN dotnet restore Nikhil.PersonalLog.UI.csproj

# Copy everything else and build
COPY . ./
RUN dotnet publish Nikhil.PersonalLog.UI.csproj -c Release -o out
#RUN dotnet publish Nikhil.PersonalLog.UI.csproj --output /output --configuration Release -o out
# -c Release -o out

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
#echo "current working directory: "
COPY --from=build-env /app/out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Nikhil.PersonalLog.UI.dll"]
#ENTRYPOINT ["/app", "--server.urls", "http://0.0.0.0:8080"]
