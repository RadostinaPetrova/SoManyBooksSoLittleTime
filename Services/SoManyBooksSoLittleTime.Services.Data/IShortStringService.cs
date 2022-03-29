namespace SoManyBooksSoLittleTime.Services.Data
{
    public interface IShortStringService
    {
        string GetShortString(string str, int maxLength);
    }
}
