namespace gateway_chat_server.Producer
{
    public interface IMessageProducer
    {
        void SendMessage<T> (T message);
        void SendFile<T>(T message);
    }
}
