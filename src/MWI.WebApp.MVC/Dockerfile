#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/MWI.WebApp.MVC/MWI.WebApp.MVC.csproj", "src/MWI.WebApp.MVC/"]
RUN dotnet restore "src/MWI.WebApp.MVC/MWI.WebApp.MVC.csproj"
COPY . .
WORKDIR "/src/src/MWI.WebApp.MVC"
RUN dotnet build "MWI.WebApp.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MWI.WebApp.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MWI.WebApp.MVC.dll"]