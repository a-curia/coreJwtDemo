FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["JwtDemo/JwtDemo.csproj", "JwtDemo/"]
RUN dotnet restore "JwtDemo/JwtDemo.csproj"
COPY . .
WORKDIR "/src/JwtDemo"
RUN dotnet build "JwtDemo.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "JwtDemo.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "JwtDemo.dll"]