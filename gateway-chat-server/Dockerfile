#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
EXPOSE 8091

RUN apt-get update && apt-get upgrade -y && \
    apt-get install -y python3 python3-pip && \
    pip3 install awscli && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

RUN --mount=type=secret,id=aws_access_key cat /run/secrets/aws_access_key > /tmp/aws_access_key && \
    --mount=type=secret,id=aws_secret_key cat /run/secrets/aws_secret_key > /tmp/aws_secret_key && \
    aws configure set aws_access_key_id $(cat /tmp/aws_access_key) && \
    aws configure set aws_secret_access_key $(cat /tmp/aws_secret_key) && \
    aws configure set default_region us-east-1 && \
    aws configure set default_output_format json

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["gateway-chat-server.csproj", "."]
RUN dotnet restore "./././gateway-chat-server.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./gateway-chat-server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./gateway-chat-server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "gateway-chat-server.dll"]

RUN rm /tmp/aws_secret_key && \
    rm /tmp/aws_access_key