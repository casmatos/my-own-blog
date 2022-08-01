FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
EXPOSE 442

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/CMS.Blog.WebApi/CMS.Blog.WebApi.csproj", "CMS.Blog.WebApi/"]
COPY ["src/CMS.Blog.Service/CMS.Blog.Service.csproj", "CMS.Blog.Service/"]
COPY ["src/CMS.Blog.Model/CMS.Blog.Model.csproj", "CMS.Blog.Model/"]
COPY ["src/CMS.Blog.Repository/CMS.Blog.Repository.csproj", "CMS.Blog.Repository/"]
RUN dotnet restore "CMS.Blog.WebApi/CMS.Blog.WebApi.csproj"
COPY ./src/. ./.
WORKDIR /src/CMS.Blog.WebApi
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CMS.Blog.WebApi.dll"]