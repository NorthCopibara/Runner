using JokerGho5t.MessageSystem;

public class MessageClass<T> : Message
{
    private string nameEvent;
    private T parameters;
    public T param => parameters;

    public MessageClass(string nameEvent, T parameters)
    {
        this.nameEvent = nameEvent;
        this.parameters = parameters;
    }

    public static void SendEvent(string gameEvent, T param)
    {
        SendEvent(new MessageClass<T>(gameEvent, param));
    }

    private static void SendEvent(MessageClass<T> gameEventMessage)
    {
        Send(gameEventMessage.nameEvent, gameEventMessage);
    }
}
