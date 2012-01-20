namespace RecreateMe
{
    public interface IHandler<in TRequest, out TResponse>
    {
        TResponse Handle(TRequest request);
    }
}