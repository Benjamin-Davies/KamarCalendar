FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-stage

WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY KAMAR/*.csproj ./KAMAR/
COPY KamarCalendar/*.csproj ./KamarCalendar/
RUN dotnet restore

# copy everything else and build app
COPY KAMAR/. ./KAMAR/
COPY KamarCalendar/. ./KamarCalendar/
WORKDIR /app/KamarCalendar
RUN dotnet publish -c Release -o out


# build the runtime image

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as runtime-stage

WORKDIR /app

COPY --from=build-stage /app/KamarCalendar/out ./

CMD ASPNETCORE_URLS=http://+:$PORT dotnet KamarCalendar.dll
