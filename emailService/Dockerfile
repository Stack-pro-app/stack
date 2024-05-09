FROM maven:3.8.2-openjdk-17 AS build
WORKDIR /app
COPY . .
RUN mvn clean install -DskipTests

# Runtime stage
FROM openjdk:17-alpine
WORKDIR /app
COPY --from=build /app/target/emailService-SNAPSHOT.jar .
EXPOSE 8080
CMD ["java", "-jar", "emailService-SNAPSHOT.jar"]
