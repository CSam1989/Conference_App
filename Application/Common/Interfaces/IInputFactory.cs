namespace Application.Common.Interfaces
{
    public interface IInputFactory
    {
        IInputStrategy GetInputStrategy(string inputType);
    }
}