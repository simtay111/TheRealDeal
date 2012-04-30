namespace RecreateMe
{
    public interface IHandle<in TRequest, out TResponse>
    {
        TResponse Handle(TRequest request);
    }
}