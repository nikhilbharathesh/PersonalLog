FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore Nikhil.PersonalLog.Web.csproj
COPY . ./
#RUN dotnet build Nikhil.PersonalLog.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Nikhil.PersonalLog.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Nikhil.PersonalLog.Web.dll"]
