FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app 

COPY *.sln .

COPY UserTestsREST/*.csproj ./UserTestsREST/
COPY UserTestsBL/*.csproj ./UserTestsBL/
COPY UserTestsDL/*.csproj ./UserTestsDL/
COPY UserTestsModels/*.csproj ./UserTestsModels/
COPY UTTests/*.csproj ./UTTests/

RUN cd UserTestsREST && dotnet restore

COPY . ./


RUN dotnet publish UserTestsREST -c Release -o publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app 

COPY --from=build /app/publish ./
CMD ["dotnet", "UserTestsREST.dll"]