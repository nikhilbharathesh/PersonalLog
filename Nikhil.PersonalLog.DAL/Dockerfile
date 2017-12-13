FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore Nikhil.PersonalLog.DAL.csproj

# Copy everything else and build
COPY . ./
RUN dotnet publish Nikhil.PersonalLog.DAL.csproj -c Release -o out
#RUN dotnet publish Nikhil.PersonalLog.DAL.csproj --output /output --configuration Release -o out
# -c Release -o out

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 5000
ENTRYPOINT ["dotnet", "Nikhil.PersonalLog.DAL.dll"]


