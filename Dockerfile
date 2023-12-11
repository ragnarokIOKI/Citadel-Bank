FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BankAPI/BankAPI.csproj", "BankAPI/"]
RUN dotnet restore "BankAPI/BankAPI.csproj"
COPY . .
WORKDIR "/src/BankAPI"
RUN dotnet build "BankAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BankAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankAPI.dll"]
