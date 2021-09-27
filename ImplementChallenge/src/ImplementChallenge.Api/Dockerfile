FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
 
WORKDIR /app
EXPOSE 80
EXPOSE 8080 
COPY ImplementChallenge.Api.csproj ./
 
RUN dotnet restore
 
COPY . ./
 
RUN dotnet publish -c Debug -o out
 
FROM mcr.microsoft.com/dotnet/aspnet:5.0
 
WORKDIR /app
 
COPY --from=build-env /app/out .
 
ENTRYPOINT [ "dotnet", "ImplementChallenge.Api.dll" ]

