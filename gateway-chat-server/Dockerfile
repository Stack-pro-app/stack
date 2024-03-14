#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
EXPOSE 8091

RUN apt-get update && \
    apt-get install -y \
    python3 \
    python3-pip

# Install AWS CLI using pip
RUN pip3 install awscli

# Configure AWS CLI (you may choose to provide these values through environment variables instead)
RUN aws configure set aws_access_key_id AKIAYI4HD5JJE7IVFNXH
RUN aws configure set aws_secret_access_key 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
RUN aws configure set default_region us-east-1
RUN aws configure set default_output_format json

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