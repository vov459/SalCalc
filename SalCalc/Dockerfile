#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Используйте базовый образ SDK для сборки проекта
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SalCalc.csproj", "./"]
RUN dotnet restore "SalCalc.csproj"
COPY . .
RUN dotnet build "SalCalc.csproj" -c Release -o /app/build

# Используйте базовый образ ASP.NET для выполнения проекта
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "SalCalc.dll"]
