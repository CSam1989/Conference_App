namespace Application.Common.Interfaces
{
    public interface IInputValidationService
    {
        bool IsValidTalkTitle(string name);
        bool IsValidTalkDuration(string duration, out int output);
    }
}