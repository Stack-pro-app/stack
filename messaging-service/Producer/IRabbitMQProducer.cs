namespace messaging_service.Producer
{
    public interface IRabbitMQProducer
    {
       void SendToQueue<T> (T message,string queue);
    }
}
