namespace messaging_service.Producer
{
    public interface IRabbitMQProducer
    {
       void SendNotification<T> (T notification);
    }
}
