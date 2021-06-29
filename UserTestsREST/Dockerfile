#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
# WORKDIR /app
# EXPOSE 80
# EXPOSE 443

# FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# WORKDIR /src
# COPY ["UserTestsREST/UserTestsREST.csproj", "UserTestsREST/"]
# RUN dotnet restore "UserTestsREST/UserTestsREST.csproj"
# COPY . .
# WORKDIR "/src/UserTestsREST"
# RUN dotnet build "UserTestsREST.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "UserTestsREST.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "UserTestsREST.dll"]

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app 

COPY *.sln .

COPY . .
RUN cd UserTestsREST && dotnet restore

RUN dotnet publish UserTestsREST -c Release -o publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app 

COPY --from=build /app/publish ./
CMD ["dotnet", "UserTestsREST.dll"]