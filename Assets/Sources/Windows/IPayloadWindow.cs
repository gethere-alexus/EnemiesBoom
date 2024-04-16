namespace Sources.Windows
{
    public interface IPayloadWindow<in TPayload>
    {
        void ConstructPayload(TPayload payload);
    }
}