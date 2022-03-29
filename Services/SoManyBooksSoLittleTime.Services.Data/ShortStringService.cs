namespace SoManyBooksSoLittleTime.Services.Data
{
    public class ShortStringService : IShortStringService
    {
        public string GetShortString(string str, int maxLength)
        {
            if (str == null)
            {
                return str;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }

            return str.Substring(0, maxLength) + "...";
        }
    }
}
