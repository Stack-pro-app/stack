FROM maven:3.8.4-openjdk-17-slim as build
WORKDIR /app
COPY . .
RUN mvn clean install

FROM openjdk:17-jdk-alpine
WORKDIR /app
COPY --from=build /app/target/eureka-server-0.0.1-SNAPSHOT.jar .
EXPOSE 8761
CMD ["java", "-jar", "eureka-server-0.0.1-SNAPSHOT.jar"]