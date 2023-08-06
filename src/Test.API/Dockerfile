FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8282
EXPOSE 4433

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Test.API/Test.API.csproj", "src/Test.API/"]
COPY ["src/Test.Application/Test.Application.csproj", "src/Test.Application/"]
COPY ["src/Test.Domain/Test.Domain.csproj", "src/Test.Domain/"]
COPY ["src/Test.Infrastructure/Test.Infrastructure.csproj", "Tsrc/est.Infrastructure/"]
COPY ["src/Test.UnitTest/Test.UnitTest.csproj", "src/Test.UnitTest/"] 
RUN dotnet restore "src/Test.API/Test.API.csproj"
COPY . . 

WORKDIR "src/Test.UnitTest"
RUN dotnet restore "Test.UnitTest.csproj"
RUN dotnet test --no-restore --verbosity normal

WORKDIR "../Test.API"
RUN dotnet build "Test.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Test.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Test.API.dll"]
