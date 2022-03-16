namespace SoManyBooksSoLittleTime.Services.Data
{
    using System.Collections.Generic;

    public interface IAuthorsService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAuthorsAsKeyValuePairs();
    }
}
