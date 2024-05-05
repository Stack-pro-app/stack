namespace auth_service.Producer;

public interface IRabbitMQProducer
{
   void SendRegistration<T>(T register);
}